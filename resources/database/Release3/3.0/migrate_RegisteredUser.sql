-- Some user manage to enter over 30 characters for their names. Truncate them at the first space.

UPDATE linkme_owner.user_profile
SET firstName = LEFT(firstName, CHARINDEX(' ', firstName) - 1)
WHERE LEN(firstName) > 30

UPDATE linkme_owner.user_profile
SET lastName = LEFT(lastName, CHARINDEX(' ', lastName) - 1)
WHERE LEN(lastName) > 30

GO

/* Converting various bit fields to flags is where it gets a bit ugly. Flags mappings:
	EmailVerified = emailVerified
	MustChangePassword = newUserMustChangePassword
	Inactive = NOT active
	SendPlainTextEmail = NOT preferredHtmlEmailFormat
	SendNewsletters = NOT OptOutOfNewsletters
	Activated = TRUE for all existing users
*/

INSERT INTO dbo.RegisteredUser([id], loginId, createdTime, emailAddress, firstName, lastName, flags,
	passwordHash)
SELECT dbo.GuidFromString(up.[id]) AS [id], up.userId, up.joinDate,
	COALESCE(ep.emailAddress, ap.emailAddress, up.userId) AS emailAddress, LTRIM(RTRIM(firstName)), LTRIM(RTRIM(lastName)),
	emailVerified | CASE newUserMustChangePassword WHEN 1 THEN 2 ELSE 0 END
		| CASE active WHEN 0 THEN 4 ELSE 0 END | CASE preferredHtmlEmailFormat WHEN 0 THEN 8 ELSE 0 END
		| CASE OptOutOfNewsletters WHEN 0 THEN 16 ELSE 0 END
		| 32 AS flags,
	ISNULL([password], '')
FROM linkme_owner.user_profile up
LEFT OUTER JOIN linkme_owner.employer_profile ep
ON up.[id] = ep.[id]
LEFT OUTER JOIN linkme_owner.administrator_profile ap
ON up.[id] = ap.[id]
GO
