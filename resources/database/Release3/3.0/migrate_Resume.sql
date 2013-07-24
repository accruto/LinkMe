INSERT INTO dbo.Resume([id], version, lastEditedTime, lensXml, candidateId, parsedFromFileId)
SELECT dbo.GuidFromString(rd.[Id]), 1, NULLIF(resumeLastUpdatedDate, joinDate) AS lastEditedTime,
	resumeXml, dbo.GuidFromString(rd.[Id]), NULL
FROM linkme_owner.networker_resume_data rd
INNER JOIN linkme_owner.networker_profile np
ON rd.[Id] = np.[id]
INNER JOIN linkme_owner.user_profile up
ON np.[Id] = up.[id]

GO

UPDATE dbo.Resume
SET lensXml = NULL
WHERE lensXml LIKE ''

GO
