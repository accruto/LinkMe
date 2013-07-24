-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateRepositoryType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateRepositoryType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateRepositoryType
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@displayName AS VARCHAR(128),
	@repositoryDisplayName AS VARCHAR(128),
	@description AS VARCHAR(512),
	@class VARCHAR(512),
	@isFileRepository BIT,
	@isVisible BIT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcRepositoryType WHERE moduleName = @moduleName AND name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcRepositoryType (moduleName, name, displayName, repositoryDisplayName, description, class, isFileRepository, isVisible)
	VALUES (@moduleName, @name, @displayName, @repositoryDisplayName, @description, @class, @isFileRepository, @isVisible)
END
ELSE
BEGIN
	-- RepositoryType exists - update it.

	UPDATE dbo.FcRepositoryType
	SET
		displayName = @displayName,
		repositoryDisplayName = @repositoryDisplayName,
		description = @description,
		class = @class,
		isFileRepository = @isFileRepository,
		isVisible = @isVisible
	WHERE moduleName = @moduleName AND name = @name
END

RETURN 0

END
GO
