DECLARE @jobName NVARCHAR(200)
SET @jobName = N'Delete old ViewState'

IF (EXISTS(SELECT * FROM msdb.dbo.sysjobs WHERE [name] = @jobName))
	EXEC msdb.dbo.sp_delete_job @job_name = @jobName
GO
