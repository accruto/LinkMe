ALTER TABLE dbo.JobApplication
ADD isDeleted BIT NULL
GO

-- Set all to 0 to signify not deleted.

UPDATE
	dbo.JobApplication
SET
	isDeleted = 0

-- Make not null.

ALTER TABLE dbo.JobApplication
ALTER COLUMN isDeleted BIT NOT NULL
GO

ALTER TABLE dbo.JobApplication
ADD isFeatured BIT NULL
GO

-- Set all to 0 to signify not featured.

UPDATE
	dbo.JobApplication
SET
	isFeatured = 0

-- Make not null.

ALTER TABLE dbo.JobApplication
ALTER COLUMN isFeatured BIT NOT NULL
GO

