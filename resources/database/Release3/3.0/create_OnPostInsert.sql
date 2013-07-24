if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnPostInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnPostInsert]
GO

CREATE TRIGGER OnPostInsert
ON tbl_Post
FOR INSERT

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id INT
DECLARE @userId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, b.UserId
FROM
	inserted AS i
	INNER JOIN tbl_Blog AS b ON i.Blog = b.id
	INNER JOIN Networker AS n ON b.UserId = n.id

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @id, @userId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 62, @userId

	FETCH NEXT FROM insertedCursor INTO @id, @userId
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF

