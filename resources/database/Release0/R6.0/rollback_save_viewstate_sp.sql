
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'saveLinkMeViewState' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.saveLinkMeViewState
GO
