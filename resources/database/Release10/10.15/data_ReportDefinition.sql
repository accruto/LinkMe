UPDATE
	dbo.ReportDefinition
SET
	reportType = 'ResumeSearchActivityReport'
WHERE
	reportType = 'ClientActivityReport'