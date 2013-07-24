IF NOT EXISTS (SELECT * FROM dbo.AtsIntegrator WHERE id = '{10ABCEE8-B3A6-4995-97BC-0C9C1F3067A8}')
BEGIN
	INSERT
		dbo.AtsIntegrator (id, name)
	VALUES
		('{10ABCEE8-B3A6-4995-97BC-0C9C1F3067A8}', 'HrCareers')

	INSERT
		dbo.IntegratorUser (id, username, password, integratorId, permissions)
	VALUES
		('{378F2700-4512-4F84-8692-4C0D3B915A2B}', 'HrCareers-jobs', '************************', '{10ABCEE8-B3A6-4995-97BC-0C9C1F3067A8}', 0)
END
	