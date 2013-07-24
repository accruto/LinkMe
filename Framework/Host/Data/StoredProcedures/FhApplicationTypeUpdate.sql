-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhApplicationTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhApplicationTypeUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhApplicationTypeUpdate
(
	@name AS VARCHAR(128),
	@displayName AS VARCHAR(128),
	@description AS NVARCHAR(512),
	@configurationHandlerClass VARCHAR(512),
	@configurationPropertyPageClass VARCHAR(512),
	@extensionClass VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the application type.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhApplicationType
WHERE name = @name

IF @id IS NULL
BEGIN
	-- ApplicationType doesn't exist - insert it.

	INSERT INTO dbo.FhApplicationType (id, name, displayName, description, configurationHandlerClass, configurationPropertyPageClass, extensionClass)
	VALUES (NEWID(), @name, @displayName, @description, @configurationHandlerClass, @configurationPropertyPageClass, @extensionClass)
END
ELSE
BEGIN
	-- ApplicationType exists - update it.

	UPDATE dbo.FhApplicationType
	SET
		displayName = @displayName,
		description = @description,
		configurationHandlerClass = @configurationHandlerClass,
		configurationPropertyPageClass = @configurationPropertyPageClass,
		extensionClass = @extensionClass
	WHERE id = @id
END

RETURN 0

END
GO