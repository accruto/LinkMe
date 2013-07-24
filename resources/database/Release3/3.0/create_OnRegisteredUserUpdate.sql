if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnRegisteredUserUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnRegisteredUserUpdate]
GO

CREATE TRIGGER OnRegisteredUserUpdate
ON RegisteredUser
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER

-- First Name.

DECLARE firstNameCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.firstName, i.firstName
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	i.firstName <> d.firstName

OPEN firstNameCursor

FETCH NEXT FROM firstNameCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 11, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM firstNameCursor INTO @id, @previousValue, @newValue
END

CLOSE firstNameCursor
DEALLOCATE firstNameCursor

-- Last Name.

DECLARE lastNameCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.lastName, i.lastName
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	i.lastName <> d.lastName

OPEN lastNameCursor

FETCH NEXT FROM lastNameCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 12, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM lastNameCursor INTO @id, @previousValue, @newValue
END

CLOSE lastNameCursor
DEALLOCATE lastNameCursor

-- Email Address.

DECLARE emailAddressCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.emailAddress, i.emailAddress
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	i.emailAddress <> d.emailAddress

OPEN emailAddressCursor

FETCH NEXT FROM emailAddressCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 17, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM emailAddressCursor INTO @id, @previousValue, @newValue
END

CLOSE emailAddressCursor
DEALLOCATE emailAddressCursor

SET NOCOUNT OFF
