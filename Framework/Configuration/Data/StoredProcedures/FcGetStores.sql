-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetStores]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetStores]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetStores
(
	@moduleName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, description, storeType, initialisationString, repositoryType, repositoryInitialisationString, isReadOnly
FROM dbo.FcStore
WHERE moduleName = @moduleName

RETURN 0

END
GO