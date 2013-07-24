IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingInsertCommunicationOpened]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingInsertCommunicationOpened]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingInsertCommunicationLinkClicked]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingInsertCommunicationLinkClicked]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingInsertCommunicationLink]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingInsertCommunicationLink]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingInsertCommunication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingInsertCommunication]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingEnsureCommunicationLink]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingEnsureCommunicationLink]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingEnsureCommunication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingEnsureCommunication]
GO

-----------------------
-- Ensure Communication

CREATE PROCEDURE [dbo].[TrackingEnsureCommunication]
(
	@id UNIQUEIDENTIFIER
)
AS

SET NOCOUNT ON

BEGIN TRAN
IF NOT EXISTS
(
	SELECT
		*
	FROM
		dbo.TrackingCommunication WITH (UPDLOCK, SERIALIZABLE)
	WHERE
		id = @id
)
BEGIN
	INSERT
		dbo.TrackingCommunication (id)
	VALUES
		(@id)
END
COMMIT TRAN

RETURN 0
GO

----------------------------
-- Ensure Communication Link

CREATE PROCEDURE [dbo].[TrackingEnsureCommunicationLink]
(
	@id UNIQUEIDENTIFIER,
	@communicationId UNIQUEIDENTIFIER
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunication @communicationId

-- Insert the communication link.

BEGIN TRAN
IF NOT EXISTS
(
	SELECT
		*
	FROM
		dbo.TrackingCommunicationLink WITH (UPDLOCK, SERIALIZABLE)
	WHERE
		id = @id AND communicationId = @communicationId
)
BEGIN
	INSERT
		dbo.TrackingCommunicationLink (id, communicationId)
	VALUES
		(@id, @communicationId)
END
COMMIT TRAN

RETURN 0
GO

-----------------------
-- Insert Communication

CREATE PROCEDURE [dbo].[TrackingInsertCommunication]
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

-- Make sure it is there.

EXEC dbo.TrackingEnsureCommunication @id

-- Update it.

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

RETURN 0
GO

----------------------------
-- Insert Communication Link

CREATE PROCEDURE [dbo].[TrackingInsertCommunicationLink]
(
	@id UNIQUEIDENTIFIER,
	@communicationId UNIQUEIDENTIFIER,
	@link NVARCHAR(1024),
	@instance INT
)
AS

SET NOCOUNT ON

-- Make sure it is there.

EXEC dbo.TrackingEnsureCommunicationLink @id, @communicationId

-- Update it.

UPDATE
	dbo.TrackingCommunicationLink
SET
	link = @link,
	instance = @instance
WHERE
	id = @id

RETURN 0
GO

------------------------------------
-- Insert Communication Link Clicked

CREATE PROCEDURE [dbo].[TrackingInsertCommunicationLinkClicked]
(
	@id UNIQUEIDENTIFIER,
	@communicationId UNIQUEIDENTIFIER,
	@clicked BIGINT
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunicationLink @id, @communicationId

-- Insert the click.

BEGIN TRAN
IF NOT EXISTS
(
	SELECT
		*
	FROM
		dbo.TrackingCommunicationLinkClicked WITH (UPDLOCK, SERIALIZABLE)
	WHERE
		id = @id AND clicked = @clicked
)
BEGIN
	INSERT
		dbo.TrackingCommunicationLinkClicked (id, clicked)
	VALUES
		(@id, @clicked)
END
COMMIT TRAN

RETURN 0
GO

------------------------------
-- Insert Communication Opened

CREATE PROCEDURE [dbo].[TrackingInsertCommunicationOpened]
(
	@id UNIQUEIDENTIFIER,
	@opened BIGINT
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunication @id

-- Insert the open.

BEGIN TRAN
IF NOT EXISTS
(
	SELECT
		*
	FROM
		dbo.TrackingCommunicationOpened WITH (UPDLOCK, SERIALIZABLE)
	WHERE
		id = @id AND opened = @opened
)
BEGIN
	INSERT
		dbo.TrackingCommunicationOpened (id, opened)
	VALUES
		(@id, @opened)
END
COMMIT TRAN

RETURN 0
GO

