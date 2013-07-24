SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

DROP PROCEDURE linkme_owner.GetMailQueueBatch
GO


CREATE PROCEDURE linkme_owner.GetMailQueueBatch
	@LOCKED_BY  varchar(50),
	@BATCH_SIZE int
AS

BEGIN
	
-- SELECT THE RELEVANT ROWS
	

CREATE TABLE #TMP_MAILIDS 
(
	MAILID VARCHAR(100)
)
	SET ROWCOUNT @BATCH_SIZE
	
	
	INSERT INTO #TMP_MAILIDS
		SELECT mailqueue.[Id] FROM 
		linkme_owner.linkme_mail_queue mailqueue WITH (XLOCK, READPAST)
	WHERE 
		messageStatus != 'SentSuccess'
	AND
		lockedBy IS NULL 
	ORDER BY 
		mailqueue.dateSent ASC
	
SET ROWCOUNT 0
	
	UPDATE  
		linkme_owner.linkme_mail_queue
	SET 
		lockedBy = @LOCKED_BY 
	WHERE 	
		[id] IN (SELECT MAILID FROM #TMP_MAILIDS)



	select 
		 mailqueue.[Id], mailqueue.toAddress, mailqueue.fromAddress, mailqueue.subject, 
		mailqueue.bodyFormat, mailqueue.body, mailqueue.errorCount
	FROM 	
		linkme_owner.linkme_mail_queue mailqueue WITH (XLOCK, READPAST)
	WHERE 	
		lockedBy = @LOCKED_BY
	
		
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

