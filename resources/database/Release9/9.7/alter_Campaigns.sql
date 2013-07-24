IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('dbo.CampaignCriteriaSet') AND NAME = 'type')
	ALTER TABLE dbo.CampaignCriteriaSet
	DROP COLUMN type
GO

