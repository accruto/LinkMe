if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnNetworkerUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnNetworkerUpdate]
GO

CREATE TRIGGER OnNetworkerUpdate
ON Networker
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER

-- What I'm Doing Text.

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.whatImDoingText, i.whatImDoingText
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
WHERE
	ISNULL(i.whatImDoingText, '') <> ISNULL(d.whatImDoingText, '')

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 61, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM insertedCursor INTO @id, @previousValue, @newValue
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF


