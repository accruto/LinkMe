DECLARE employerCursor CURSOR FOR
SELECT dbo.GuidFromString(ep.[id]), contactPhoneNumber, currentJobTitle,
	CASE subRole WHEN 'Employer' THEN 1 WHEN 'Recruiter' THEN 2 END AS employerSubRole,
	companyId, ownerPartnerId, LEFT(organisationName, 100)
FROM linkme_owner.employer_profile ep
INNER JOIN linkme_owner.user_profile up
ON ep.[id] = up.[id]

OPEN employerCursor

DECLARE @id UNIQUEIDENTIFIER
DECLARE @contactPhoneNumber VARCHAR(20)
DECLARE @jobTitle NVARCHAR(100)
DECLARE @subRole TINYINT
DECLARE @companyId UNIQUEIDENTIFIER
DECLARE @ownerPartnerId UNIQUEIDENTIFIER
DECLARE @organisationName NVARCHAR(100)

FETCH NEXT FROM employerCursor
INTO @id, @contactPhoneNumber, @jobTitle, @subRole, @companyId, @ownerPartnerId, @organisationName

WHILE @@FETCH_STATUS = 0
BEGIN
	-- Create a new, unverified Company for every employer that doesn't have a verified one.

	IF (@companyId IS NULL)
	BEGIN
		SET @companyId = NEWID()

		INSERT INTO dbo.Company([id], [name], verifiedById)
		VALUES (@companyId, @organisationName, NULL)
	END

	-- Insert the Employer.
	
	INSERT INTO dbo.Employer([id], contactPhoneNumber, jobTitle, subRole, companyId, ownerPartnerId)
	VALUES (@id, @contactPhoneNumber, @jobTitle, @subRole, @companyId, @ownerPartnerId)

	-- Fetch the next row.

	FETCH NEXT FROM employerCursor
	INTO @id, @contactPhoneNumber, @jobTitle, @subRole, @companyId, @ownerPartnerId, @organisationName
END

CLOSE employerCursor
DEALLOCATE employerCursor

GO
