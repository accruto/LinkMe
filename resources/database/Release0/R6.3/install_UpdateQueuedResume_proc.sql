SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'UpdateQueuedResume' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.UpdateQueuedResume
GO


CREATE PROCEDURE linkme_owner.UpdateQueuedResume
	@QUEUED_RESUME_ID VARCHAR(50),
	@ATTEMPTS INT,
	@NCFAILS INT,
	@STATUS INT,
	@SUBMIT_DATE DATETIME
	
AS

BEGIN
	

	UPDATE 
		linkme_owner.resume_queue
	SET 
		status = @STATUS,
		submitDate = @SUBMIT_DATE,
		attempts = @ATTEMPTS,
		nonCriticalFailures = @NCFAILS ,
		lockedBy = NULL
	WHERE
		ID = @QUEUED_RESUME_ID


END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

