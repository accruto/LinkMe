-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhSinkTypesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhSinkTypesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhSinkTypesGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, class, configurationHandlerClass, configurationPropertyPageClass
FROM dbo.FhSinkType

RETURN 0

END
GO
