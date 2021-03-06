IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.RegionAlias') AND type in (N'U'))
DROP TABLE dbo.RegionAlias
GO

CREATE TABLE dbo.RegionAlias
(
	id INT NOT NULL,
	displayName LocationDisplayName NOT NULL,
	regionId INT NOT NULL
)
GO

ALTER TABLE dbo.RegionAlias
ADD CONSTRAINT PK_RegionAlias PRIMARY KEY CLUSTERED
(
	id
)

ALTER TABLE dbo.RegionAlias
ADD CONSTRAINT FK_RegionAlias_Region FOREIGN KEY(regionId)
REFERENCES dbo.Region (id)
GO

INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (1, 'Canberra (All Canberra)', 2808)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (2, 'Sydney (All Sydney)', 2809)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (3, 'Darwin (All Darwin)', 2810)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (4, 'Brisbane (All Brisbane)', 2811)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (5, 'Adelaide (All Adelaide)', 2812)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (6, 'Hobart (All Hobart)', 2813)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (7, 'Melbourne (All Melbourne)', 2814)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (8, 'Perth (All Perth)', 2815)
INSERT dbo.RegionAlias (id, displayName, regionId) VALUES (9, 'Gold Coast (All Gold Coast)', 2816)