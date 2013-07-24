-- Drop the normalisedName column

ALTER TABLE dbo.Industry
DROP CONSTRAINT UQ_Industry_normalisedName

ALTER TABLE dbo.Industry
DROP COLUMN normalisedName

-- Add the urlName column

ALTER TABLE dbo.Industry
ADD urlName NVARCHAR(100) NOT NULL DEFAULT ''

GO