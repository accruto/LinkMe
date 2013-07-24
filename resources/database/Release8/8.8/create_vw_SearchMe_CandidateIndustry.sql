IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_SearchMe_CandidateIndustry]'))
	DROP VIEW [dbo].[vw_SearchMe_CandidateIndustry]
GO

CREATE VIEW dbo.vw_SearchMe_CandidateIndustry AS
	SELECT r.id, i.doc, 
		HashBytes('SHA1', i.doc) AS sha1sum, 
		r.lastEditedTime, 
		CASE WHEN doc IS NULL THEN 1 ELSE 0 END AS docIsNull
	FROM 
	(
		SELECT c.id, dbo.StrJoin(REPLACE(ci.industryId, '-', '')) AS doc FROM dbo.Candidate c
		INNER JOIN dbo.CandidateIndustry ci
		ON c.id = ci.candidateId
		GROUP BY c.id
	) AS i 
	INNER JOIN dbo.Resume AS r ON r.candidateId = i.id