IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CandidateDiaryEntry') AND type in (N'U'))
DROP TABLE dbo.CandidateDiaryEntry
GO

CREATE TABLE dbo.CandidateDiaryEntry
(
	id UNIQUEIDENTIFIER NOT NULL,
	diaryId UNIQUEIDENTIFIER NOT NULL,
	deleted BIT NOT NULL DEFAULT(0),
	title NVARCHAR(512) NULL,
	description NVARCHAR(2048) NULL,
	startTime DATETIME NULL,
	endTime DATETIME NULL,
	totalHours FLOAT NULL
)

ALTER TABLE dbo.CandidateDiaryEntry
ADD CONSTRAINT PK_CandidateDiaryEntry PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE CLUSTERED INDEX IX_CandidateDiaryEntry_diaryId ON dbo.CandidateDiaryEntry
(
	diaryId
)
GO

ALTER TABLE dbo.CandidateDiaryEntry
ADD CONSTRAINT FK_CandidateDiaryEntry_CandidateDiary FOREIGN KEY (diaryId)
REFERENCES dbo.[CandidateDiary] (id)
GO


