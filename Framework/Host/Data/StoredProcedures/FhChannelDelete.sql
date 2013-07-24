-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhChannelDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhChannelDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhChannelDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT
DECLARE @channelId AS UNIQUEIDENTIFIER
SELECT @channelId = id FROM dbo.FhChannel WHERE fullName = @fullName

BEGIN TRANSACTION

-- Delete sinks.

DELETE FROM dbo.FhSink
WHERE channelId = @channelId

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete channel.

DELETE FROM dbo.FhChannel
WHERE id = @channelId

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