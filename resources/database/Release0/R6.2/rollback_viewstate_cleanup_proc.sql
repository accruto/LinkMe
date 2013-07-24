IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'clearExpiredViewStates' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.clearExpiredViewStates
GO