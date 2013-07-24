
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'deleteNetworkNode' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.deleteNetworkNode
END
GO

CREATE PROCEDURE linkme_owner.deleteNetworkNode
	@networkNodeId varchar(32)
AS

BEGIN

	DELETE FROM linkme_owner.networkNodes WHERE networkerId = @networkNodeId
END

GO
