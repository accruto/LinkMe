-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiMessageHandlerDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiMessageHandlerDelete]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiMessageHandlerDelete
(
	@fullName AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT
DECLARE @pattern AS VARCHAR(514)
SET @pattern = @fullName + '.%'

BEGIN TRANSACTION

-- Delete child message handlers.

DELETE FROM dbo.FiMessageHandler
WHERE fullName LIKE @pattern

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

-- Delete this message handler.

DELETE FROM dbo.FiMessageHandler
WHERE fullName = @fullName

SET @error = @@ERROR
IF @error != 0
BEGIN
	ROLLBACK TRANSACTION
	RETURN @error
END

COMMIT TRANSACTION
RETURN 0

END
GO