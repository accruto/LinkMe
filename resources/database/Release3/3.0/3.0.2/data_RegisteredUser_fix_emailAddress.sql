UPDATE dbo.RegisteredUser
SET emailAddress = LEFT(emailAddress, LEN(emailAddress) - 1)
WHERE emailAddress LIKE '%@%.'

UPDATE dbo.RegisteredUser
SET loginId = LEFT(loginId, LEN(loginId) - 1)
WHERE loginId LIKE '%@%.'

GO
