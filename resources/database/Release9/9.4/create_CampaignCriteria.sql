IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CampaignCriteria') AND type in (N'U'))
DROP TABLE dbo.CampaignCriteria
GO

CREATE TABLE dbo.CampaignCriteria
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(50) NOT NULL,
	value SQL_VARIANT NULL
)

ALTER TABLE dbo.CampaignCriteria
ADD CONSTRAINT PK_CampaignCriteria PRIMARY KEY NONCLUSTERED
(
	id,
	name
)
GO

ALTER TABLE dbo.CampaignCriteria
ADD CONSTRAINT FK_CampaignCriteria_CampaignCriteriaSet FOREIGN KEY (id)
REFERENCES dbo.CampaignCriteriaSet (id)
GO

