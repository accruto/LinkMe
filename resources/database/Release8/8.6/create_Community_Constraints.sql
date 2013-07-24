-- Community

ALTER TABLE Community
	ADD CONSTRAINT UQ_Community_name UNIQUE (name)
GO

ALTER TABLE Community
	ADD CONSTRAINT UQ_Community_url UNIQUE (url)
GO

ALTER TABLE Community ADD CONSTRAINT PK_Community
	PRIMARY KEY CLUSTERED (id)
GO

-- CommunityMember

ALTER TABLE CommunityMember ADD CONSTRAINT PK_CommunityMember
	PRIMARY KEY CLUSTERED (id)
GO

ALTER TABLE CommunityMember ADD CONSTRAINT FK_primaryCommunityId
	FOREIGN KEY (primaryCommunityId) REFERENCES Community (id)
GO

-- CommunityOrganisationalUnit

ALTER TABLE CommunityOrganisationalUnit ADD CONSTRAINT PK_CommunityOrganisationalUnit
	PRIMARY KEY CLUSTERED (id)
GO

-- CommunityAssociation

ALTER TABLE CommunityAssociation ADD CONSTRAINT PK_CommunityAssociation
	PRIMARY KEY CLUSTERED (organisationalUnitId, communityId)
GO

ALTER TABLE CommunityAssociation ADD CONSTRAINT FK_CommunityAssociation_CommunityOrganisationalUnit
	FOREIGN KEY (organisationalUnitId) REFERENCES CommunityOrganisationalUnit (id)
GO

ALTER TABLE CommunityAssociation ADD CONSTRAINT FK_CommunityAssociation_Community
	FOREIGN KEY (communityId) REFERENCES Community (id)
GO

