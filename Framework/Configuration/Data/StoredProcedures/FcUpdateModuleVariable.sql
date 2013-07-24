-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateModuleVariable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateModuleVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateModuleVariable
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@description AS VARCHAR(512),
	@type AS VARCHAR(128),
	@value AS TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcModuleVariable WHERE moduleName = @moduleName AND name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcModuleVariable (moduleName, name, description, type, value)
	VALUES (@moduleName, @name, @description, @type, @value)
END
ELSE
BEGIN
	-- ModuleVariable exists - update it.

	UPDATE dbo.FcModuleVariable
	SET
		description = @description,
		type = @type,
		value = @value
	WHERE moduleName = @moduleName AND name = @name
END

RETURN 0

END
GO
