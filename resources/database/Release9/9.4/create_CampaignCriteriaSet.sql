IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CampaignCriteriaSet') AND type in (N'U'))
DROP TABLE dbo.CampaignCriteriaSet
GO

CREATE TABLE dbo.CampaignCriteriaSet
(
	id UNIQUEIDENTIFIER NOT NULL,
	type NVARCHAR(50) NOT NULL,
)

ALTER TABLE dbo.CampaignCriteriaSet
ADD CONSTRAINT PK_CampaignCriteriaSet PRIMARY KEY NONCLUSTERED
(
	id
)
GO

ALTER TABLE dbo.CampaignCriteriaSet
ADD CONSTRAINT FK_CampaignCriteriaSet_Campaign FOREIGN KEY (id)
REFERENCES dbo.Campaign (id)
GO

