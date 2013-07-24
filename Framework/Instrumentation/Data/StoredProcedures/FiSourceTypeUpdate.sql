-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSourceTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiSourceTypeUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiSourceTypeUpdate
(
	@name AS VARCHAR(128),
	@availableEvents AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the type.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiSourceType
WHERE name = @name

IF @id IS NULL
BEGIN
	-- SourceType doesn't exist - insert it.

	INSERT INTO dbo.FiSourceType (id, name, availableEvents)
	VALUES (NEWID(), @name, @availableEvents)
END
ELSE
BEGIN
	-- SourceType exists - update it.

	UPDATE dbo.FiSourceType
	SET
		availableEvents = @availableEvents
	WHERE id = @id
END

RETURN 0

END
GO