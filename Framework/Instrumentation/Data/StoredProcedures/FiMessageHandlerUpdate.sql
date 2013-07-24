-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlerUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageHandlerUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageHandlerUpdate
(
	@fullName AS VARCHAR(512),
	@messageHandlerType AS VARCHAR(128),
	@configurationData TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiMessageHandler
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Message handler doesn't exist - insert it. First ensure the parent exists.

	DECLARE @parentFullName AS VARCHAR(512)
	SET @parentFullName = dbo.FiGetParentNameFromFullName(@fullName)

	DECLARE @parentId AS UNIQUEIDENTIFIER
	EXEC dbo.FiMessageHandlerEnsure @parentFullName, @parentId OUTPUT

	INSERT INTO dbo.FiMessageHandler (id, fullName, parentId, messageHandlerType, configurationData)
	VALUES (NEWID(), @fullName, @parentId, @messageHandlerType, @configurationData)
END
ELSE
BEGIN
	-- MessageHandler exists - update it.

	UPDATE dbo.FiMessageHandler
	SET
		messageHandlerType = @messageHandlerType,
		configurationData = @configurationData
	WHERE id = @id
END

RETURN 0

END
GO