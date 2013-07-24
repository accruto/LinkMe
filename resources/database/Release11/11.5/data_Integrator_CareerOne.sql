IF NOT EXISTS (SELECT * FROM dbo.AtsIntegrator WHERE id = 'B13FE5D6-C425-4120-A25E-7F5DAC643F6B')
BEGIN
	INSERT
		dbo.AtsIntegrator (id, name)
	VALUES
		('B13FE5D6-C425-4120-A25E-7F5DAC643F6B', 'CareerOne')

	INSERT
		dbo.IntegratorUser (id, username, password, integratorId, permissions)
	VALUES
		('9B732ACD-17EA-4500-8011-59952466E15D', 'CareerOne-jobs', '************************', 'B13FE5D6-C425-4120-A25E-7F5DAC643F6B', 0)
END
	