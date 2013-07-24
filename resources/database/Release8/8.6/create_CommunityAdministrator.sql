IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('CommunityAdministrator') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE CommunityAdministrator
GO

CREATE TABLE CommunityAdministrator ( 
	id uniqueidentifier NOT NULL
)
GO

ALTER TABLE CommunityAdministrator ADD CONSTRAINT PK_CommunityAdministrator 
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE CommunityAdministrator ADD CONSTRAINT FK_CommunityAdministrator_RegisteredUser 
	FOREIGN KEY (id) REFERENCES RegisteredUser (id)
GO


