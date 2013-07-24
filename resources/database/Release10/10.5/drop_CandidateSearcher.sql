ALTER TABLE [dbo].[Employer] DROP CONSTRAINT [FK_Employer_CandidateSearcher]
GO

ALTER TABLE [dbo].[ResumeSearch] DROP CONSTRAINT [FK_ResumeSearch_CandidateSearcher]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CandidateSearcher]') AND type in (N'U'))
DROP TABLE [dbo].[CandidateSearcher]
GO
