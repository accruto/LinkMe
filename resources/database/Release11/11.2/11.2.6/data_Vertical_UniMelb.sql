SELECT
	*
FROM
	dbo.Vertical

UPDATE
	dbo.Vertical
SET
	host = 'careersportal.graduate.arts.unimelb.edu.au',
	externalLoginUrl = 'https://security.arts.unimelb.edu.au/graduate/careersportal/',
	requiresExternalLogin = 1
WHERE
	name = 'University of Melbourne Arts'
