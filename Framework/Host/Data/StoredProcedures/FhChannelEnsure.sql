-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhChannelEnsure]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhChannelEnsure]
GO

-------------------------------------------------------------------------------
-- Create

-- Ensure that a channel with the specified name exists and return its GUID.

CREATE PROCEDURE dbo.FhChannelEnsure
(
	@fullName AS VARCHAR(512),
	@id AS UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN

SET NOCOUNT ON

SELECT @id = id
FROM dbo.FhChannel
WHERE fullName = @fullName

IF @id IS NULL
BEGIN
	-- Channel doesn't exist - create it.

	DECLARE @parentFullName VARCHAR(512)
	DECLARE @containerId AS UNIQUEIDENTIFIER

	SET @parentFullName = dbo.FhGetParentNameFromFullName(@fullName)

	-- Get the parent container GUID, creating the parent container if it doesn't already exist.

	EXEC dbo.FhContainerEnsure @parentFullName, @containerId OUTPUT

	-- Insert this channel

	SET @id = NEWID()

	INSERT INTO dbo.FhChannel (id, fullName, containerId)
	VALUES (@id, @fullName, @containerId)
END

RETURN 0

END
GO
