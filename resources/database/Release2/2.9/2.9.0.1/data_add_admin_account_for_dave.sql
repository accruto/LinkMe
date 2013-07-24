DECLARE @passwordHash VARCHAR(50)
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA==' -- changemenow

EXEC linkme_owner.CreateAdminUser '152e1d41-7b69-4a4c-a5d1-9ec908961949', 'Dave Admin', @passwordHash,
	'Dave', 'Kenyon', 'dkenyon@linkme.com.au'
GO
