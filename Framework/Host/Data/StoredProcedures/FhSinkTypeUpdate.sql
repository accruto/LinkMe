-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSinkTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSinkTypeUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSinkTypeUpdate
(
	@name AS VARCHAR(128),
	@class VARCHAR(512),
	@configurationHandlerClass VARCHAR(512),
	@configurationPropertyPageClass VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the thread pool type.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhSinkType
WHERE name = @name

IF @id IS NULL
BEGIN
	-- SinkType doesn't exist - insert it.

	INSERT INTO dbo.FhSinkType (id, name, class, configurationHandlerClass, configurationPropertyPageClass)
	VALUES (NEWID(), @name, @class, @configurationHandlerClass, @configurationPropertyPageClass)
END
ELSE
BEGIN
	-- SinkType exists - update it.

	UPDATE dbo.FhSinkType
	SET
		class = @class,
		configurationHandlerClass = @configurationHandlerClass,
		configurationPropertyPageClass = @configurationPropertyPageClass
	WHERE id = @id
END

RETURN 0

END
GO