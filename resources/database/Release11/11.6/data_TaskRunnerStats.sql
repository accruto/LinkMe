UPDATE
	dbo.TaskRunnerStats
SET
	task = 'EmailResumeSearchAlertsTask'
WHERE
	task = 'ResumeSearchAlertsTask'