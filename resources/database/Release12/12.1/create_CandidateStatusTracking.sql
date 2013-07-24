/****** Object:  Table [dbo].[CandidateStatusTracking]    Script Date: 10/28/2010 10:12:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CandidateStatusTracking]') AND type in (N'U'))
DROP TABLE [dbo].[CandidateStatusTracking]

CREATE TABLE dbo.CandidateStatusTracking
(
	date DATETIME NOT NULL,
	candidateId UNIQUEIDENTIFIER NOT NULL,
	status CandidateStatus NOT NULL
)

ALTER TABLE dbo.CandidateStatusTracking ADD CONSTRAINT PK_CandidateStatusTracking
PRIMARY KEY CLUSTERED (date, candidateId)

