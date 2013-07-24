UPDATE dbo.RegisteredUser
SET loginId = LTRIM(RTRIM(loginId))
WHERE loginId LIKE ' %' OR loginId LIKE '% '
GO
