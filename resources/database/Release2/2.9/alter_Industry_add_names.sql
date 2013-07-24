ALTER TABLE linkme_owner.Industry
ADD shortDisplayName NVARCHAR(200), normalisedName VARCHAR(30)
GO

-- Temporary hack just to set NOT NULL - needs to be overwritten with real data
UPDATE linkme_owner.Industry
SET shortDisplayName = displayName, normalisedName = displayName
GO

ALTER TABLE linkme_owner.Industry
ALTER COLUMN shortDisplayName NVARCHAR(200) NOT NULL

ALTER TABLE linkme_owner.Industry
ALTER COLUMN normalisedName VARCHAR(30) NOT NULL
GO

ALTER TABLE linkme_owner.Industry
ADD CONSTRAINT UQ_Industry_shortDisplayName UNIQUE (shortDisplayName)

ALTER TABLE linkme_owner.Industry
ADD CONSTRAINT UQ_Industry_normalisedName UNIQUE (normalisedName)
GO
