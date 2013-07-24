if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnResumeInsertUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnResumeInsertUpdate]
GO

CREATE TRIGGER OnResumeInsertUpdate
ON Resume
FOR INSERT, UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @candidateId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, i.candidateId
FROM
	inserted AS i
	INNER JOIN Networker AS n ON i.candidateId = n.id

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @id, @candidateId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 100, @candidateId

	FETCH NEXT FROM insertedCursor INTO @id, @candidateId
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF

