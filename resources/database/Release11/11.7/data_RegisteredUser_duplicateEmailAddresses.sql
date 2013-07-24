SELECT
	id, emailAddress, emailAddressVerified, secondaryEmailAddress, secondaryEmailAddressVerified
FROM
	dbo.RegisteredUser
WHERE
	emailAddress = secondaryEmailAddress

UPDATE
	dbo.RegisteredUser
SET
	secondaryEmailAddress = NULL,
	secondaryEmailAddressVerified = 0
WHERE
	emailAddress = secondaryEmailAddress