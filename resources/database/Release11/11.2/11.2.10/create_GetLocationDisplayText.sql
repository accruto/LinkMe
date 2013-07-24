IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].GetLocationDisplayText') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].GetLocationDisplayText
GO

CREATE FUNCTION dbo.GetLocationDisplayText(@locationReferenceId AS UNIQUEIDENTIFIER)
RETURNS NVARCHAR(100)
AS
BEGIN

	-- Mirrors the logic in LocationReference.ToString()

	DECLARE @unstructuredLocation NVARCHAR(100)
	DECLARE @namedLocationId INT
	DECLARE @countrySubdivisionId INT
	DECLARE @shortDisplayName NVARCHAR(100)
	DECLARE @displayText NVARCHAR(100)

	SELECT
		@unstructuredLocation = unstructuredLocation,
		@namedLocationId = namedLocationId,
		@countrySubdivisionId = countrySubdivisionId
	FROM
		dbo.LocationReference
	WHERE
		id = @locationReferenceId

	-- Unstructured location

	IF (NOT @unstructuredLocation IS NULL)
		RETURN @unstructuredLocation

	IF (@namedLocationId IS NULL)
		RETURN NULL

	-- Locality

	IF EXISTS (SELECT * FROM dbo.Locality WHERE id = @namedLocationId)
	BEGIN
		SELECT
			@shortDisplayName = shortDisplayName
		FROM
			dbo.CountrySubdivision
		WHERE
			id = @countrySubdivisionId

		IF (@shortDisplayName IS NULL)
			SELECT
				@displayText = displayName
			FROM
				dbo.NamedLocation
			WHERE
				id = @namedLocationId
		ELSE
			SELECT
				@displayText = displayName + ' ' + @shortDisplayName
			FROM
				dbo.NamedLocation
			WHERE
				id = @namedLocationId
	END

	-- PostalCode

	IF EXISTS (SELECT * FROM dbo.PostalCode WHERE id = @namedLocationId)
	BEGIN
		SELECT
			@shortDisplayName = shortDisplayName
		FROM
			dbo.CountrySubdivision
		WHERE
			id = @countrySubdivisionId

		IF (@shortDisplayName IS NULL)
			SELECT
				@displayText = displayName
			FROM
				dbo.NamedLocation
			WHERE
				id = @namedLocationId
		ELSE
			SELECT
				@displayText = displayName + ' ' + @shortDisplayName
			FROM
				dbo.NamedLocation
			WHERE
				id = @namedLocationId
	END

	-- Region

	IF EXISTS (SELECT * FROM dbo.Region WHERE id = @namedLocationId)
		SELECT
			@displayText = displayName
		FROM
			dbo.NamedLocation
		WHERE
			id = @namedLocationId

	-- PostalSuburb

	IF EXISTS (SELECT * FROM dbo.PostalSuburb WHERE id = @namedLocationId)
	BEGIN
		SELECT
			@shortDisplayName = shortDisplayName
		FROM
			dbo.CountrySubdivision
		WHERE
			id = @countrySubdivisionId

		SELECT
			@displayText = nl.displayName + ' ' + @shortDisplayName + ' ' + pc.displayName
		FROM
			dbo.NamedLocation AS nl
		INNER JOIN
			dbo.PostalSuburb AS ps ON ps.id = nl.id
		INNER JOIN
			dbo.NamedLocation AS pc ON pc.id = ps.postcodeId
		WHERE
			nl.id = @namedLocationId
	END

	-- CountrySubdivision

	IF EXISTS (SELECT * FROM dbo.CountrySubdivision WHERE id = @namedLocationId)
	BEGIN
		SELECT
			@displayText = shortDisplayName
		FROM
			dbo.CountrySubdivision
		WHERE
			id = @namedLocationId
	END

	RETURN @displayText
END
GO
