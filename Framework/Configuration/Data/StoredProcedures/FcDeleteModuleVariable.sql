-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcDeleteModuleVariable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcDeleteModuleVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcDeleteModuleVariable
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT

-- Delete this variable

DELETE FROM FcModuleVariable
WHERE moduleName = @moduleName AND name = @name

SET @error = @@ERROR
IF @error != 0
BEGIN
	RAISERROR('Error %d occurred whilst trying to delete repository ''%s''.', 16, 1, @error, @name)
	RETURN 1
END

RETURN 0

END
GO
