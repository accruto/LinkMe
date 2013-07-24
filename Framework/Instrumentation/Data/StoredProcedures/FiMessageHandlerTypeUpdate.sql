-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageReaderTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageReaderTypeUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageReaderTypeUpdate
(
	@name AS VARCHAR(128),
	@displayName AS VARCHAR(128),
	@class VARCHAR(512),
	@configurationHandlerClass VARCHAR(512),
	@configurationPropertyPageClass VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the type.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiMessageReaderType
WHERE name = @name

IF @id IS NULL
BEGIN
	-- MessageReaderType doesn't exist - insert it.

	INSERT INTO dbo.FiMessageReaderType (id, name, displayName, class, configurationHandlerClass, configurationPropertyPageClass)
	VALUES (NEWID(), @name, @displayName, @class, @configurationHandlerClass, @configurationPropertyPageClass)
END
ELSE
BEGIN
	-- MessageReaderType exists - update it.

	UPDATE dbo.FiMessageReaderType
	SET
		displayName = @displayName,
		class = @class,
		configurationHandlerClass = @configurationHandlerClass,
		configurationPropertyPageClass = @configurationPropertyPageClass
	WHERE id = @id
END

RETURN 0

END
GO