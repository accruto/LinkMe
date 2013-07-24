-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FiEventTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FiEventTypeUpdate]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FiEventTypeUpdate
(
	@name AS VARCHAR(512),
	@isEnabled AS BIT,
	@eventDetails AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the channel

DECLARE @id AS UNIQUEIDENTIFIER

SELECT @id = id
FROM dbo.FiEventType
WHERE name = @name

IF @id IS NULL
BEGIN
	INSERT INTO dbo.FiEventType (id, name, isEnabled, eventDetails)
	VALUES (NEWID(), @name, @isEnabled, @eventDetails)
END
ELSE
BEGIN
	-- EventType exists - update it.

	UPDATE dbo.FiEventType
	SET
		isEnabled = @isEnabled,
		eventDetails = @eventDetails
	WHERE id = @id
END

RETURN 0

END
GO