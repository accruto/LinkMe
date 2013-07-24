SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetNextQueuedResumeData' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.GetNextQueuedResumeData
GO


CREATE PROCEDURE linkme_owner.GetNextQueuedResumeData
	@QUEUE_NAME varchar(50),
	@LOCKED_BY  varchar(50)
AS

BEGIN
	
-- SELECT THE RELEVANT ROWS

	UPDATE  
		linkme_owner.resume_queue 
	SET 
		lockedBy = @LOCKED_BY 
	WHERE 	
		status = 0 -- pending
	AND
		lensQueueName = @QUEUE_NAME
	AND
		lockedBy IS NULL 



	select 
		 queue.id, queue.networkerId, queue.status, queue.submitDate, queue.attempts, queue.nonCriticalFailures, resumeData.resumeXml
	FROM 	
		linkme_owner.resume_queue queue WITH (XLOCK, READPAST)
	INNER 
		JOIN linkme_owner.networker_resume_data resumeData
	ON 
		queue.networkerId = resumeData.Id
	WHERE 	
		lockedBy = @LOCKED_BY
	ORDER BY 
		queue.submitDate ASC


END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

