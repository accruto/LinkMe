ALTER VIEW [dbo].[vw_SearchMe_CandidateCommunity] AS
	SELECT r.id, i.doc, 
		HashBytes('SHA1', i.doc) AS sha1sum, 
		0 AS docIsNull
	FROM 
	(
		SELECT c.id, REPLACE(cm.primaryCommunityId, '-', '') AS doc 
		FROM dbo.Candidate c
		INNER JOIN dbo.CommunityMember cm ON c.id = cm.id
	) AS i 
	INNER JOIN dbo.Resume AS r ON r.candidateId = i.id