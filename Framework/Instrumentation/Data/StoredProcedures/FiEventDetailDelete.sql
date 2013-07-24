-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventDetailDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiEventDetailDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiEventDetailDelete
(
	@name AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DELETE FROM dbo.FiEventDetail
WHERE name = @name

RETURN 0

END
GO