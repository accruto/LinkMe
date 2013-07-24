IF NOT EXISTS (SELECT * FROM dbo.AtsIntegrator WHERE id = '{40A25B33-0243-4C30-B9B0-25802B3EB7C8}')
BEGIN
	INSERT
		dbo.AtsIntegrator (id, name)
	VALUES
		('{40A25B33-0243-4C30-B9B0-25802B3EB7C8}', 'Adzuna')

	INSERT
		dbo.IntegratorUser (id, username, password, integratorId, permissions)
	VALUES
		('{DFE67046-9BCA-4769-AA62-EBD82000C057}', 'Adzuna-jobs', 'mPaQdNc5zEBSRw37d4HdZw==', '{40A25B33-0243-4C30-B9B0-25802B3EB7C8}', 4)
END
	