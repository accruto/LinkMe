-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiNamespaceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiNamespaceUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiNamespaceUpdate
(
	@fullName AS VARCHAR(512),
	@enabledEvents AS VARCHAR(512) = NULL,
	@mixedEvents AS VARCHAR(512) = NULL
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiNamespace
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Namespace doesn't exist - insert it. First ensure the parent exists.

	DECLARE @parentFullName AS VARCHAR(512)
	SET @parentFullName = dbo.FiGetParentNameFromFullName(@fullName)

	DECLARE @parentId AS UNIQUEIDENTIFIER
	EXEC dbo.FiNamespaceEnsure @parentFullName, @parentId OUTPUT

	INSERT INTO dbo.FiNamespace (id, fullName, parentId, enabledEvents, mixedEvents)
	VALUES (NEWID(), @fullName, @parentId, @enabledEvents, @mixedEvents)
END
ELSE
BEGIN
	-- Namespace exists - update it.

	UPDATE dbo.FiNamespace
	SET
		enabledEvents = @enabledEvents,
		mixedEvents = @mixedEvents
	WHERE id = @id
END

RETURN 0

END
GO
