IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ResumesViewedBySearcher]'))
DROP VIEW [dbo].[ResumesViewedBySearcher]
GO

CREATE VIEW dbo.ResumesViewedBySearcher (searcherId, resumeId, viewCount)
WITH SCHEMABINDING
AS
	-- Effectively a SELECT DISTINCT, but that isn't supported in indexed views for some
	-- strange reason, so you have to do GROUP BY and HAVE to have COUNT_BIG!
	SELECT ue.actorId, ar.resumeId, COUNT_BIG(*)
	FROM dbo.UserEvent ue
	INNER JOIN dbo.UserEventActionedResume ar
	ON ue.[id] = ar.eventId
	WHERE ue.[type] = 3
	GROUP BY ue.actorId, ar.resumeId
GO

CREATE UNIQUE CLUSTERED INDEX IX_ResumesViewedBySearcher
ON dbo.ResumesViewedBySearcher (searcherId, resumeId)
GO
