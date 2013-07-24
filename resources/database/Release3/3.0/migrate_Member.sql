-- Clean up the country data first.

UPDATE linkme_owner.user_profile
SET country = 'USA'
WHERE country = 'United States' OR country = 'United States of America'
GO

-- Define default member access settings.

DECLARE @defaultAccessFirst INT
DECLARE @defaultAccessSecond INT
DECLARE @defaultAccessPublic INT
DECLARE @defaultAccessEmployer TINYINT

SET @defaultAccessFirst = 8380415
SET @defaultAccessSecond = 6931803
SET @defaultAccessPublic = 590859
SET @defaultAccessEmployer = 31

-- Find the IDs of Australia.

DECLARE @australiaId INT
SELECT @australiaId = [id]
FROM dbo.Country
WHERE displayName = 'Australia'

-- Create Member and Address records for all the networker_profile records.
-- Countries that are not in the Country table get set to "Other".

DECLARE memberCursor CURSOR FOR
SELECT dbo.GuidFromString(np.[id]), CASE sex WHEN 'Male' THEN 1 WHEN 'Female' THEN 2 ELSE 0 END AS gender,
	NULLIF(LTRIM(RTRIM(mobilePhoneNumber)), '') AS phoneNumber,
	rs.[id] AS referralSourceId, profilePhotoId, ISNULL(c.[id], 0) AS countryId,
	LTRIM(RTRIM(postCode)), NULLIF(LTRIM(RTRIM(suburb)), ''), state, subRole
FROM linkme_owner.networker_profile np
INNER JOIN linkme_owner.user_profile up
ON np.[id] = up.[id]
LEFT OUTER JOIN dbo.ReferralSource rs
ON np.referralSourceId = rs._id -- Join on the old (GUID) ID, select the new (INT) ID
LEFT OUTER JOIN dbo.Country c
ON up.country = c.displayName
ORDER BY np.[id]

OPEN memberCursor

DECLARE @memberId UNIQUEIDENTIFIER
DECLARE @gender TINYINT
DECLARE @phoneNumber VARCHAR(20)
DECLARE @referralSourceId INT
DECLARE @profilePhotoId UNIQUEIDENTIFIER
DECLARE @countryId INT
DECLARE @subdivisionId INT
DECLARE @postCode VARCHAR(20)
DECLARE @suburb VARCHAR(80)
DECLARE @state VARCHAR(20)
DECLARE @subRole VARCHAR(20)

FETCH NEXT FROM memberCursor
INTO @memberId, @gender, @phoneNumber, @referralSourceId, @profilePhotoId, @countryId, @postCode,
	@suburb, @state, @subRole

WHILE @@FETCH_STATUS = 0
BEGIN
	-- Maximum postcode length is 8. Current postcodes that are longer are all invalid anyway.
	
	IF (LEN(@postCode) > 8) SET @postCode = NULL
	
	-- A lot of records have 'Unknown' for the state. Treat this as not entered.
	
	IF (@state = 'Unknown') SET @state = NULL

	DECLARE @countrySubdivisionId INT
	DECLARE @unstructuredLocation NVARCHAR(100)

	SET @countrySubdivisionId = NULL
	SET @unstructuredLocation = NULL

	-- Store whatever the user entered has entered for suburb, state, postcode in unstructuredLocation.

	SET @unstructuredLocation = LTRIM(RTRIM(ISNULL(@suburb, '') + ' ' + ISNULL(@state, '') + ' ' + ISNULL(@postcode, '')))

	-- Use the default (NULL) CountrySubdivision for the country.

	SELECT @countrySubdivisionId = cs.[id]
	FROM dbo.CountrySubdivision cs
	INNER JOIN dbo.GeographicalArea ga
	ON cs.[id] = ga.[id]
	WHERE cs.countryId = @countryid AND ga.displayName IS NULL

	DECLARE @addressId UNIQUEIDENTIFIER
	SET @addressId = NEWID()

	INSERT INTO dbo.Address([id], line1, line2, postcode, suburb, countrySubdivisionId, localityId, unstructuredLocation)
	VALUES (@addressId, NULL, NULL, NULL, NULL, @countrySubdivisionId, NULL, @unstructuredLocation)

	-- Work out whether the phone number is mobile or fixed. If fixed, assume it's a work number.
	-- For countries other than Australia just assume it's mobile.

	DECLARE @mobilePhoneNumber VARCHAR(20)
	DECLARE @workPhoneNumber VARCHAR(20)

	SET @mobilePhoneNumber = @phoneNumber
	SET @workPhoneNumber = NULL

	IF (@countryId = @australiaId AND (@phoneNumber LIKE '04%' OR @phoneNumber LIKE '(04%'
		OR @phoneNumber LIKE '+61(4)%' OR @phoneNumber LIKE '+6104%' OR @phoneNumber LIKE '+614%'))
	BEGIN
		SET @mobilePhoneNumber = NULL
		SET @workPhoneNumber = @phoneNumber
	END

	-- Finally add the Member (note that there is no existing date of birth data).

	INSERT INTO dbo.Member([id], dateOfBirth, gender, homePhoneNumber, mobilePhoneNumber, workPhoneNumber,
		employerAccess, firstDegreeAccess, secondDegreeAccess, publicAccess,
		enteredReferralSourceId, addressId, profilePhotoId)
	VALUES (@memberId, NULL, @gender, NULL, @mobilePhoneNumber, @workPhoneNumber, @defaultAccessEmployer,
		@defaultAccessFirst, @defaultAccessSecond, @defaultAccessPublic, @referralSourceId,
		@addressId, @profilePhotoId)

	-- Fetch the next row.

	FETCH NEXT FROM memberCursor
	INTO @memberId, @gender, @phoneNumber, @referralSourceId, @profilePhotoId, @countryId, @postCode,
		@suburb, @state, @subRole
END

CLOSE memberCursor
DEALLOCATE memberCursor

GO
