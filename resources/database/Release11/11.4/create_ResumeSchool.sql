IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.ResumeSchool') AND type in (N'U'))
DROP TABLE dbo.ResumeSchool
GO

CREATE TABLE dbo.ResumeSchool
(
	id UNIQUEIDENTIFIER NOT NULL,
	resumeId UNIQUEIDENTIFIER NOT NULL,
	institution TEXT NULL,
	description TEXT NULL,
	degree TEXT NULL,
	major TEXT NULL,
	city TEXT NULL,
	country TEXT NULL,
	endDate DATETIME NULL,
	endDateParts TINYINT NULL,
	isCurrent BIT NOT NULL,
)

ALTER TABLE dbo.ResumeSchool
ADD CONSTRAINT PK_ResumeSchool PRIMARY KEY NONCLUSTERED
(
	id
)
GO

CREATE CLUSTERED INDEX IX_ResumeSchool_resumeId ON dbo.ResumeSchool
(
	resumeId
)
GO

ALTER TABLE dbo.ResumeSchool
ADD CONSTRAINT FK_ResumeSchool_Resume FOREIGN KEY (resumeId)
REFERENCES dbo.Resume (id)
GO

