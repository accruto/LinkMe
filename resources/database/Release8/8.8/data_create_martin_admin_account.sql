DECLARE @passwordHash VARCHAR(50)
SET @passwordHash = 'X03MO1qnZdYdgyfeuILPmQ=='

EXEC dbo.CreateAdminUser 'e2517419-49af-442f-a102-ec93f9d4ff86', 'Martin Admin', @passwordHash,
	'Martin', 'Funcich', 'mfuncich@linkme.com.au'