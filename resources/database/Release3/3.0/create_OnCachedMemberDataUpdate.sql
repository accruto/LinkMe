if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnCachedMemberDataUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnCachedMemberDataUpdate]
GO

CREATE TRIGGER OnCachedMemberDataUpdate
ON CachedMemberData
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @memberId UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER

-- Current Job Titles.

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.memberId, d.currentJobs, i.currentJobs
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.memberId = d.memberId
	INNER JOIN Networker AS n ON i.memberId = n.id
WHERE
	ISNULL(i.currentJobs, '') <> ISNULL(d.currentJobs, '')

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @memberId, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 21, @memberId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM insertedCursor INTO @memberId, @previousValue, @newValue
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF

