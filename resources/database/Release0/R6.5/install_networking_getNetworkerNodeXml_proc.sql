


IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'getNetworkerNodeXml' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.getNetworkerNodeXml
END
GO


-- Get Network Node Xml For A single Networker
CREATE  procedure linkme_owner.getNetworkerNodeXml
	@networkerNodeId varchar(32)
AS
BEGIN

	select networkerId, nodeXml, updatedDate FROM networkNodes WHERE networkerId = @networkerNodeId

END
GO