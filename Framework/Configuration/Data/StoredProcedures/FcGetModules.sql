-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetModules]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetModules]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetModules
AS
BEGIN

SET NOCOUNT ON

SELECT name, displayName, description, extensionClass
FROM dbo.FcModule

RETURN 0

END
GO