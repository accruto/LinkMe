if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnAddressUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnAddressUpdate]
GO

CREATE TRIGGER OnAddressUpdate
ON Address
FOR UPDATE

AS

-- Need to set this for NHibernate.

SET NOCOUNT ON

-- Need to use a cursor because ids are not auto-generated, e.g. via an identity or a default.

DECLARE @id UNIQUEIDENTIFIER
DECLARE @previousValue NVARCHAR(100)
DECLARE @newValue NVARCHAR(100)
DECLARE @eventId UNIQUEIDENTIFIER

/*
-- Postcode.

DECLARE postcodeCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.postcode, i.postcode
FROM
	inserted AS i
	INNER JOIN deleted as d ON i.id = d.id
WHERE
	ISNULL(i.postcode, '') <> ISNULL(d.postcode, '')

OPEN postcodeCursor

FETCH NEXT FROM postcodeCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 14, m.id
	FROM
		Member AS m
	WHERE
		m.addressId = @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM postcodeCursor INTO @id, @previousValue, @newValue
END

CLOSE postcodeCursor
DEALLOCATE postcodeCursor
*/

-- Suburb.

DECLARE suburbCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.suburb, i.suburb
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Member AS m ON i.id = m.addressId
	INNER JOIN Networker AS n ON m.id = n.id
WHERE
	ISNULL(i.suburb, '') <> ISNULL(d.suburb, '')

OPEN suburbCursor

FETCH NEXT FROM suburbCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 16, m.id
	FROM
		Member AS m
	WHERE
		m.addressId = @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM suburbCursor INTO @id, @previousValue, @newValue
END

CLOSE suburbCursor
DEALLOCATE suburbCursor

-- Locality.

DECLARE localityCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.localityId, i.localityId
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Member AS m ON i.id = m.addressId
	INNER JOIN Networker AS n ON m.id = n.id
WHERE
	ISNULL(i.localityId, 0) <> ISNULL(d.localityId, 0)

OPEN localityCursor

FETCH NEXT FROM localityCursor INTO @id, @previousValue, @newValue
WHILE @@FETCH_STATUS = 0
BEGIN

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), 16, m.id
	FROM
		Member AS m
	WHERE
		m.addressId = @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM localityCursor INTO @id, @previousValue, @newValue
END

CLOSE localityCursor
DEALLOCATE localityCursor

-- Country Subdivision.

DECLARE countrySubdivisionCursor CURSOR STATIC FORWARD_ONLY FOR
SELECT
	i.id, d.countrySubdivisionId, i.countrySubdivisionId
FROM
	inserted AS i
	INNER JOIN deleted AS d ON i.id = d.id
	INNER JOIN Member AS m ON i.id = m.addressId
	INNER JOIN Networker AS n ON m.id = n.id
WHERE
	i.countrySubdivisionId <> d.countrySubdivisionId

OPEN countrySubdivisionCursor

FETCH NEXT FROM countrySubdivisionCursor INTO @id, @previousValue, @newValue
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
		SET @eventType = 15
	END
	ELSE
	BEGIN
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

	SET @eventId = NEWID()
	INSERT
		dbo.NetworkerEvent ( id, [time], type, actorId )
	SELECT
		@eventId, GETDATE(), @eventType, m.id
	FROM
		Member AS m
	WHERE
		m.addressId = @id

	INSERT
		dbo.NetworkerEventDelta ( eventId, [from], [to] )
	SELECT
		@eventId, @previousValue, @newValue

	FETCH NEXT FROM countrySubdivisionCursor INTO @id, @previousValue, @newValue
END

CLOSE countrySubdivisionCursor
DEALLOCATE countrySubdivisionCursor

SET NOCOUNT OFF

