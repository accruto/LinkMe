-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhChannelUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhChannelUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhChannelUpdate
(
	@fullName AS VARCHAR(512),
	@description AS NVARCHAR(512),
	@threadPoolName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhChannel
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Channel doesn't exist - insert it. First ensure the namespace exists.

	DECLARE @containerFullName AS VARCHAR(512)
	SET @containerFullName = dbo.FhGetParentNameFromFullName(@fullName)

	DECLARE @containerId AS UNIQUEIDENTIFIER
	EXEC dbo.FhContainerEnsure @containerFullName, @containerId OUTPUT

	INSERT INTO dbo.FhChannel (id, fullName, containerId, description, threadPoolName)
	VALUES (NEWID(), @fullName, @containerId, @description, @threadPoolName)
END
ELSE
BEGIN
	-- Channel exists - update it.

	UPDATE dbo.FhChannel
	SET
		description = @description,
		threadPoolName = @threadPoolName
	WHERE id = @id
END

RETURN 0

END
GO