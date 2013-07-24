UPDATE dbo.JobAd
SET integratorReferenceId = externalReferenceId
FROM dbo.JobAd ja
INNER JOIN dbo.IntegratorUser iu
ON ja.integratorUserId = iu.[id]
WHERE ja.integratorReferenceId IS NULL AND iu.username = 'JobG8'
