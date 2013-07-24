ALTER TABLE dbo.CandidateNote
ADD ownedById UNIQUEIDENTIFIER NULL
GO

CREATE INDEX IX_CandidateNote_OwnedBy
ON dbo.CandidateNote (ownedById)
GO
