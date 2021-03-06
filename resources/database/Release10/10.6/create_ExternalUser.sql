IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExternalUser]') AND type in (N'U'))
DROP TABLE [dbo].[ExternalUser]
GO

CREATE TABLE dbo.ExternalUser(
	id uniqueidentifier NOT NULL,
	externalProviderId uniqueidentifier NOT NULL,
	externalId NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_ExternalUser PRIMARY KEY CLUSTERED 
	(
		externalProviderId,
		id
	)
)
GO

