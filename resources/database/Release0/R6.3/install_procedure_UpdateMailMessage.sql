SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

DROP PROCEDURE linkme_owner.UpdateMailMessage
GO

CREATE PROCEDURE linkme_owner.UpdateMailMessage
	@MESSAGE_ID  varchar(50),
	@MESSAGE_STATUS	varchar(20),
	@DATE_SENT	datetime,
	@ERROR_COUNT	int
AS

BEGIN
	
-- SELECT THE RELEVANT ROWS

	UPDATE  
		linkme_owner.linkme_mail_queue
	SET 
		lockedBy = NULL,
		dateSent = @DATE_SENT,
		messageStatus = @MESSAGE_STATUS,
		errorCount = @ERROR_COUNT
	WHERE 	
		[ID] = @MESSAGE_ID

END



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

