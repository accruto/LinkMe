WHILE EXISTS (SELECT * FROM dbo.RegisteredUser WHERE firstName LIKE '%  %')
BEGIN
	UPDATE dbo.RegisteredUser
	SET firstName = REPLACE(firstName, '  ', ' ')
	WHERE firstName LIKE '%  %'
END

WHILE EXISTS (SELECT * FROM dbo.RegisteredUser WHERE lastName LIKE '%  %')
BEGIN
	UPDATE dbo.RegisteredUser
	SET lastName = REPLACE(lastName, '  ', ' ')
	WHERE lastName LIKE '%  %'
END

GO
