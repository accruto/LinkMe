ALTER TABLE dbo.CandidateAccessPurchase
ADD adjustedAllocation BIT NULL
GO

UPDATE
	dbo.CandidateAccessPurchase
SET
	adjustedAllocation = 0
GO

-- Make the column not null

ALTER TABLE dbo.CandidateAccessPurchase
ALTER COLUMN adjustedAllocation BIT NOT NULL
GO

