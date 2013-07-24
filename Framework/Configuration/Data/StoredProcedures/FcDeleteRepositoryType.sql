-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcDeleteRepositoryType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcDeleteRepositoryType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcDeleteRepositoryType
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT

-- Delete this category.

DELETE FROM FcRepositoryType
WHERE moduleName = @moduleName AND name = @name

SET @error = @@ERROR
IF @error != 0
BEGIN
	RAISERROR('Error %d occurred whilst trying to delete repository type ''%s''.', 16, 1, @error, @name)
	RETURN 1
END

RETURN 0

END
GO
