if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnMemberUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnMemberUpdate]
GO

CREATE TRIGGER OnMemberUpdate
ON Member
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER

-- Home phone number

DECLARE homePhoneNumberCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.homePhoneNumber, i.homePhoneNumber
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	ISNULL(i.homePhoneNumber, '') <> ISNULL(d.homePhoneNumber, '')

OPEN homePhoneNumberCursor

FETCH NEXT FROM homePhoneNumberCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 13, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM homePhoneNumberCursor INTO @id, @previousValue, @newValue
END

CLOSE homePhoneNumberCursor
DEALLOCATE homePhoneNumberCursor

-- Mobile phone number

DECLARE mobilePhoneNumberCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.mobilePhoneNumber, i.mobilePhoneNumber
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	ISNULL(i.mobilePhoneNumber, '') <> ISNULL(d.mobilePhoneNumber, '')

OPEN mobilePhoneNumberCursor

FETCH NEXT FROM mobilePhoneNumberCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 18, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM mobilePhoneNumberCursor INTO @id, @previousValue, @newValue
END

CLOSE mobilePhoneNumberCursor
DEALLOCATE mobilePhoneNumberCursor

-- Work phone number

DECLARE workPhoneNumberCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.workPhoneNumber, i.workPhoneNumber
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	ISNULL(i.workPhoneNumber, '') <> ISNULL(d.workPhoneNumber, '')

OPEN workPhoneNumberCursor

FETCH NEXT FROM workPhoneNumberCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 19, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM workPhoneNumberCursor INTO @id, @previousValue, @newValue
END

CLOSE workPhoneNumberCursor
DEALLOCATE workPhoneNumberCursor

-- Profile photo

DECLARE profilePhotoCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.profilePhotoId, i.profilePhotoId
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	ISNULL(i.profilePhotoId, '{00000000-0000-0000-0000-000000000000}') <> ISNULL(d.profilePhotoId, '{00000000-0000-0000-0000-000000000000}')

OPEN profilePhotoCursor

FETCH NEXT FROM profilePhotoCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 41, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM profilePhotoCursor INTO @id, @previousValue, @newValue
END

CLOSE profilePhotoCursor
DEALLOCATE profilePhotoCursor

SET NOCOUNT OFF

