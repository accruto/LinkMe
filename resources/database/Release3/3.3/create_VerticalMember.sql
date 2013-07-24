IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('VerticalMember') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE VerticalMember
GO

CREATE TABLE VerticalMember ( 
	id UNIQUEIDENTIFIER NOT NULL,
	primaryVerticalId UNIQUEIDENTIFIER NOT NULL
)
GO

ALTER TABLE VerticalMember ADD CONSTRAINT PK_VerticalMember
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE VerticalMember ADD CONSTRAINT FK_primaryVerticalId
	FOREIGN KEY (primaryVerticalId) REFERENCES Vertical (id)
GO






