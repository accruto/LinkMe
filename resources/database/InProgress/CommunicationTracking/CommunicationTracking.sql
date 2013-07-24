-- TrackingCommunicationBounced

IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE id = object_id('dbo.TrackingCommunicationBounced') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE dbo.TrackingCommunicationBounced
GO

CREATE TABLE dbo.TrackingCommunicationBounced
(
	id UNIQUEIDENTIFIER NOT NULL,
	bounced BIGINT NOT NULL
)
GO

ALTER TABLE dbo.TrackingCommunicationBounced ADD CONSTRAINT PK_TrackingCommunicationBounced
	PRIMARY KEY NONCLUSTERED (id, bounced)
GO

-- TrackingInsertCommunicationBounced

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrackingInsertCommunicationBounced]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[TrackingInsertCommunicationBounced]
GO

CREATE PROCEDURE dbo.TrackingInsertCommunicationBounced
(
	@id UNIQUEIDENTIFIER,
	@bounced BIGINT
)
AS

SET NOCOUNT ON

EXEC dbo.TrackingEnsureCommunication @id

-- Insert the bounce.

INSERT
	dbo.TrackingCommunicationBounced (id, bounced)
VALUES
	(@id, @bounced)

RETURN 0
GO

