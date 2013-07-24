UPDATE
	dbo.JobAd
SET
	jobTypes = 1
WHERE
	jobTypes = 0
	AND status = 2
	AND integratorUserId = '3BAE70FC-350B-4778-953A-05226170602B'
