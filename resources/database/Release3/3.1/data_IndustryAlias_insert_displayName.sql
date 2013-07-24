IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Advert./Media/Entertain.')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{3DE20A01-2593-4aaa-9F44-8229F834C3B3}', id, 'Advert./Media/Entertain.'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Advertising, Media & Entertainment'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Banking & Fin. Services')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{E4873BD9-6F2D-4cab-A633-F7160290B0FC}', id, 'Banking & Fin. Services'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Banking & Financial Services'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Consulting & Corp. Strategy')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{C4EF4E71-809B-4d48-923D-F89C0DA9DA33}', id, 'Consulting & Corp. Strategy'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Consulting & Corporate Strategy'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Manufacturing/Operations')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{95B8409C-8075-4aa8-BE4D-768F7D56CC79}', id, 'Manufacturing/Operations'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Manufacturing & Operations'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Call Centre/Cust. Service')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{F1FCF4AF-E619-498d-86BB-B5110781CFB9}', id, 'Call Centre/Cust. Service'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Call Centre & Customer Service'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Government/Defence')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{9D86EEBC-DA87-473f-8CF5-3372F0E0B8E6}', id, 'Government/Defence'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Government & Defence'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'I.T. & T')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{ED12AD3D-548D-4533-9245-B909D26EF34F}', id, 'I.T. & T'
	FROM
		dbo.Industry
	WHERE
		displayName = 'IT & Telecommunications'

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Retail & Consumer Prods.')
	INSERT
		dbo.IndustryAlias
	SELECT
		'{297EEAF3-B3E4-4bc3-BB0C-DF0D3DF8B4AC}', id, 'Retail & Consumer Prods.'
	FROM
		dbo.Industry
	WHERE
		displayName = 'Retail & Consumer Products'

GO
