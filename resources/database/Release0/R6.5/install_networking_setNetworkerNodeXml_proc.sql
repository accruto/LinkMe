IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'setNetworkerNodeXml' 
	   AND 	  type = 'P')
BEGIN
DROP PROCEDURE linkme_owner.setNetworkerNodeXml
END
GO


-- Set Network Node Xml For A single Networker
CREATE  procedure linkme_owner.setNetworkerNodeXml
	@networkerNodeId varchar(32),
	@updatedDate datetime,
	@networkerXml	text
AS
BEGIN
		
	DECLARE @count int

	select @count  = count(networkerId) FROM networkNodes WHERE networkerId = @networkerNodeId

	IF (@count > 0 )
	BEGIN
		update networkNodes
		set 
		networkerId = @networkerNodeId,
		nodeXml = @networkerXml, updatedDate = @updatedDate
		WHERE networkerId = @networkerNodeId
	END
	ELSE
	BEGIN
		INSERT INTO networkNodes VALUES (@networkerNodeId, @networkerXml, @updatedDate)

	END

	SELECT @networkerNodeId;
END
GO
