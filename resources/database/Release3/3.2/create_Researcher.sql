IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Researcher') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Researcher
GO

CREATE TABLE Researcher ( 
	id uniqueidentifier NOT NULL
)
GO

ALTER TABLE Researcher ADD CONSTRAINT PK_Researcher 
	PRIMARY KEY CLUSTERED (id)
GO


