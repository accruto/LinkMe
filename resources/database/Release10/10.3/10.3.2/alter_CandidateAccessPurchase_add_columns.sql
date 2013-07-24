ALTER TABLE dbo.CandidateAccessPurchase
ADD allocationId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.CandidateAccessPurchase
ADD referenceId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.CandidateAccessPurchase
ALTER COLUMN candidateId UNIQUEIDENTIFIER NULL
GO
