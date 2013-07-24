if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnBlogPostInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnBlogPostInsert]
GO

CREATE TRIGGER OnBlogPostInsert
ON dbo.BlogPost
FOR INSERT

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @blogId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, i.blogId
FROM
	inserted AS i

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @id, @blogId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 62, @blogId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, NULL, @id

	FETCH NEXT FROM insertedCursor INTO @id, @blogId
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF

