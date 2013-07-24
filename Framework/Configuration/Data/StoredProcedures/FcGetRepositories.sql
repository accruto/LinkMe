-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetRepositories]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetRepositories]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetRepositories
(
	@moduleName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, description, repositoryType, initialisationString, isReadOnly, isLocal
FROM dbo.FcRepository
WHERE moduleName = @moduleName

RETURN 0

END
GO