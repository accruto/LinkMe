IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.Organisation') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.Organisation
GO

CREATE TABLE dbo.Organisation
( 
	[id] uniqueidentifier NOT NULL,
	displayName CompanyName NOT NULL
)
GO

ALTER TABLE dbo.Organisation
ADD CONSTRAINT PK_Organisation 
PRIMARY KEY CLUSTERED ([id])
GO
