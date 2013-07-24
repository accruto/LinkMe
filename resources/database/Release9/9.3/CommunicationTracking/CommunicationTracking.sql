IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TrackingCommunicationLinkClicked]') AND name = N'IX_TrackingCommunicationLinkClicked_id')
DROP INDEX [IX_TrackingCommunicationLinkClicked_id] ON [dbo].[TrackingCommunicationLinkClicked]
GO

CREATE CLUSTERED INDEX [IX_TrackingCommunicationLinkClicked_id] ON [dbo].[TrackingCommunicationLinkClicked]
(
	[id]
)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrackingInsertCommunicationLinkClicked]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TrackingInsertCommunicationLinkClicked]
GO

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

IF NOT EXISTS (SELECT * FROM dbo.TrackingCommunicationLinkClicked WHERE id = @id AND clicked = @clicked)
	INSERT
		dbo.TrackingCommunicationLinkClicked (id, clicked)
	VALUES
		(@id, @clicked)

RETURN 0

GO
