
## Custom Resource

> refer to Synapxe.FhirWebApi.CustomResource

- new table `Questionniare`:
```sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.Questionnaire', 'U') IS NOT NULL
    DROP TABLE dbo.Questionnaire;
GO

CREATE TABLE [dbo].[Questionnaire](
	[Id] [varchar](64) NOT NULL,
	[OID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[FormType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Questionnaire] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[Questionnaire] ([Id], [FormType]) VALUES (NEWID(),N'FORM_GENERAL')
GO
INSERT [dbo].[Questionnaire] ([Id], [FormType]) VALUES (NEWID(),N'FORM_DS')
GO
INSERT [dbo].[Questionnaire] ([Id], [FormType]) VALUES (NEWID(),N'FORM_PPC')
GO
INSERT [dbo].[Questionnaire] ([Id], [FormType]) VALUES (NEWID(),N'WORKSHEET_GENERAL')
GO
INSERT [dbo].[Questionnaire] ([Id], [FormType]) VALUES (NEWID(),N'WORKSHEET_DS')
GO
INSERT [dbo].[Questionnaire] ([Id], [FormType]) VALUES (NEWID(),N'WORKSHEET_PPC')
GO
```

-- alter table `FormQuestion_MA` to add new column `AcpFormEntityId` as foreign key to `Questionnaire` table, and new column `Id` as primary key:
```sql
ALTER TABLE [dbo].[FormQuestion_MA]
ADD [AcpFormEntityId] [varchar](64) NULL

ALTER TABLE [dbo].[FormQuestion_MA]
ADD [IsHistory] [bit] NOT NULL DEFAULT 0

ALTER TABLE [dbo].[FormQuestion_MA]
ADD CONSTRAINT [FK_FormQuestion_MA_Questionnaire]
FOREIGN KEY ([AcpFormEntityId]) REFERENCES [dbo].[Questionnaire] ([Id]);
```

-- update the data based on the `FormType` and `WorksheetType` respectively.
```sql

UPDATE FormQuestion_MA SET AcpFormEntityId = 'E45A8174-DBA2-4176-A7E3-DAE0D22F7C78' WHERE FormType = 'DS'
UPDATE FormQuestion_MA SET AcpFormEntityId = '18082E76-E1F4-455E-9936-D6EC6B6E684A' WHERE FormType = 'GENERAL'
UPDATE FormQuestion_MA SET AcpFormEntityId = '8D4A3854-0E3C-4DF1-AB5A-EAD02FB596AA' WHERE FormType = 'PPC'

```

-- update column type from `DateTime` to `DateTimeOffset`:
```sql

ALTER TABLE FormQuestion_MA
ADD CreatedOnOffset DATETIMEOFFSET;
GO

UPDATE FormQuestion_MA
SET CreatedOnOffset = CreatedOn AT TIME ZONE 'Singapore Standard Time';
GO

ALTER TABLE FormQuestion_MA
DROP COLUMN CreatedOn;
GO

EXEC sp_rename 'FormQuestion_MA.CreatedOnOffset', 'CreatedOn', 'COLUMN';
GO

```

alternatively, can use the stored procedure to update the data type: (# need further test)
```sql
CREATE PROCEDURE ConvertDateTimeToDateTimeOffset
    @TableName NVARCHAR(128),
    @ColumnName NVARCHAR(128),
    @TimeZone NVARCHAR(128) = 'Singapore Standard Time'
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);

    -- Step 1: Add a new datetimeoffset column
    SET @SQL = N'ALTER TABLE ' + QUOTENAME(@TableName) + 
               N' ADD ' + QUOTENAME(@ColumnName + 'Offset') + N' DATETIMEOFFSET;';
    EXEC sp_executesql @SQL;

    -- Step 2: Update the new column with converted values
    SET @SQL = N'UPDATE ' + QUOTENAME(@TableName) + 
               N' SET ' + QUOTENAME(@ColumnName + 'Offset') + N' = ' + QUOTENAME(@ColumnName) + 
               N' AT TIME ZONE ' + QUOTENAME(@TimeZone) + N';';
    EXEC sp_executesql @SQL;

    -- Step 3: Drop the old datetime column
    SET @SQL = N'ALTER TABLE ' + QUOTENAME(@TableName) + 
               N' DROP COLUMN ' + QUOTENAME(@ColumnName) + N';';
    EXEC sp_executesql @SQL;

    -- Step 4: Rename the new datetimeoffset column to the original column name
    SET @SQL = N'EXEC sp_rename ''' + @TableName + N'.' + @ColumnName + 'Offset'', ''' + @ColumnName + ''', ''COLUMN'';';
    EXEC sp_executesql @SQL;
END
GO

EXEC ConvertDateTimeToDateTimeOffset @TableName = 'FormQuestion_MA', @ColumnName = 'CreatedOn'; 
```

- table `Question_MA`
```sql
ALTER TABLE [dbo].[Question_MA]
ADD [AcpFormEntityId] [varchar](64) NULL

ALTER TABLE [dbo].[Question_MA]
ADD CONSTRAINT [FK_Question_MA_Questionnaire]
FOREIGN KEY ([AcpFormEntityId]) REFERENCES [dbo].[Questionnaire] ([Id]);

UPDATE q
SET q.AcpFormEntityId = f.AcpFormEntityId
FROM [dbo].[Question_MA] q
INNER JOIN [dbo].[FormQuestion_MA] f
    ON q.OID = f.QuestionOID
-- and update datetime to datetimeoffset
```

- table `WorksheetQuestion_MA`
```sql
ALTER TABLE [dbo].[WorksheetQuestion_MA]
ADD [AcpFormEntityId] [varchar](64) NULL

ALTER TABLE [dbo].[WorksheetQuestion_MA]
ADD CONSTRAINT [FK_WorksheetQuestion_MA_Questionnaire]
FOREIGN KEY ([AcpFormEntityId]) REFERENCES [dbo].[Questionnaire] ([Id]);

UPDATE WorksheetQuestion_MA SET AcpFormEntityId = '1E1ADD44-F1EA-48E7-9A95-2108093B9E79' WHERE WorksheetType = 'DS'
UPDATE WorksheetQuestion_MA SET AcpFormEntityId = '9D628696-0DE4-4A7E-B89D-1132926FEF2F' WHERE WorksheetType = 'GENERAL'
UPDATE WorksheetQuestion_MA SET AcpFormEntityId = '68672224-A225-40A7-B83A-559919E0C427' WHERE WorksheetType = 'PPC'

UPDATE q
SET q.AcpFormEntityId = f.AcpFormEntityId
FROM [dbo].[Question_MA] q
INNER JOIN [dbo].[WorksheetQuestion_MA] f
    ON q.OID = f.QuestionOID

-- and update datetime to datetimeoffset
```