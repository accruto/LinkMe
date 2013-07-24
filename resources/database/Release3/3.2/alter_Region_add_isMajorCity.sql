ALTER TABLE Region
ADD isMajorCity BIT NULL
GO

UPDATE Region
SET isMajorCity = 1
GO

UPDATE Region
SET isMajorCity = 0
WHERe urlName = 'gold-coast'
GO

ALTER TABLE Region
ALTER COLUMN isMajorCity BIT NOT NULL
GO
