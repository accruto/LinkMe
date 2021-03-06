IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DatabaseScript]') AND type in (N'U'))
DROP TABLE [dbo].[DatabaseScript]
GO

CREATE TABLE dbo.DatabaseScript
(
	id INT IDENTITY NOT FOR REPLICATION NOT NULL,
	[time] DATETIME NOT NULL,
	succeeded BIT NOT NULL,
	[fileName] NVARCHAR(256) NULL,
	filePath NVARCHAR(256) NULL,
	[checksum] BINARY(20) NOT NULL,
	[text] NTEXT NOT NULL
)
GO

ALTER TABLE dbo.DatabaseScript
ADD CONSTRAINT PK_DatabaseScript PRIMARY KEY CLUSTERED ([id])
GO
