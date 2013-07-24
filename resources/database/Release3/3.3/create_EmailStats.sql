IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('EmailStats') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE EmailStats
GO

CREATE TABLE EmailStats ( 
	id uniqueidentifier NOT NULL,
	emailId varchar(50) NOT NULL,
	verticalId varchar(50) NULL,
	variationId varchar(50) NULL,
	sends int NOT NULL
)
GO

ALTER TABLE EmailStats ADD CONSTRAINT PK_EmailStats 
	PRIMARY KEY CLUSTERED (id)
GO






