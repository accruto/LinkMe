if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnCandidateIndustryInsertDelete]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnCandidateIndustryInsertDelete]
GO

CREATE TRIGGER OnCandidateIndustryInsertDelete
ON CandidateIndustry
FOR INSERT, DELETE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @candidateId UNIQUEIDENTIFIER
DECLARE @industryId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER

-- Inserted

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.candidateId, i.industryId
FROM
	inserted AS i
	INNER JOIN Networker AS n ON i.candidateId = n.id

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @candidateId, @industryId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 22, @candidateId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, NULL, @industryId

	FETCH NEXT FROM insertedCursor INTO @candidateId, @industryId
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

-- deleted

DECLARE deletedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	d.candidateId, d.industryId
FROM
	deleted AS d
	INNER JOIN Networker AS n ON d.candidateId = n.id

OPEN deletedCursor

FETCH NEXT FROM deletedCursor INTO @candidateId, @industryId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 22, @candidateId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @industryId, NULL

	FETCH NEXT FROM deletedCursor INTO @candidateId, @industryId
END

CLOSE deletedCursor
DEALLOCATE deletedCursor

SET NOCOUNT OFF
