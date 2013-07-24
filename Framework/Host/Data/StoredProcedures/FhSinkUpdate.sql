-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSinkUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSinkUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSinkUpdate
(
	@channelFullName AS VARCHAR(512),
	@name AS VARCHAR(128),
	@description AS NVARCHAR(512),
	@sinkType AS VARCHAR(128),
	@configurationData TEXT,
	@index INT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = S.id
FROM dbo.FhSink AS S
INNER JOIN dbo.FhChannel AS C ON C.id = S.channelId
WHERE C.fullName = @channelFullName AND S.name = @name

IF @id IS NULL
BEGIN
	-- Sink doesn't exist - insert it. First ensure the channel exists.

	DECLARE @channelId AS UNIQUEIDENTIFIER
	EXEC dbo.FhChannelEnsure @channelFullName, @channelId OUTPUT

	INSERT INTO dbo.FhSink (id, name, channelId, description, sinkType, configurationData, [index])
	VALUES (NEWID(), @name, @channelId, @description, @sinkType, @configurationData, @index)
END
ELSE
BEGIN
	-- Sink exists - update it.

	UPDATE dbo.FhSink
	SET
		description = @description,
		sinkType = @sinkType,
		configurationData = @configurationData,
		[index] = @index
	WHERE id = @id
END

RETURN 0

END
GO