IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CommunityEmployerEnquiry') AND type in (N'U'))
DROP TABLE dbo.CommunityEmployerEnquiry
GO

CREATE TABLE dbo.CommunityEmployerEnquiry
(
	id UNIQUEIDENTIFIER NOT NULL,
	communityId UNIQUEIDENTIFIER NOT NULL,
	createdTime DATETIME NOT NULL,
	companyName NVARCHAR(100) NOT NULL,
	emailAddress NVARCHAR(320) NOT NULL,
	firstName NVARCHAR(30) NOT NULL,
	lastName NVARCHAR(30) NOT NULL,
	jobTitle NVARCHAR(100) NULL,
	phoneNumber NVARCHAR(20) NOT NULL
)

ALTER TABLE dbo.CommunityEmployerEnquiry
ADD CONSTRAINT PK_CommunityEmployerEnquiry PRIMARY KEY NONCLUSTERED
(
	id
)
GO

ALTER TABLE dbo.CommunityEmployerEnquiry
ADD CONSTRAINT FK_CommunityEmployerEnquiry_Community FOREIGN KEY (communityId)
REFERENCES dbo.Community (id)
GO

