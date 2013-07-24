-- Resume

ALTER TABLE dbo.Resume
ADD isEmpty
AS
	CASE WHEN
		courses IS NOT NULL
		OR awards IS NOT NULL
		OR skills IS NOT NULL
		OR objective IS NOT NULL
		OR summary IS NOT NULL
		OR other IS NOT NULL
		OR citizenship IS NOT NULL
		OR affiliations IS NOT NULL
		OR professional IS NOT NULL
		OR interests IS NOT NULL
		OR referees IS NOT NULL
	THEN
		0
	ELSE
		1
	END
PERSISTED
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Resume]') AND name = N'IX_Resume_id_isEmpty')
DROP INDEX [IX_Resume_id_isEmpty] ON [dbo].[Resume]
GO

CREATE NONCLUSTERED INDEX [IX_Resume_id_isEmpty] ON [dbo].[Resume] 
(
	id,
	isEmpty
)
GO

-- ResumeJob

ALTER TABLE dbo.ResumeJob
ADD isEmpty
AS
	-- Assume that if the row exists it is not empty.

	0
PERSISTED
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResumeJob]') AND name = N'IX_ResumeJob_resumeId_isEmpty')
DROP INDEX [IX_ResumeJob_resumeId_isEmpty] ON [dbo].[ResumeJob]
GO

CREATE NONCLUSTERED INDEX [IX_ResumeJob_resumeId_isEmpty] ON [dbo].[ResumeJob] 
(
	resumeId,
	isEmpty
)
GO

-- ResumeSchool

ALTER TABLE dbo.ResumeSchool
ADD isEmpty
AS
	-- Assume that if the row exists it is not empty.

	0
PERSISTED
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ResumeSchool]') AND name = N'IX_ResumeSchool_resumeId_isEmpty')
DROP INDEX [IX_ResumeSchool_resumeId_isEmpty] ON [dbo].[ResumeSchool]
GO

CREATE NONCLUSTERED INDEX [IX_ResumeSchool_resumeId_isEmpty] ON [dbo].[ResumeSchool] 
(
	resumeId,
	isEmpty
)
GO
