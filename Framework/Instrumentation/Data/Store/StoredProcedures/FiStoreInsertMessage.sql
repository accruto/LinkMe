-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiStoreInsertMessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiStoreInsertMessage]
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiStoreInsertMessage
(
	@time BIGINT,
	@sequence INT,
	@source VARCHAR(512),
	@event VARCHAR(256),
	@type VARCHAR(256),
	@method VARCHAR(256),
	@message NTEXT,
	@exception NTEXT,
	@details NTEXT,
	@messageId INT OUTPUT
)
AS

SET NOCOUNT ON

DECLARE @error INT
DECLARE @sourceId INT

-- Find or create the source id.

SELECT
	@sourceId = id
FROM
	dbo.FiStoreSource
WHERE
	fullName = @source

IF @sourceId IS NULL
BEGIN
	INSERT
		dbo.FiStoreSource (fullName)
	VALUES
		(@source)

	SET @error = @@ERROR

	IF (@error = 0)
	BEGIN
		SET @sourceId = @@IDENTITY
	END
	ELSE IF (@error = 2601)
	BEGIN
		-- Another connection inserted this row - select it again.

		SELECT
			@sourceId = id
		FROM
			dbo.FiStoreSource
		WHERE
			fullName = @source
	END
	ELSE
	BEGIN
		RAISERROR( 'Failed to get or create an entry for Source ''%s''.', 16, 1, @source)
		RETURN 1
	END
END

-- Insert the message.

INSERT
	dbo.FiStoreMessage ([time], sequence, sourceId, [event], [type], method, [message], exception, details)
VALUES
	(@time, @sequence, @sourceId, @event, @type, @method, @message, @exception, @details)

SET @messageId = @@IDENTITY

RETURN 0
GO
