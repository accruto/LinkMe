if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnLocationReferenceUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnLocationReferenceUpdate]
GO

CREATE TRIGGER OnLocationReferenceUpdate
ON LocationReference
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER
DECLARE @networkerId UNIQUEIDENTIFIER

-- Suburb: iterate over those location references where the change in the named location corresponds to one postal suburb being updated to another.

DECLARE suburbCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, nld.displayName, nli.displayName, m.id
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN PostalSuburb AS psd ON psd.id = d.namedLocationId
	INNER JOIN NamedLocation AS nld ON nld.id = psd.id
	INNER JOIN PostalSuburb AS psi ON psi.id = i.namedLocationId
	INNER JOIN NamedLocation AS nli ON nli.id = psi.id
	INNER JOIN Address AS a ON a.locationReferenceId = i.id
	INNER JOIN Member AS m ON m.addressId = a.id
WHERE
	ISNULL(i.namedLocationId, 0) <> ISNULL(d.namedLocationId, 0)

OPEN suburbCursor

FETCH NEXT FROM suburbCursor INTO @id, @previousValue, @newValue, @networkerId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 16, @networkerId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM suburbCursor INTO @id, @previousValue, @newValue, @networkerId
END

CLOSE suburbCursor
DEALLOCATE suburbCursor

-- Suburb: iterate over those location references where a postal suburb has been removed.

DECLARE suburbCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, nld.displayName, m.id
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN PostalSuburb AS psd ON psd.id = d.namedLocationId
	INNER JOIN NamedLocation AS nld ON nld.id = psd.id
	INNER JOIN Address AS a ON a.locationReferenceId = i.id
	INNER JOIN Member AS m ON m.addressId = a.id
WHERE
	ISNULL(i.namedLocationId, 0) <> ISNULL(d.namedLocationId, 0)
	AND NOT EXISTS (SELECT * FROM PostalSuburb ps WHERE ps.id = i.namedLocationId)

OPEN suburbCursor

FETCH NEXT FROM suburbCursor INTO @id, @previousValue, @networkerId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 16, @networkerId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, NULL

	FETCH NEXT FROM suburbCursor INTO @id, @previousValue, @networkerId
END

CLOSE suburbCursor
DEALLOCATE suburbCursor

-- Suburb: iterate over those location references where a non-postal suburb has been changed to a postal suburb.

DECLARE suburbCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, nli.displayName, m.id
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN PostalSuburb AS psi ON psi.id = i.namedLocationId
	INNER JOIN NamedLocation AS nli ON nli.id = psi.id
	INNER JOIN Address AS a ON a.locationReferenceId = i.id
	INNER JOIN Member AS m ON m.addressId = a.id
WHERE
	ISNULL(i.namedLocationId, 0) <> ISNULL(d.namedLocationId, 0)
	AND NOT EXISTS (SELECT * FROM PostalSuburb AS ps WHERE ps.id = d.namedLocationId)

OPEN suburbCursor

FETCH NEXT FROM suburbCursor INTO @id, @newValue, @networkerId
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 16, @networkerId

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, NULL, @newValue

	FETCH NEXT FROM suburbCursor INTO @id, @newValue, @networkerId
END

CLOSE suburbCursor
DEALLOCATE suburbCursor

-- Country Subdivision: iterate over those location references where the countrySubdivisionId has changed.

DECLARE countrySubdivisionCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.countrySubdivisionId, i.countrySubdivisionId, m.id
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Address AS a ON a.locationReferenceId = i.id
	INNER JOIN Member AS m ON m.addressId = a.id
WHERE
	i.countrySubdivisionId <> d.countrySubdivisionId

OPEN countrySubdivisionCursor

FETCH NEXT FROM countrySubdivisionCursor INTO @id, @previousValue, @newValue, @networkerId
WHILE @@FETCH_STATUS = 0
BEGIN

	-- Check whether the country has changed.

	DECLARE @eventType INT

	IF EXISTS (
		SELECT
			*
		FROM
			CountrySubdivision AS csi
			INNER JOIN CountrySubdivision AS dsi ON csi.countryId = dsi.countryId
		WHERE
			csi.id = @newValue
			AND dsi.id = @previousValue
	)
	BEGIN

		-- CountrySubdivision has been updated.

		SET @eventType = 15
	END
	ELSE
	BEGIN
		-- The country has been updated so grab the values of the country.

		SET @eventType = 14

		SELECT
			@previousValue = countryId
		FROM
			CountrySubdivision
		WHERE
			id = @previousValue

		SELECT
			@newValue = countryId
		FROM
			CountrySubdivision
		WHERE
			id = @newValue
	END

	-- Insert the event.

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), @eventType, @networkerId

	-- Insert the delta.

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM countrySubdivisionCursor INTO @id, @previousValue, @newValue, @networkerId
END

CLOSE countrySubdivisionCursor
DEALLOCATE countrySubdivisionCursor

SET NOCOUNT OFF

