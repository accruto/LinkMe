-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhApplicationsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhApplicationsGet]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhApplicationsGet

AS
BEGIN

SET NOCOUNT ON

SELECT name, description, applicationType, configurationData
FROM dbo.FhApplication

RETURN 0

END
GO
