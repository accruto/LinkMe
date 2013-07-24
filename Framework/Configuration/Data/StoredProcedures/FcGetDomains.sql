-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetDomains]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetDomains]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetDomains
AS
BEGIN

SET NOCOUNT ON

SELECT name, description, isDefault
FROM dbo.FcDomain

RETURN 0

END
GO