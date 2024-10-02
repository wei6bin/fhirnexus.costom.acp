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

### Step 2: Run the project
- The project will expose the PatientFHIR table as a FHIR resource, and the data can be accessed using the FHIR API. Refer to the `patient.http` file for the sample API requests.
	- get the patient by id `GET {{baseUrl}}/Patient/3`
	- search the patient by name `GET {{baseUrl}}/Patient?gender=Male` with pagination parameters `_count=10&_page=1`

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

In order to map to Questionniare resource, the following tables are created:
- FormQuestionnaire
```sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormQuestionnaire](
	[OID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[FormType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_FormQuestionnaire] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
- WorksheetQuestionnaire
```sql
CREATE TABLE [dbo].[WorksheetQuestionnaire](
	[OID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[FormType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_WorksheetQuestionnaire] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

And prepopulate the tables with the following data:
- FormQuestionnaire
```sql
SET IDENTITY_INSERT [dbo].[FormQuestionnaire] ON 
GO
INSERT [dbo].[FormQuestionnaire] ([OID], [FormType]) VALUES (1,N'GENERAL')
GO
INSERT [dbo].[FormQuestionnaire] ([OID], [FormType]) VALUES (2,N'DS')
GO
INSERT [dbo].[FormQuestionnaire] ([OID], [FormType]) VALUES (3,N'PPC')
GO
SET IDENTITY_INSERT [dbo].[FormQuestionnaire] OFF
GO
```

- WorksheetQuestionnaire
```sql
SET IDENTITY_INSERT [dbo].[WorksheetQuestionnaire] ON 
GO
INSERT [dbo].[WorksheetQuestionnaire] ([OID], [FormType]) VALUES (1,N'GENERAL')
GO
INSERT [dbo].[WorksheetQuestionnaire] ([OID], [FormType]) VALUES (2,N'DS')
GO
INSERT [dbo].[WorksheetQuestionnaire] ([OID], [FormType]) VALUES (3,N'PPC')
GO
SET IDENTITY_INSERT [dbo].[WorksheetQuestionnaire] OFF
GO
```

- create the data model ``