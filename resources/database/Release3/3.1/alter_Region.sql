ALTER TABLE dbo.Region
ADD urlName LocationDisplayName NULL
GO

ALTER TABLE dbo.Region ALTER COLUMN urlName LocationDisplayName NOT NULL
GO

ALTER TABLE dbo.Region
ADD CONSTRAINT UQ_Region_urlName UNIQUE (urlName)

GO