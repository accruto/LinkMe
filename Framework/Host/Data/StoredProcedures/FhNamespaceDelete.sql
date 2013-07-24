-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhNamespaceDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhNamespaceDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhNamespaceDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT
DECLARE @namespacePattern AS VARCHAR(514)
SET @namespacePattern = @fullName + '.%'

BEGIN TRANSACTION

-- Delete thread pools.

DELETE FROM dbo.FhThreadPool
WHERE fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete sinks.

DELETE FROM dbo.FhSink
FROM dbo.FhSink AS S
INNER JOIN dbo.FhChannel AS C ON C.id = S.channelId
WHERE C.fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete channels.

DELETE FROM dbo.FhChannel
WHERE fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete sources.

DELETE FROM dbo.FhSource
WHERE fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete containers.

DELETE FROM dbo.FhContainer
WHERE fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete child namespaces.

DELETE FROM dbo.FhNamespace
WHERE fullName LIKE @namespacePattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete this namespace.

DELETE FROM dbo.FhNamespace
WHERE fullName = @fullName

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

COMMIT TRANSACTION
RETURN 0

END
GO