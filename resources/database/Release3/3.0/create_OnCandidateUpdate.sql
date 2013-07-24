if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnCandidateUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnCandidateUpdate]
GO

CREATE TRIGGER OnCandidateUpdate
ON Candidate
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER

-- Status

DECLARE statusCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.status, i.status
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	i.status <> d.status

OPEN statusCursor

FETCH NEXT FROM statusCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	-- Status.

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 52, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM statusCursor INTO @id, @previousValue, @newValue
END

CLOSE statusCursor
DEALLOCATE statusCursor

-- Current Job Types.

DECLARE currentJobTypesCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.currentJobTypes, i.currentJobTypes
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	i.currentJobTypes <> d.currentJobTypes

OPEN currentJobTypesCursor

FETCH NEXT FROM currentJobTypesCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 23, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM currentJobTypesCursor INTO @id, @previousValue, @newValue
END

CLOSE currentJobTypesCursor
DEALLOCATE currentJobTypesCursor

-- Desired Job Title.

DECLARE desiredJobTitleCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.desiredJobTitle, i.desiredJobTitle
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Networker AS n ON i.id = n.id
WHERE
	ISNULL(i.desiredJobTitle, '') <> ISNULL(d.desiredJobTitle, '')

OPEN desiredJobTitleCursor

FETCH NEXT FROM desiredJobTitleCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 51, @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM desiredJobTitleCursor INTO @id, @previousValue, @newValue
END

CLOSE desiredJobTitleCursor
DEALLOCATE desiredJobTitleCursor

SET NOCOUNT OFF

