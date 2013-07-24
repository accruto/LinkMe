-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventDetailsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiEventDetailsGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiEventDetailsGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, factoryClass
FROM dbo.FiEventDetail

RETURN 0

END
GO
