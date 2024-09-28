## This sample project demonstrates how to connect to existing data store using the FHIRNexus.

The demo here is to connect PatientFHIR table (created based on the Patient table), and expose it as a FHIR resource. The existing table structure is modified to include the FHIR specific fields.

### Step 1: Create a new project

```sql
SELECT *
INTO [dbo].[PatientFHIR]
FROM [dbo].[Patient]

ALTER TABLE [dbo].[PatientFHIR] ADD [VersionId] [int] NOT NULL DEFAULT 1
ALTER TABLE [dbo].[PatientFHIR] ADD [TimeStamp] [varbinary](max) NULL
ALTER TABLE [dbo].[PatientFHIR] ADD [LastUpdated] [datetimeoffset](7) NULL
ALTER TABLE [dbo].[PatientFHIR] ADD [IsHistory] [bit] NOT NULL DEFAULT 0

EXEC sp_rename 'dbo.PatientFHIR.OID', 'Id', 'COLUMN'
```

### Step 2: Create a new project with Custom data store, refer to current project
- define the Patient model (PatientFHIR)
- implement the Patient data mapper, and register the mapper class at fhirengine json (UseDataMapper)
- support the Patient search service
- define the Patient resource structure definition:
	- enable validation: to restrict the cardinality of data fields. e.g. the `birthDate` is optional defined by FHIR base profile, use custom profile to enable it as mandatory field.
	- document extensions: to allow for the definition of extensions to standard resources, e.g. to accept the `citizenship` as an extension.
- define the ValueSet for the `citizenship` field, to restrict the value set of the field, refer to the `seed-valuesets.json`. Alternatively, the terminology service can be used to manage the value set.
- point to the ACP local database

### Step 3: Run the project
- The project will expose the PatientFHIR table as a FHIR resource, and the data can be accessed using the FHIR API. Refer to the `patient.http` file for the sample API requests.
	- get the patient by id `GET {{baseUrl}}/Patient/3`
	- search the patient by name `GET {{baseUrl}}/Patient?gender=Male` with pagination parameters `_count=10&_page=1`