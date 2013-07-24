-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetDomainVariables]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetDomainVariables]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetDomainVariables
(
	@domainName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, description, type, value
FROM dbo.FcDomainVariable
WHERE domainName = @domainName

RETURN 0

END
GO