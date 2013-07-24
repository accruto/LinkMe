-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiSourceTypesGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiSourceTypesGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiSourceTypesGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, availableEvents
FROM dbo.FiSourceType

RETURN 0

END
GO
