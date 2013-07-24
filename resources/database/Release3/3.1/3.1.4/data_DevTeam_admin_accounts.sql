DECLARE @passwordHash VARCHAR(50)
SET @passwordHash = 'lXfmJw+uCeOy/Qw34bvf0A=='

EXEC dbo.CreateAdminUser 'd2517419-49af-442f-a102-ec93f9d4ff87', 'Alex Admin', @passwordHash,
	'Alex', 'Chiviliov', 'achiviliov@linkme.com.au'
EXEC dbo.CreateAdminUser '6072e2db-5e24-4ccb-990d-72685d5c1c8a', 'ajw', @passwordHash,
	'Alan', 'Williams', 'awilliams@linkme.com.au'
EXEC dbo.CreateAdminUser '9a193e05-1485-4301-b9c0-13d2b26b1918', 'Andrew Admin', @passwordHash,
	'Andrew', 'Cantos', 'acantos@linkme.com.au'
EXEC dbo.CreateAdminUser '09adc7d6-0dca-4e5b-b5ee-93e6a13f760d', 'Art Admin', @passwordHash,
	'Artem', 'Skvria', 'askvira@linkme.com.au'
EXEC dbo.CreateAdminUser 'f79728f6-4071-43ea-a17a-1ad1869bec35', 'DH Admin', @passwordHash,
	'Dave', 'Hodgman', 'dhodgman@linkme.com.au'
EXEC dbo.CreateAdminUser '1bff6396-c939-4669-9429-85a1dd2225bc', 'Evgeny Admin', @passwordHash,
	'Evgeny', 'Potashnik', 'epotashnik@linkme.com.au'
EXEC dbo.CreateAdminUser '5bbb7898-dd6e-4ca0-8378-bcd8c0a6cdb7', 'Khan Admin', @passwordHash,
	'Khan', 'Thompson', 'kthompson@linkme.com.au'
EXEC dbo.CreateAdminUser 'c4bfa16a-6507-4c0a-82cc-749efb366349', 'Lucas Admin', @passwordHash,
	'Lucas', 'Richter', 'lrichter@linkme.com.au'
EXEC dbo.CreateAdminUser 'e4628b6e-5c2a-443e-b456-df148f49790a', 'Marcelo Admin', @passwordHash,
	'Marcelo', 'Cantos', 'mcantos@linkme.com.au'
EXEC dbo.CreateAdminUser '2e7d03b6-37e2-4d12-89f3-ffb36b939509', 'Matthew Admin', @passwordHash,
	'Matthew', 'Wilson', 'mwilson@linkme.com.au'
EXEC dbo.CreateAdminUser '3a893c95-86d9-431a-aca1-bcaf12998edf', 'MS Admin', @passwordHash,
	'Michelle', 'Spencer', 'mspencer@linkme.com.au'
EXEC dbo.CreateAdminUser 'ae6e9ee9-be57-41bb-a62b-3d32ee772bc9', 'PT Admin', @passwordHash,
	'Paul', 'Tyrrell', 'ptyrrell@linkme.com.au'
EXEC dbo.CreateAdminUser '583b84a1-76aa-488e-959e-f69f1d5e28b0', 'Ray Admin', @passwordHash,
	'Raymond', 'Low', 'rlow@linkme.com.au'

GO
