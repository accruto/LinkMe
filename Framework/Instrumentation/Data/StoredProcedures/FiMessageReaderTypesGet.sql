-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageReaderTypesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageReaderTypesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageReaderTypesGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, displayName, class, configurationHandlerClass, configurationPropertyPageClass
FROM dbo.FiMessageReaderType

RETURN 0

END
GO
