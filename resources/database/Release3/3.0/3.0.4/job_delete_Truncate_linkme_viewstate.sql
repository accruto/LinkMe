-- Delete the old "Truncate linkme_viewstate" job, which is no longer required.

DECLARE @JobID BINARY(16)  

SELECT @JobID = job_id     
FROM   msdb.dbo.sysjobs    
WHERE (name = N'Truncate linkme_viewstate')       
IF (@JobID IS NOT NULL)    
BEGIN  
-- Check if the job is a multi-server job  
IF (EXISTS (SELECT  * 
      FROM    msdb.dbo.sysjobservers 
      WHERE   (job_id = @JobID) AND (server_id <> 0))) 
BEGIN 
-- There is, so abort the script 
RAISERROR (N'Unable to import job ''Truncate linkme_viewstate'' since there is already a multi-server job with this name.', 16, 1) 
END 
ELSE 
-- Delete the [local] job 
EXECUTE msdb.dbo.sp_delete_job @job_name = N'Truncate linkme_viewstate' 
SELECT @JobID = NULL
END 
