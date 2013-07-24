-- creating the store procedure
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = N'getLinkMeViewState' 
	   AND 	  type = 'P')
DROP PROCEDURE  linkme_owner.getLinkMeViewState
GO

