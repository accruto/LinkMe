
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'loadNetwork' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.loadNetwork
END
GO

CREATE PROCEDURE linkme_owner.loadNetwork
AS

BEGIN

	SELECT networkerId, nodeXml, updatedDate FROM linkme_owner.networkNodes
END

GO