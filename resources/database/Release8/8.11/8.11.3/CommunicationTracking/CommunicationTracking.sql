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

IF NOT EXISTS (SELECT * FROM dbo.TrackingCommunicationOpened WHERE id = @id AND opened = @opened)
	INSERT
		dbo.TrackingCommunicationOpened (id, opened)
	VALUES
		(@id, @opened)

RETURN 0
GO

