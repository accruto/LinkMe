ALTER TABLE dbo.CampaignCriteriaSet
ADD campaignId UNIQUEIDENTIFIER NULL
GO

UPDATE
	dbo.CampaignCriteriaSet
SET
	campaignId = id

ALTER TABLE dbo.CampaignCriteriaSet
ALTER COLUMN campaignId UNIQUEIDENTIFIER NOT NULL

ALTER TABLE [dbo].[CampaignCriteria] DROP CONSTRAINT [FK_CampaignCriteria_CampaignCriteriaSet]
GO

UPDATE
	dbo.CampaignCriteriaSet
SET
	id = NEWID()

UPDATE
	dbo.CampaignCriteria
SET
	dbo.CampaignCriteria.id = (SELECT s.id FROM dbo.CampaignCriteriaSet AS s WHERE s.campaignId = dbo.CampaignCriteria.id)
GO

ALTER TABLE dbo.CampaignCriteria
ADD CONSTRAINT FK_CampaignCriteria_CampaignCriteriaSet FOREIGN KEY (id)
REFERENCES dbo.CampaignCriteriaSet (id)
GO
