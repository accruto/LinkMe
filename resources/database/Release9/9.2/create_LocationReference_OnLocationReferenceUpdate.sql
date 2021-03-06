IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[OnLocationReferenceUpdate]'))
DROP TRIGGER [dbo].[OnLocationReferenceUpdate]
GO

CREATE TRIGGER [dbo].[OnLocationReferenceUpdate]
ON [dbo].[LocationReference]
FOR UPDATE
AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- We only care when a Member's Address has been updated, so make sure it's one of those.

DECLARE @updatedMA TABLE
(
	[id] UNIQUEIDENTIFIER NOT NULL,
	iUnstructuredLocation NVARCHAR(100),
	iNamedLocationId INT,
	iCountrySubdivisionId INT,
	iLocalityId INT,
	dUnstructuredLocation NVARCHAR(100),
	dNamedLocationId INT,
	dCountrySubdivisionId INT,
	dLocalityId INT,
	memberId UNIQUEIDENTIFIER NOT NULL,
	dDisplayName LocationDisplayName NULL,
	iDisplayName LocationDisplayName NULL
)

INSERT INTO @updatedMA (id, iUnstructuredLocation, iNamedLocationId, iCountrySubdivisionId, iLocalityId,
	dUnstructuredLocation, dNamedLocationId, dCountrySubdivisionId, dLocalityId,
	memberId, dDisplayName, iDisplayName)
SELECT i.id, i.unstructuredLocation, i.namedLocationId, i.countrySubdivisionId, i.localityId,
	d.unstructuredLocation, d.namedLocationId, d.countrySubdivisionId, d.localityId,
	m.id, nld.displayName, nli.displayName
FROM inserted AS i
INNER JOIN deleted AS d ON i.id = d.id
INNER JOIN dbo.Address AS a WITH (NOLOCK) ON a.locationReferenceId = i.id
INNER JOIN dbo.Member AS m WITH (NOLOCK) ON m.addressId = a.id
LEFT OUTER JOIN dbo.PostalSuburb AS psd WITH (NOLOCK) ON psd.id = d.namedLocationId
LEFT OUTER JOIN dbo.NamedLocation AS nld WITH (NOLOCK) ON nld.id = psd.id
LEFT OUTER JOIN dbo.PostalSuburb AS psi WITH (NOLOCK) ON psi.id = i.namedLocationId
LEFT OUTER JOIN dbo.NamedLocation AS nli WITH (NOLOCK) ON nli.id = psi.id

IF (NOT EXISTS(SELECT * FROM @updatedMA))
	RETURN

-- Store the NetworkerEvents to insert in a temporary table to avoid cursors, which would otherwise
-- be needed, because we're inserting into two joined tables.

DECLARE @newEvent TABLE
(
	[id] UNIQUEIDENTIFIER NOT NULL,
	[time] DATETIME NOT NULL,
	[type] dbo.NetworkerEventType NOT NULL,
	actorId UNIQUEIDENTIFIER NOT NULL,
	[from] NVARCHAR(100),
	[to] NVARCHAR(100)
)

-- Suburb: add those location references where the change in the named location corresponds
-- to one postal suburb being updated to another or a postal suburb being added or a postal
-- suburb being deleted.

INSERT INTO	@newEvent ([id], [time], [type], actorId, [from], [to])
SELECT NEWID(), GETDATE(), 16, memberId, dDisplayName, iDisplayName
FROM @updatedMA AS u
WHERE ISNULL(u.iNamedLocationId, 0) <> ISNULL(u.dNamedLocationId, 0)
	AND NOT (u.dDisplayName IS NULL AND u.iDisplayName IS NULL)

-- Country Subdivision: iterate over those location references where the countrySubdivisionId has changed.

INSERT INTO @newEvent ([id], [time], [type], actorId, [from], [to])
SELECT	NEWID(), GETDATE(), CASE WHEN dsi.countryId = csi.countryId THEN 15 ELSE 14 END, u.memberId,
	CASE WHEN dsi.countryId = csi.countryId THEN dsi.[id] ELSE dsi.countryId END,
	CASE WHEN dsi.countryId = csi.countryId THEN csi.[id] ELSE csi.countryId END
FROM @updatedMA AS u
	INNER JOIN dbo.CountrySubdivision AS csi WITH (NOLOCK) ON u.iCountrySubdivisionId = csi.[id]
	INNER JOIN dbo.CountrySubdivision AS dsi WITH (NOLOCK) ON u.dCountrySubdivisionId = dsi.[id]
WHERE u.iCountrySubdivisionId <> u.dCountrySubdivisionId

-- Finally insert the events into the actual tables

IF (NOT EXISTS(SELECT * FROM @newEvent))
	RETURN

INSERT dbo.NetworkerEvent ([id], [time], [type], actorId)
SELECT [id], [time], [type], actorId
FROM @newEvent

INSERT dbo.NetworkerEventDelta (eventId, [from], [to])
SELECT [id], [from], [to]
FROM @newEvent

SET NOCOUNT OFF
GO
