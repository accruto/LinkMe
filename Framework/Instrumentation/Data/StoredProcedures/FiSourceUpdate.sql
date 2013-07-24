-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSourceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiSourceUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiSourceUpdate
(
	@fullyQualifiedReference AS VARCHAR(512),
	@enabledEvents AS NVARCHAR(128) = NULL,
	@sourceType AS NVARCHAR(128) = NULL
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the component.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiSource
WHERE fullyQualifiedReference = @fullyQualifiedReference

IF @id IS NULL
BEGIN
	-- Container doesn't exist - insert it. First ensure the namespace exists.

	DECLARE @namespaceFullName AS VARCHAR(512)
	SET @namespaceFullName = dbo.FiGetParentNameFromFullyQualifiedReference(@fullyQualifiedReference)

	DECLARE @namespaceId AS UNIQUEIDENTIFIER
	EXEC dbo.FiNamespaceEnsure @namespaceFullName, @namespaceId OUTPUT

	INSERT INTO dbo.FiSource (id, fullyQualifiedReference, namespaceId, enabledEvents, sourceType)
	VALUES (NEWID(), @fullyQualifiedReference, @namespaceId, @enabledEvents, @sourceType)
END
ELSE
BEGIN
	-- Source exists - update it.

	UPDATE dbo.FiSource
	SET
		enabledEvents = @enabledEvents,
		sourceType = @sourceType
	WHERE id = @id
END

RETURN 0

END
GO