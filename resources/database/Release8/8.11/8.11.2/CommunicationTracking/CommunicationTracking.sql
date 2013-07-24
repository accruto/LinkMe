-- TrackingInsertCommunication

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingInsertCommunication]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingInsertCommunication]
GO

CREATE PROCEDURE dbo.TrackingInsertCommunication
(
	@id UNIQUEIDENTIFIER,
	@sent BIGINT,
	@definition NVARCHAR(100),
	@vertical UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER,
	@emailAddress NVARCHAR(100)
)
AS

SET NOCOUNT ON

DECLARE @error INT

-- Insert the communication.

IF NOT EXISTS (SELECT * FROM dbo.TrackingCommunication WHERE id = @id)
BEGIN
	INSERT
		dbo.TrackingCommunication (id, sent, definition, vertical, userId, emailAddress)
	VALUES
		(@id, @sent, @definition, @vertical, @userId, @emailAddress)
END
ELSE
BEGIN
	-- Another connection inserted this row - update it.

	UPDATE
		dbo.TrackingCommunication
	SET
		sent = @sent,
		definition = @definition,
		vertical = @vertical,
		userId = @userId,
		emailAddress = @emailAddress
	WHERE
		id = @id
END

RETURN 0
GO

-- TrackingInsertCommunicationLink

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingInsertCommunicationLink]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingInsertCommunicationLink]
GO

CREATE PROCEDURE dbo.TrackingInsertCommunicationLink
(
	@id UNIQUEIDENTIFIER,
	@communicationId UNIQUEIDENTIFIER,
	@link NVARCHAR(1024),
	@instance INT
)
AS

SET NOCOUNT ON

DECLARE @error INT

-- Ensure other stuff is there.

EXEC dbo.TrackingEnsureCommunication @communicationId

-- Insert the link.

IF NOT EXISTS (SELECT * FROM dbo.TrackingCommunicationLink WHERE id = @id)
BEGIN
	INSERT
		dbo.TrackingCommunicationLink (id, communicationId, link, instance)
	VALUES
		(@id, @communicationId, @link, @instance)
END
ELSE
BEGIN
	-- Another connection inserted this row - update it.

	UPDATE
		dbo.TrackingCommunicationLink
	SET
		communicationId = @communicationId,
		link = @link,
		instance = @instance
	WHERE
		id = @id
END

RETURN 0
GO

