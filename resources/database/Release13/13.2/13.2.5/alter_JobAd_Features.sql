ALTER TABLE
	dbo.JobAd
ADD
	features INT NULL
GO

UPDATE
	dbo.JobAd
SET
	features = 0
GO

ALTER TABLE
	dbo.JobAd
ALTER COLUMN
	features INT NOT NULL
GO

