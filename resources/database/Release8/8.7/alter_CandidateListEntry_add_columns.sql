ALTER TABLE dbo.CandidateListEntry
ADD createdById UNIQUEIDENTIFIER NULL
GO

ALTER TABLE dbo.CandidateListEntry
ADD CONSTRAINT FK_CandidateListEntry_CreatedByEmployer
FOREIGN KEY (createdById) REFERENCES dbo.Employer ([id])
GO

ALTER TABLE dbo.CandidateListEntry
ADD ownedById UNIQUEIDENTIFIER NULL
GO
