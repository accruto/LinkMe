-- Alias used by JobG8

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Advert / Media / Entertainment')
	INSERT dbo.IndustryAlias
		(id, industryId, displayName)
	VALUES
		('00680c25-7b58-4422-af40-b7ee77d82ea2', 'c4b0906b-0c58-4906-b2e7-7d198dd1f0cd', 'Advert / Media / Entertainment')

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'Call Centre / CustomerService')
	INSERT dbo.IndustryAlias
		(id, industryId, displayName)
	VALUES
		('9889C828-BF0A-4b7f-BA97-D9F97AE713E9', '9ec2f049-e169-413d-ad85-8cd83dd8467e', 'Call Centre / CustomerService')

IF NOT EXISTS (SELECT * FROM dbo.IndustryAlias WHERE displayName = 'HR / Recruitment')
	INSERT dbo.IndustryAlias
		(id, industryId, displayName)
	VALUES
		('09044963-B47D-4974-920F-9E1D55AEB8DD', '995542b4-11f8-401e-b288-300fc9f6e376', 'HR / Recruitment')