SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

DROP PROCEDURE linkme_owner.CleanUpCompletedResumes
GO


CREATE PROCEDURE linkme_owner.CleanUpCompletedResumes
	@DAYSBEFOREEXPIRE INT
AS

BEGIN
	IF (@DAYSBEFOREEXPIRE > 0) 
	BEGIN
		DELETE FROM linkme_owner.resume_queue
		WHERE 
		DATEDIFF ( dd , submitDate , GETDATE() )  > @DAYSBEFOREEXPIRE
	END
	ELSE
	BEGIN
		RAISERROR ('Days Before Expire Parameter Must be greater than zero.',16,1)
	END
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

