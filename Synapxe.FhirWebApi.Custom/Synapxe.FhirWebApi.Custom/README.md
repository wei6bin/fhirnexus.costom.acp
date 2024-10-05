## This sample project demonstrates how to connect to existing data store using the FHIRNexus.

The demo here is to connect PatientFHIR table (created based on the Patient table), and expose it as a FHIR resource. The existing table structure is modified to include the FHIR specific fields.

### Step 1: Create a new project with Custom data store, refer to current project
- define the Patient model (PatientFHIR)
- implement the Patient data mapper, and register the mapper class at fhirengine json (UseDataMapper)
- support the Patient search service
- define the Patient resource structure definition:
	- enable validation: to restrict the cardinality of data fields. e.g. the `birthDate` is optional defined by FHIR base profile, use custom profile to enable it as mandatory field.
	- document extensions: to allow for the definition of extensions to standard resources, e.g. to accept the `citizenship` as an extension.
- define the ValueSet for the `citizenship` field, to restrict the value set of the field, refer to the `seed-valuesets.json`. Alternatively, the terminology service can be used to manage the value set.
- point to the ACP local database

## Resource Mapping:
### Questionnaire maps to tables

- Form Questionnaire
	- FormQuestion_MA
	- Question_MA
	- QuestionOption_MA

- Worksheet Questionnaire
	- WorksheetQuestion_MA
	- Question_MA
	- QuestionOption_MA

In order to map to Questionniare resource, the following table is created:
```sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire](
	[OID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[FormType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Questionnaire] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

And prepopulate the tables with the following data:
```sql
SET IDENTITY_INSERT [dbo].[Questionnaire] ON 
GO
INSERT [dbo].[Questionnaire] ([OID], [FormType]) VALUES (1,N'FORM_GENERAL')
GO
INSERT [dbo].[Questionnaire] ([OID], [FormType]) VALUES (2,N'FORM_DS')
GO
INSERT [dbo].[Questionnaire] ([OID], [FormType]) VALUES (3,N'FORM_PPC')
GO
INSERT [dbo].[Questionnaire] ([OID], [FormType]) VALUES (4,N'WORKSHEET_GENERAL')
GO
INSERT [dbo].[Questionnaire] ([OID], [FormType]) VALUES (5,N'WORKSHEET_DS')
GO
INSERT [dbo].[Questionnaire] ([OID], [FormType]) VALUES (6,N'WORKSHEET_PPC')
GO
SET IDENTITY_INSERT [dbo].[Questionnaire] OFF
GO
```

- create the data model `QuestionnaireModel.cs`
- create the data mapper `QuestionnaireDataMapper.cs`
- register the data mapper at `UseDataMapper.cs`
- alter existing table `FormQuestion_MA` and `WorksheetQuestion_MA` to include the `QuestionnaireOID` field, and update the data based on the `FormType` and `WorksheetType` respectively.
```sql
ALTER TABLE [dbo].[FormQuestion_MA] ADD [QuestionnaireOID] [bigint] NULL;
GO

ALTER TABLE [dbo].[FormQuestion_MA]  WITH CHECK ADD  CONSTRAINT [FK_FormQuestion_Questionnaire] FOREIGN KEY([QuestionnaireOID])
REFERENCES [dbo].[Questionnaire] ([OID])
GO

ALTER TABLE [dbo].[FormQuestion_MA] CHECK CONSTRAINT [FK_FormQuestion_Questionnaire]
GO

UPDATE [dbo].[FormQuestion_MA]
SET [QuestionnaireOID] = CASE [FormType]
    WHEN 'GENERAL' THEN 1
    WHEN 'DS' THEN 2
    WHEN 'PPC' THEN 3
END
WHERE [FormType] IN ('GENERAL', 'DS', 'PPC');
GO

ALTER TABLE [dbo].[WorksheetQuestion_MA] ADD [QuestionnaireOID] [bigint] NULL;
GO

UPDATE [dbo].[WorksheetQuestion_MA]
SET [QuestionnaireOID] = CASE [WorksheetType]
	WHEN 'GENERAL' THEN 4
	WHEN 'DS' THEN 5
	WHEN 'PPC' THEN 6
END
WHERE [WorksheetType] IN ('GENERAL', 'DS', 'PPC');
```
- alter the `Question_MA` table to include the column `QuestionnaireOID`
```sql
ALTER TABLE Question_MA
ADD QuestionnaireOID bigint;

ALTER TABLE Question_MA
ADD CONSTRAINT FK_Question_MA_Questionnaire
FOREIGN KEY (QuestionnaireOID) REFERENCES Questionnaire(OID);
```
- update the `Question_MA` table column `FormQuestionOID` based on the `QuestionID` field and `OID` field of the `FormQuestion_MA` table.
```sql
UPDATE Question_MA
SET QuestionnaireOID = C.OID
FROM Question_MA A INNER JOIN FormQuestion_MA B ON A.OID = B.QuestionOID
	INNER JOIN Questionnaire C ON C.OID = B.QuestionnaireOID

UPDATE Question_MA
SET QuestionnaireOID = C.OID
FROM Question_MA A INNER JOIN WorksheetQuestion_MA B ON A.OID = B.QuestionOID
	INNER JOIN Questionnaire C ON C.OID = B.QuestionnaireOID
```
- query to check the overlap question records at Form and Worksheet level
```sql
WITH FirstQuery AS (
select QuestionOID from Question_MA A INNER JOIN FormQuestion_MA B ON A.OID = B.QuestionOID
	INNER JOIN Questionnaire C ON C.OID = B.QuestionnaireOID
),
SecondQuery AS
(select QuestionOID from Question_MA A INNER JOIN WorksheetQuestion_MA B ON A.OID = B.QuestionOID
	INNER JOIN Questionnaire C ON C.OID = B.QuestionnaireOID)

	SELECT QuestionOID
FROM FirstQuery
INTERSECT
SELECT QuestionOID
FROM SecondQuery;
```
- update the `capability-statement.json` to include the Questionnaire resource

### Step 2: Run the project
- The project will expose the PatientFHIR table as a FHIR resource, and the data can be accessed using the FHIR API. Refer to the `patient.http` file for the sample API requests.
	- get the patient by id `GET {{baseUrl}}/Patient/3`
	- search the patient by name `GET {{baseUrl}}/Patient?gender=Male` with pagination parameters `_count=10&_page=1`
- refer to the `questionnaire.http` file for the sample API requests.
	- get the questionnaire by id `GET {{baseUrl}}/Questionnaire/1`
	- search the questionnaire by type `GET {{baseUrl}}/Questionnaire?form-type=FORM_GENERAL`, ideally it returns a bundle with single resource.