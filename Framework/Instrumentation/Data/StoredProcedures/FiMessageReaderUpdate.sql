-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageReaderUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageReaderUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageReaderUpdate
(
	@name AS VARCHAR(512),
	@messageReaderType AS VARCHAR(128),
	@configurationData TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiMessageReader
WHERE name = @name

IF @id IS NULL
BEGIN
	INSERT INTO dbo.FiMessageReader (id, name, messageReaderType, configurationData)
	VALUES (NEWID(), @name, @messageReaderType, @configurationData)
END
ELSE
BEGIN
	-- MessageReader exists - update it.

	UPDATE dbo.FiMessageReader
	SET
		messageReaderType = @messageReaderType,
		configurationData = @configurationData
	WHERE id = @id
END

RETURN 0

END
GO