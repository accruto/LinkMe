UPDATE JobAd
SET candidateListId = NULL
FROM JobAd ja
WHERE NOT EXISTS
(
	SELECT *
	FROM CandidateListEntry cle
	WHERE cle.candidateListId = ja.candidateListId
)
GO

DELETE CandidateList
FROM CandidateList cl
WHERE NOT EXISTS
(
	SELECT *
	FROM CandidateListEntry cle
	WHERE cle.candidateListId = cl.[id]
)
GO
