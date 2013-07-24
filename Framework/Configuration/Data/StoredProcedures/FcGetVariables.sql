-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetVariables]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetVariables]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetVariables

AS
BEGIN

SET NOCOUNT ON

SELECT name, description, type, value
FROM dbo.FcVariable

RETURN 0

END
GO