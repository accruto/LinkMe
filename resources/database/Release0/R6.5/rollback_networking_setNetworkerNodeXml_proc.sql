IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'setNetworkerNodeXml' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.setNetworkerNodeXml
END
GO
