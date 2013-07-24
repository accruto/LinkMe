if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnWhiteboardMessageInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnWhiteboardMessageInsert]
GO

CREATE TRIGGER OnWhiteboardMessageInsert
ON WhiteboardMessage
FOR INSERT

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @boardOwnerId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	id, boardOwnerId
FROM
	inserted

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @id, @boardOwnerId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 63, @boardOwnerId

	FETCH NEXT FROM insertedCursor INTO @id, @boardOwnerId
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF

