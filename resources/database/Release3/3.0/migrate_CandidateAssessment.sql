INSERT INTO dbo.CandidateAssessment([id], createdTime, externalId, externalSecret, candidateId)
SELECT dbo.GuidFromString([id]), created, assessmeId,
	SUBSTRING(reportLink, CHARINDEX('ppt=', reportLink) + 4, 32) AS externalSecret,
	dbo.GuidFromString(networkerId)
FROM linkme_owner.networker_assessment
GO
