
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'UpdateQueuedResume' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.UpdateQueuedResume
GO

