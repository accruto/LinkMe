set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER TRIGGER [OnWhiteboardMessageInsert]
ON [dbo].[WhiteboardMessage]
FOR INSERT

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @boardOwnerId UNIQUEIDENTIFIER
DECLARE @senderId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER
DECLARE @text VARCHAR(100)

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	id, senderId, boardOwnerId, [text]
FROM
	inserted

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @id, @senderId, @boardOwnerId, @text
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId, actionedNetworkerId )
	SELECT
		@eventId, GETDATE(), 63, @boardOwnerId, @senderId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, NULL, left(@text, 100)

	FETCH NEXT FROM insertedCursor INTO @id, @senderId, @boardOwnerId, @text
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF
