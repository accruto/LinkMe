
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CleanUpCompletedResumes' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.CleanUpCompletedResumes
END
GO


CREATE PROCEDURE linkme_owner.CleanUpCompletedResumes
	@DAYSBEFOREEXPIRE INT
AS

BEGIN
	IF (@DAYSBEFOREEXPIRE > 0) 
	BEGIN
		DELETE FROM linkme_owner.resume_queue
		WHERE 
		DATEDIFF ( d , submitDate , GETDATE() )  > @DAYSBEFOREEXPIRE
		AND STATUS = 1 -- Success ONLY
	END
	ELSE
	BEGIN
		RAISERROR ('Days Before Expire Parameter Must be greater than zero.',16,1)
	END
END
GO
