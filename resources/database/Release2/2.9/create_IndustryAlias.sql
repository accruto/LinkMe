IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('linkme_owner.IndustryAlias') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE linkme_owner.IndustryAlias
GO

CREATE TABLE linkme_owner.IndustryAlias
( 
	id uniqueidentifier NOT NULL,
	industryId uniqueidentifier NOT NULL,
	normalisedName varchar(50) NOT NULL
)
GO

ALTER TABLE linkme_owner.IndustryAlias
	ADD CONSTRAINT UQ_IndustryAlias_normalisedName UNIQUE (normalisedName)
GO

ALTER TABLE linkme_owner.IndustryAlias ADD CONSTRAINT PK_IndustryAlias 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE linkme_owner.IndustryAlias ADD CONSTRAINT FK_IndustryAlias_Industry 
	FOREIGN KEY (industryId) REFERENCES linkme_owner.Industry (id)
GO
