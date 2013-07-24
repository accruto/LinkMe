-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetComputers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetComputers]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetComputers
(
	@domainName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, description
FROM dbo.FcComputer
WHERE domainName = @domainName

RETURN 0

END
GO