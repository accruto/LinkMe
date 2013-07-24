DELETE
	dbo.CommunicationDefinition
WHERE
	name IN ('ExpiringApplicantCreditsEmail', 'ExpiringContactCreditsEmail', 'ExpiringJobAdCreditsEmail', 'ExpiringUnlimitedContactCreditsEmail')

INSERT
	dbo.CommunicationDefinition (id, name, categoryId)
VALUES
	('{E0FE4873-DB93-4fd0-AD0E-F4CC043894A3}', 'ExpiringApplicantCreditsEmail', '2C237669-B320-494B-8142-15B0A5FACBFA')

INSERT
	dbo.CommunicationDefinition (id, name, categoryId)
VALUES
	('{7714C367-AA1C-463d-950E-BA302D09CCA7}', 'ExpiringContactCreditsEmail', '2C237669-B320-494B-8142-15B0A5FACBFA')

INSERT
	dbo.CommunicationDefinition (id, name, categoryId)
VALUES
	('{A236ED0B-77B0-40bf-83F4-682491E82AA6}', 'ExpiringJobAdCreditsEmail', '2C237669-B320-494B-8142-15B0A5FACBFA')

INSERT
	dbo.CommunicationDefinition (id, name, categoryId)
VALUES
	('{D0BE909D-9061-438d-B593-5F0EAC6F102D}', 'ExpiringUnlimitedContactCreditsEmail', '2C237669-B320-494B-8142-15B0A5FACBFA')