-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhContainerDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhContainerDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhContainerDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT
DECLARE @containerId AS UNIQUEIDENTIFIER
SELECT @containerId = id FROM dbo.FhContainer WHERE fullName = @fullName

BEGIN TRANSACTION

-- Delete sinks.

DELETE FROM dbo.FhSink
FROM dbo.FhSink AS S
INNER JOIN dbo.FhChannel AS C ON C.id = S.channelId
WHERE C.containerId = @containerId

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete channels.

DELETE FROM dbo.FhChannel
WHERE containerId = @containerId

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete sources

DELETE FROM dbo.FhSource
WHERE containerId = @containerId

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete thread pools.

DELETE FROM dbo.FhThreadPool
WHERE containerId = @containerId

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete container.

DELETE FROM dbo.FhContainer
WHERE id = @containerId

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