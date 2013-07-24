-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSourceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSourceUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSourceUpdate
(
	@fullName AS VARCHAR(512),
	@description AS NVARCHAR(512),
	@sourceType AS VARCHAR(128),
	@configurationData TEXT,
	@channelName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhSource
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Source doesn't exist - insert it. First ensure the namespace exists.

	DECLARE @containerFullName AS VARCHAR(512)
	SET @containerFullName = dbo.FhGetParentNameFromFullName(@fullName)

	DECLARE @containerId AS UNIQUEIDENTIFIER
	EXEC dbo.FhContainerEnsure @containerFullName, @containerId OUTPUT

	INSERT INTO dbo.FhSource (id, fullName, containerId, description, sourceType, configurationData, channelName)
	VALUES (NEWID(), @fullName, @containerId, @description, @sourceType, @configurationData, @channelName)
END
ELSE
BEGIN
	-- Source exists - update it.

	UPDATE dbo.FhSource
	SET
		description = @description,
		sourceType = @sourceType,
		configurationData = @configurationData,
		channelName = @channelName
	WHERE id = @id
END

RETURN 0

END
GO