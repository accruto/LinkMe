IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CleanUpCompletedResumes' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.CleanUpCompletedResumes
END
GO