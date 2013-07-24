IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('Contributor') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Contributor;

CREATE TABLE Contributor ( 
	id uniqueidentifier NOT NULL
);

ALTER TABLE Contributor ADD CONSTRAINT PK_Contributor 
	PRIMARY KEY CLUSTERED (id);


