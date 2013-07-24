-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FhThreadPoolDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FhThreadPoolDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FhThreadPoolDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DELETE FROM dbo.FhThreadPool
WHERE fullName = @fullName

RETURN 0

END
GO