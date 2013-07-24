
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'loadNetworkGraph' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.LoadNetworkGraph
END
GO