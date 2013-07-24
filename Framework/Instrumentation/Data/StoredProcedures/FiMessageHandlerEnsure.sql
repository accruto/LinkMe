-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlerEnsure]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageHandlerEnsure]
GO

-------------------------------------------------------------------------------
-- Create

-- Ensure that a namespace with the specified name exists and return its GUID.

CREATE PROCEDURE dbo.FiMessageHandlerEnsure
(
	@fullName AS VARCHAR(512),
	@id AS UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN

SET NOCOUNT ON

IF (@fullName = '')
BEGIN
	SET @id = NULL
	RETURN 0
END

SELECT @id = id
FROM dbo.FiMessageHandler
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- MessageHandler doesn't exist - create it.

	DECLARE @parentFullName VARCHAR(512)
	DECLARE @parentId AS UNIQUEIDENTIFIER

	SET @parentFullName = dbo.FiGetParentNameFromFullName(@fullName)

	-- Get the parent GUID, creating the parent if it doesn't already exist.

	IF (@parentFullName = '')
		SET @parentId = NULL -- This is a root.
	ELSE
		EXEC dbo.FiMessageHandlerEnsure @parentFullName, @parentId OUTPUT

	-- Insert this message handler.

	SET @id = NEWID()

	INSERT INTO dbo.FiMessageHandler (id, fullName, parentId)
	VALUES (@id, @fullName, @parentId)
END

RETURN 0

END
GO
