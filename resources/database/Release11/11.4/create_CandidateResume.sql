IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CandidateResume') AND type in (N'U'))
DROP TABLE dbo.CandidateResume
GO

CREATE TABLE dbo.CandidateResume
(
	candidateId UNIQUEIDENTIFIER NOT NULL,
	resumeId UNIQUEIDENTIFIER NOT NULL,
	parsedFromFileId UNIQUEIDENTIFIER NULL
)

ALTER TABLE dbo.CandidateResume
ADD CONSTRAINT PK_CandidateResume PRIMARY KEY CLUSTERED
(
	candidateId,
	resumeId
)

ALTER TABLE dbo.CandidateResume
ADD CONSTRAINT FK_CandidateResume_Candidate FOREIGN KEY (candidateId)
REFERENCES dbo.Candidate (id)
GO

ALTER TABLE dbo.CandidateResume
ADD CONSTRAINT FK_CandidateResume_Resume FOREIGN KEY (resumeId)
REFERENCES dbo.Resume (id)
GO

ALTER TABLE dbo.CandidateResume
ADD CONSTRAINT FK_CandidateResume_CandidateResumeFile FOREIGN KEY (parsedFromFileId)
REFERENCES dbo.CandidateResumeFile (id)
GO
