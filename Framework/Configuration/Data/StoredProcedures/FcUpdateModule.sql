-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateModule]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateModule]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateModule
(
	@name AS VARCHAR(128),
	@displayName AS VARCHAR(128),
	@description AS VARCHAR(512),
	@extensionClass VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcModule WHERE name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcModule (name, displayName, description, extensionClass)
	VALUES (@name, @displayName, @description, @extensionClass)
END
ELSE
BEGIN
	-- Module exists - update it.

	UPDATE dbo.FcModule
	SET
		displayName = @displayName,
		description = @description,
		extensionClass = @extensionClass
	WHERE name = @name
END

RETURN 0

END
GO
