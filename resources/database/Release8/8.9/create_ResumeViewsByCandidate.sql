IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ResumeViewsByCandidate]'))
DROP VIEW [dbo].[ResumeViewsByCandidate]
GO

CREATE VIEW dbo.ResumeViewsByCandidate (candidateId, resumeViews)
WITH SCHEMABINDING
AS
	SELECT r.candidateId, COUNT_BIG(*)
	FROM dbo.UserEvent ue
	INNER JOIN dbo.UserEventActionedResume ar
	ON ue.[id] = ar.eventId
	INNER JOIN dbo.Resume r
	ON ar.resumeId = r.[id]
	WHERE ue.[type] = 3
	GROUP BY r.candidateId
GO

CREATE UNIQUE CLUSTERED INDEX IX_ResumeViewsByCandidate
ON dbo.ResumeViewsByCandidate (candidateId)
GO
