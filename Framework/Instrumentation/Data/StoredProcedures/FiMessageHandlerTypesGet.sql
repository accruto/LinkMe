-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlerTypesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageHandlerTypesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageHandlerTypesGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, displayName, class, configurationHandlerClass, configurationPropertyPageClass
FROM dbo.FiMessageHandlerType

RETURN 0

END
GO
