-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateStore]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateStore]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateStore
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@description AS VARCHAR(512),
	@storeType AS VARCHAR(128),
	@initialisationString AS VARCHAR(512),
	@repositoryType AS VARCHAR(128),
	@repositoryInitialisationString AS VARCHAR(512),
	@isReadOnly AS BIT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcStore WHERE moduleName = @moduleName AND name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcStore (moduleName, name, description, storeType, initialisationString, repositoryType, repositoryInitialisationString, isReadOnly)
	VALUES (@moduleName, @name, @description, @storeType, @initialisationString, @repositoryType, @repositoryInitialisationString, @isReadOnly)
END
ELSE
BEGIN
	-- Store exists - update it.

	UPDATE dbo.FcStore
	SET
		description = @description,
		storeType = @storeType,
		initialisationString = @initialisationString,
		repositoryType = @repositoryType,
		repositoryInitialisationString = @repositoryInitialisationString,
		isReadOnly = @isReadOnly
	WHERE moduleName = @moduleName AND name = @name
END

RETURN 0

END
GO
