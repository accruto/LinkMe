if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnNetworkLinkInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnNetworkLinkInsert]
GO

CREATE TRIGGER OnNetworkLinkInsert
ON NetworkLink
FOR INSERT

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @fromId UNIQUEIDENTIFIER
DECLARE @toId UNIQUEIDENTIFIER
DECLARE @eventId UNIQUEIDENTIFIER

DECLARE insertedCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT fromNetworkerId, toNetworkerId FROM inserted

OPEN insertedCursor

FETCH NEXT FROM insertedCursor INTO @fromId, @toId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId, actionedNetworkerId )
	SELECT
		@eventId, GETDATE(), 31, @fromId, @toId

	FETCH NEXT FROM insertedCursor INTO @fromId, @toId
END

CLOSE insertedCursor
DEALLOCATE insertedCursor

SET NOCOUNT OFF

