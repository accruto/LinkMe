-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateVariable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateVariable
(
	@name AS VARCHAR(128),
	@description AS VARCHAR(512),
	@type AS VARCHAR(128),
	@value AS TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcVariable WHERE name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcVariable (name, description, type, value)
	VALUES (@name, @description, @type, @value)
END
ELSE
BEGIN
	-- Variable exists - update it.

	UPDATE dbo.FcVariable
	SET
		description = @description,
		type = @type,
		value = @value
	WHERE name = @name
END

RETURN 0

END
GO
