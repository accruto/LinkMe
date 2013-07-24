/*
SELECT
	employerAccess,
	dbo.GetProfessionalVisibilityDisplayText(employerAccess),
	employerAccess | 128,
	dbo.GetProfessionalVisibilityDisplayText(employerAccess | 128)
FROM
	dbo.Member
*/

UPDATE
	dbo.Member
SET
	employerAccess = employerAccess | 128
WHERE
	(employerAccess & 1) = 1