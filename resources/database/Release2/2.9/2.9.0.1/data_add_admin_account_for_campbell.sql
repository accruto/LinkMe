DECLARE @passwordHash VARCHAR(50)
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA==' -- changemenow

EXEC linkme_owner.CreateAdminUser 'cf1364f3-8ab6-4636-9dbd-da83caaa923f', 'Campbell Admin', @passwordHash,
	'Campbell', 'Sallabank', 'csallabank@linkme.com.au'
GO
