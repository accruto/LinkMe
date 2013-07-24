-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhApplicationUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhApplicationUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhApplicationUpdate
(
	@name AS VARCHAR(128),
	@description AS NVARCHAR(512),
	@applicationType AS VARCHAR(128),
	@configurationData TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the application.

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FhApplication
WHERE name = @name

IF @id IS NULL
BEGIN
	-- ApplicationType doesn't exist - insert it.

	INSERT INTO dbo.FhApplication (id, name, description, applicationType, configurationData)
	VALUES (NEWID(), @name, @description, @applicationType, @configurationData)
END
ELSE
BEGIN
	-- Application exists - update it.

	UPDATE dbo.FhApplication
	SET
		description = @description,
		applicationType = @applicationType,
		configurationData = @configurationData
	WHERE id = @id
END

RETURN 0

END
GO