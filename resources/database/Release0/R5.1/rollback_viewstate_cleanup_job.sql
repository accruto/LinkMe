DECLARE @JobID BINARY(16) 

  -- Delete the job with the same name (if it exists)
  SELECT @JobID = job_id     
  FROM   msdb.dbo.sysjobs    
  WHERE (name = N'LinkMe_Viewstate_Cleanup')    

  IF (@JobID IS NOT NULL)    
  BEGIN  
    -- Delete the [local] job 
    EXECUTE msdb.dbo.sp_delete_job @job_name = N'LinkMe_Viewstate_Cleanup' 
    SELECT @JobID = NULL
  END 