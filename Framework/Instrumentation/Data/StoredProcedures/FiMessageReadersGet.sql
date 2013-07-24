-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageReadersGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageReadersGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageReadersGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, messageReaderType, configurationData
FROM dbo.FiMessageReader

RETURN 0

END
GO
