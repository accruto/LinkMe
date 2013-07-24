IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.CandidateDiary') AND type in (N'U'))
DROP TABLE dbo.CandidateDiary
GO

CREATE TABLE dbo.CandidateDiary
(
	id UNIQUEIDENTIFIER NOT NULL,
)

ALTER TABLE dbo.CandidateDiary
ADD CONSTRAINT PK_CandidateDiary PRIMARY KEY NONCLUSTERED
(
	id
)

