IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CampaignTemplate') AND type in (N'U'))
DROP TABLE dbo.CampaignTemplate
GO

CREATE TABLE dbo.CampaignTemplate
(
	id UNIQUEIDENTIFIER NOT NULL,
	subject TEXT NOT NULL,
	body TEXT NOT NULL
)

ALTER TABLE dbo.CampaignTemplate
ADD CONSTRAINT PK_CampaignTemplate PRIMARY KEY NONCLUSTERED
(
	id
)
GO

ALTER TABLE dbo.CampaignTemplate
ADD CONSTRAINT FK_CampaignTemplate_Campaign FOREIGN KEY (id)
REFERENCES dbo.Campaign (id)
GO

