IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'CleanUpNetworkerOverlookedSummaries' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.CleanUpNetworkerOverlookedSummaries
END
GO