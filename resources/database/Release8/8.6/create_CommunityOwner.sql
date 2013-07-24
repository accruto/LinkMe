IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('CommunityAgent') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE CommunityAgent
GO

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('CommunityOwner') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE CommunityOwner
GO

CREATE TABLE CommunityOwner ( 
	id UNIQUEIDENTIFIER NOT NULL,
	communityId UNIQUEIDENTIFIER NOT NULL
)
GO

ALTER TABLE CommunityOwner ADD CONSTRAINT PK_CommunityOwner
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE CommunityOwner ADD CONSTRAINT FK_communityId
	FOREIGN KEY (communityId) REFERENCES Community (id)
GO






