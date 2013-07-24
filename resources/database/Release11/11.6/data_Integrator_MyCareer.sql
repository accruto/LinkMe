IF NOT EXISTS (SELECT * FROM dbo.AtsIntegrator WHERE id = 'A52E664C-6282-40B2-A09B-6FBD03A2F5B0')
BEGIN
	INSERT
		dbo.AtsIntegrator (id, name)
	VALUES
		('A52E664C-6282-40B2-A09B-6FBD03A2F5B0', 'MyCareer')
END

IF NOT EXISTS (SELECT * FROM dbo.IntegratorUser WHERE id = 'D50FBB3E-C8F6-4055-8681-D4B27C298173')
BEGIN
	INSERT
		dbo.IntegratorUser (id, username, password, integratorId, permissions)
	VALUES
		('D50FBB3E-C8F6-4055-8681-D4B27C298173', 'MyCareer-jobs', 'X03MO1qnZdYdgyfeuILPmQ==', 'A52E664C-6282-40B2-A09B-6FBD03A2F5B0', 0)
END
	