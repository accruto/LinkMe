
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'deleteNetworkNode' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.deleteNetworkNode
END
GO
