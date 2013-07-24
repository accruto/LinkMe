-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetStoreTypes]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetStoreTypes]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetStoreTypes
(
	@moduleName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, displayName, storeDisplayName, description, class, extensionClass, isFileStore
FROM dbo.FcStoreType
WHERE moduleName = @moduleName

RETURN 0

END
GO