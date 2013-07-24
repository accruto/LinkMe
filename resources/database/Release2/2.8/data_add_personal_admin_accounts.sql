DECLARE @passwordHash VARCHAR(50)
SET @passwordHash = '4C+YKJIYzi7OVxqXoB3TfA==' -- changemenow

EXEC linkme_owner.CreateAdminUser '9879a0b3-e9e7-4f31-9510-49e7482494c9', 'Lucy Admin', @passwordHash,
	'Lucy', 'Stow', 'lstow@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '7e865a6e-6a57-4e21-8380-b33b7bb0760b', 'Sonja Admin', @passwordHash,
	'Sonja', 'Butler', 'sbutler@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '0a6daba6-120b-46de-b297-4c1680a59bd0', 'Lee Admin', @passwordHash,
	'Lee', 'Harvison', 'lharvison@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '9e129ca6-cdcd-445b-970f-cd7a60478f06', 'Corinne Admin', @passwordHash,
	'Corinne', 'Cornette', 'ccornette@linkme.com.au'
EXEC linkme_owner.CreateAdminUser '1828b733-c70d-4173-9547-411ecafa9a5d', 'Stuart Admin', @passwordHash,
	'Stuart', 'Atkins', 'satkins@linkme.com.au'
EXEC linkme_owner.CreateAdminUser 'ffa9523c-9fa1-4589-8a39-1c519c00c5f1', 'Kathy Admin ', @passwordHash,
	'Kathy', 'Angelone', 'kangelone@linkme.com.au'
EXEC linkme_owner.CreateAdminUser 'f9a107ba-3f32-403e-92cd-5653dcb8d738', 'Alanna Admin', @passwordHash,
	'Alanna', 'Koo', 'akoo@linkme.com.au'

GO
