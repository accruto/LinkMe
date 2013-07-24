-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhApplicationTypesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhApplicationTypesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhApplicationTypesGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, displayName, description, configurationHandlerClass, configurationPropertyPageClass, extensionClass
FROM dbo.FhApplicationType

RETURN 0

END
GO
