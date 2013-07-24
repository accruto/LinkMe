-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiEventDetailUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiEventDetailUpdate
(
	@name AS VARCHAR(512),
	@factoryClass AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiEventDetail
WHERE name = @name

IF @id IS NULL
BEGIN
	INSERT INTO dbo.FiEventDetail (id, name, factoryClass)
	VALUES (NEWID(), @name, @factoryClass)
END
ELSE
BEGIN
	-- EventDetail exists - update it.

	UPDATE dbo.FiEventDetail
	SET
		factoryClass = @factoryClass
	WHERE id = @id
END

RETURN 0

END
GO