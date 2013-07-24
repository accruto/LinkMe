
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetNextQueuedResumeData' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.GetNextQueuedResumeData
GO
