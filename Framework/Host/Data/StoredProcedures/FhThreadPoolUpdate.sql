-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhThreadPoolUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhThreadPoolUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhThreadPoolUpdate
(
	@fullName AS VARCHAR(512),
	@description AS NVARCHAR(512),
	@threadPoolType AS VARCHAR(128),
	@configurationData TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhThreadPool
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- ThreadPool doesn't exist - insert it. First ensure the namespace exists.

	DECLARE @containerFullName AS VARCHAR(512)
	SET @containerFullName = dbo.FhGetParentNameFromFullName(@fullName)

	DECLARE @containerId AS UNIQUEIDENTIFIER
	EXEC dbo.FhContainerEnsure @containerFullName, @containerId OUTPUT

	INSERT INTO dbo.FhThreadPool (id, fullName, containerId, description, threadPoolType, configurationData)
	VALUES (NEWID(), @fullName, @containerId, @description, @threadPoolType, @configurationData)
END
ELSE
BEGIN
	-- ThreadPool exists - update it.

	UPDATE dbo.FhThreadPool
	SET
		description = @description,
		threadPoolType = @threadPoolType,
		configurationData = @configurationData
	WHERE id = @id
END

RETURN 0

END
GO