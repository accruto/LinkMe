IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Survey') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Survey
GO

CREATE TABLE Survey ( 
	id uniqueidentifier NOT NULL,
	title nvarchar(200) NULL,
	questionText nvarchar(200) NULL,
	isOpen bit NULL
)
GO

ALTER TABLE Survey ADD CONSTRAINT PK_Survey 
	PRIMARY KEY CLUSTERED (id)
GO





