-- TrackingCommunication

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.TrackingCommunication') AND OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.TrackingCommunication
GO

CREATE TABLE dbo.TrackingCommunication
(
	id UNIQUEIDENTIFIER NOT NULL,
	sent BIGINT NULL,
	definition NVARCHAR(100) NULL,
	vertical UNIQUEIDENTIFIER NULL,
	userId UNIQUEIDENTIFIER NULL,
	emailAddress NVARCHAR(100) NULL
)
GO

ALTER TABLE dbo.TrackingCommunication ADD CONSTRAINT PK_TrackingCommunication
	PRIMARY KEY NONCLUSTERED (id)
GO

-- TrackingCommunicationOpened

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.TrackingCommunicationOpened') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.TrackingCommunicationOpened
GO

CREATE TABLE dbo.TrackingCommunicationOpened
(
	id UNIQUEIDENTIFIER NOT NULL,
	opened BIGINT NOT NULL
)
GO

ALTER TABLE dbo.TrackingCommunicationOpened ADD CONSTRAINT PK_TrackingCommunicationOpened
	PRIMARY KEY NONCLUSTERED (id, opened)
GO

-- TrackingCommunicationLink

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.TrackingCommunicationLink') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.TrackingCommunicationLink
GO

CREATE TABLE dbo.TrackingCommunicationLink
(
	id UNIQUEIDENTIFIER NOT NULL,
	communicationId UNIQUEIDENTIFIER NOT NULL,
	link NVARCHAR(1024) NULL,
	instance INT NULL
)
GO

ALTER TABLE dbo.TrackingCommunicationLink ADD CONSTRAINT PK_TrackingCommunicationLink
	PRIMARY KEY NONCLUSTERED (id)
GO

-- TrackingCommunicationLinkClicked

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.TrackingCommunicationLinkClicked') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.TrackingCommunicationLinkClicked
GO

CREATE TABLE dbo.TrackingCommunicationLinkClicked
(
	id UNIQUEIDENTIFIER NOT NULL,
	clicked BIGINT NOT NULL
)
GO

ALTER TABLE dbo.TrackingCommunicationLinkClicked ADD CONSTRAINT PK_TrackingCommunicationLinkClicked
	PRIMARY KEY NONCLUSTERED (id, clicked)
GO

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

INSERT
	dbo.TrackingCommunication (id, sent, definition, vertical, userId, emailAddress)
VALUES
	(@id, @sent, @definition, @vertical, @userId, @emailAddress)

SET @error = @@ERROR

IF (@error = 2601)
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

-- TrackingEnsureCommunication

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingEnsureCommunication]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingEnsureCommunication]
GO

CREATE PROCEDURE dbo.TrackingEnsureCommunication
(
	@id UNIQUEIDENTIFIER
)
AS

SET NOCOUNT ON

-- Insert the communication.

IF NOT EXISTS (SELECT * FROM dbo.TrackingCommunication WHERE id = @id)
	INSERT
		dbo.TrackingCommunication (id)
	VALUES
		(@id)

RETURN 0
GO

-- TrackingInsertCommunicationOpened

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingInsertCommunicationOpened]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingInsertCommunicationOpened]
GO

CREATE PROCEDURE dbo.TrackingInsertCommunicationOpened
(
	@id UNIQUEIDENTIFIER,
	@opened BIGINT
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunication @id

-- Insert the open.

INSERT
	dbo.TrackingCommunicationOpened (id, opened)
VALUES
	(@id, @opened)

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

INSERT
	dbo.TrackingCommunicationLink (id, communicationId, link, instance)
VALUES
	(@id, @communicationId, @link, @instance)

SET @error = @@ERROR

IF (@error = 2601)
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

-- TrackingEnsureCommunicationLink

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingEnsureCommunicationLink]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingEnsureCommunicationLink]
GO

CREATE PROCEDURE dbo.TrackingEnsureCommunicationLink
(
	@id UNIQUEIDENTIFIER,
	@communicationId UNIQUEIDENTIFIER
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunication @communicationId

-- Insert the communication link.

IF NOT EXISTS (SELECT * FROM dbo.TrackingCommunicationLink WHERE id = @id)
	INSERT
		dbo.TrackingCommunicationLink (id, communicationId)
	VALUES
		(@id, @communicationId)

RETURN 0
GO

-- TrackingInsertCommunicationLinkClicked

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingInsertCommunicationLinkClicked]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingInsertCommunicationLinkClicked]
GO

CREATE PROCEDURE dbo.TrackingInsertCommunicationLinkClicked
(
	@id UNIQUEIDENTIFIER,
	@communicationId UNIQUEIDENTIFIER,
	@clicked BIGINT
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunicationLink @id, @communicationId

-- Insert the click.

INSERT
	dbo.TrackingCommunicationLinkClicked (id, clicked)
VALUES
	(@id, @clicked)

RETURN 0
GO





