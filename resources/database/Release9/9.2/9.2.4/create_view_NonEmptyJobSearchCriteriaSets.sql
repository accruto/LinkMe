IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[NonEmptyJobSearchCriteriaSets]'))
DROP VIEW [dbo].[NonEmptyJobSearchCriteriaSets]
GO

CREATE VIEW dbo.NonEmptyJobSearchCriteriaSets
WITH SCHEMABINDING
AS
-- We don't actually need the COUNT at all - it's just a way to simulate DISTINCT, which allows
-- an indexed view.
SELECT setId, COUNT_BIG(*) AS criteriaCount
FROM dbo.JobSearchCriteria jsc
WHERE [name] <> 'SortOrder'
GROUP BY setId
GO

CREATE UNIQUE CLUSTERED INDEX IX_NonEmptyJobSearchCriteriaSets
ON dbo.NonEmptyJobSearchCriteriaSets (setId)
GO
