-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSourceTypesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSourceTypesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSourceTypesGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, class, configurationHandlerClass, configurationPropertyPageClass
FROM dbo.FhSourceType

RETURN 0

END
GO
