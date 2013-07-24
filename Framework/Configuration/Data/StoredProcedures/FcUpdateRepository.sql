-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateRepository]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateRepository]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateRepository
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@description AS VARCHAR(512),
	@repositoryType AS VARCHAR(128),
	@initialisationString AS VARCHAR(512),
	@isReadOnly AS BIT,
	@isLocal AS BIT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcRepository WHERE moduleName = @moduleName AND name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcRepository (moduleName, name, description, repositoryType, initialisationString, isReadOnly, isLocal)
	VALUES (@moduleName, @name, @description, @repositoryType, @initialisationString, @isReadOnly, @isLocal)
END
ELSE
BEGIN
	-- Repository exists - update it.

	UPDATE dbo.FcRepository
	SET
		description = @description,
		repositoryType = @repositoryType,
		initialisationString = @initialisationString,
		isReadOnly = @isReadOnly,
		isLocal = @isLocal
	WHERE moduleName = @moduleName AND name = @name
END

RETURN 0

END
GO
