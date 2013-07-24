


IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'getNetworkerNodeXml' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.getNetworkerNodeXml
END
GO
