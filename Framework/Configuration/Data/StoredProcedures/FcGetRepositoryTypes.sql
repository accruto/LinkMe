-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcGetRepositoryTypes]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcGetRepositoryTypes]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcGetRepositoryTypes
(
	@moduleName AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

SELECT name, displayName, repositoryDisplayName, description, class, isFileRepository, isVisible
FROM dbo.FcRepositoryType
WHERE moduleName = @moduleName

RETURN 0

END
GO