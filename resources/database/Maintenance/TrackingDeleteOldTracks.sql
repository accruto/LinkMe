USE [Instrumentation]
GO
/****** Object:  StoredProcedure [dbo].[TrackingDeleteOldTracks]    Script Date: 05/13/2012 00:46:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE [dbo].[TrackingDeleteOldTracks]
(
	@days AS INT
)
AS

SET NOCOUNT ON

-- Determine the date.

DECLARE @date AS DATETIME
SET @date = DATEADD(dd, 0, DATEDIFF(dd, 0, DATEADD(d, -1 * @days, GETDATE())))

-- Convert it to ticks.

DECLARE @ticks AS BIGINT
SET @ticks = dbo.DateTimeToTicks(@date)

-- Delete

DELETE
    dbo.TrackingCommunicationLinkClicked
FROM
    dbo.TrackingCommunicationLinkClicked AS TCLC
INNER JOIN
	dbo.TrackingCommunicationLink AS TLC ON TCLC.id = TLC.id
INNER JOIN
	dbo.TrackingCommunication AS TC ON TLC.communicationId = TC.id
WHERE
	TC.sent < @ticks

DELETE
    dbo.TrackingCommunicationLink
FROM
	dbo.TrackingCommunicationLink AS TLC
INNER JOIN
	dbo.TrackingCommunication AS TC ON TLC.communicationId = TC.id
WHERE
	TC.sent < @ticks

DELETE
    dbo.TrackingCommunicationOpened
FROM
	dbo.TrackingCommunicationOpened AS TLO
INNER JOIN
	dbo.TrackingCommunication AS TC ON TLO.id = TC.id
WHERE
	TC.sent < @ticks

DELETE
    dbo.TrackingCommunication
WHERE
	sent < @ticks

