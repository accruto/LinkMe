IF NOT EXISTS (SELECT * FROM dbo.AtsIntegrator WHERE id = '{CC10A928-A9C5-4DB3-8936-8F8EC0CB456E}')
BEGIN
	INSERT
		dbo.AtsIntegrator (id, name)
	VALUES
		('{CC10A928-A9C5-4DB3-8936-8F8EC0CB456E}', 'Dewr')

	INSERT
		dbo.IntegratorUser (id, username, password, integratorId, permissions)
	VALUES
		('{E075F2E8-C608-465E-AD3B-96D156C7C451}', 'Dewr-jobs', '************************', '{CC10A928-A9C5-4DB3-8936-8F8EC0CB456E}', 0)
END
	