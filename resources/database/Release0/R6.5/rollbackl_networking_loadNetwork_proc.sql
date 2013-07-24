
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'loadNetwork' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.loadNetwork
END
GO