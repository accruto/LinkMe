-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateStoreType]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateStoreType]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateStoreType
(
	@moduleName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@displayName AS VARCHAR(128),
	@storeDisplayName AS VARCHAR(128),
	@description AS VARCHAR(512),
	@class VARCHAR(512),
	@extensionClass VARCHAR(512),
	@isFileStore BIT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcStoreType WHERE moduleName = @moduleName AND name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcStoreType (moduleName, name, displayName, storeDisplayName, description, class, extensionClass, isFileStore)
	VALUES (@moduleName, @name, @displayName, @storeDisplayName, @description, @class, @extensionClass, @isFileStore)
END
ELSE
BEGIN
	-- StoreType exists - update it.

	UPDATE dbo.FcStoreType
	SET
		displayName = @displayName,
		storeDisplayName = @storeDisplayName,
		description = @description,
		class = @class,
		extensionClass = @extensionClass,
		isFileStore = @isFileStore
	WHERE moduleName = @moduleName AND name = @name
END

RETURN 0

END
GO
