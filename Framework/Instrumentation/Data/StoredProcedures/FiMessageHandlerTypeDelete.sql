-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlerTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageHandlerTypeDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageHandlerTypeDelete
(
	@name AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

DELETE FROM dbo.FiMessageHandlerType
WHERE name = @name

RETURN 0

END
GO