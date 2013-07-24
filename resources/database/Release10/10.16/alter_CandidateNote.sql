-- If no createdTime but there is a lastUpdatedTime make them equal

UPDATE
	dbo.CandidateNote
SET
	createdTime = lastUpdatedTime
WHERE
	createdTime IS NULL AND lastUpdatedTime IS NOT NULL
GO

-- If no createdTime and no last updated time pick the maximum of when either the employer or member joined.

UPDATE
	CandidateNote
SET
	createdTime =
	(
		SELECT
			CASE WHEN ue.createdTime > um.createdTime THEN ue.createdTime ELSE um.createdTime END
		FROM
			dbo.CandidateNote AS n
			INNER JOIN dbo.RegisteredUser AS ue ON ue.id = n.searcherId
			INNER JOIN dbo.RegisteredUser AS um ON um.id = n.candidateId
		WHERE
			n.id = CandidateNote.id
	)
WHERE
	createdTime IS NULL
GO

UPDATE
	CandidateNote
SET
	lastUpdatedTime = createdTime
WHERE
	lastUpdatedTime IS NULL
GO

-- Make both NOT NULL

ALTER TABLE dbo.CandidateNote
ALTER COLUMN createdTime DATETIME NOT NULL
GO

ALTER TABLE dbo.CandidateNote
ALTER COLUMN lastUpdatedTime DATETIME NOT NULL
GO
