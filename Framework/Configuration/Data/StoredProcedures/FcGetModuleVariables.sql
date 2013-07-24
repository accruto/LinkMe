-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetModuleVariables]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetModuleVariables]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetModuleVariables
(
	@moduleName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, description, type, value
FROM dbo.FcModuleVariable
WHERE moduleName = @moduleName

RETURN 0

END
GO