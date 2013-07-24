DELETE dbo.ContactDetails
FROM dbo.ContactDetails cd
WHERE NOT EXISTS
(
	SELECT *
	FROM dbo.JobAd ja
	WHERE ja.contactDetailsId = cd.[id]
)
GO

DELETE dbo.Person
FROM dbo.Person p
WHERE NOT EXISTS
(
	SELECT *
	FROM dbo.JobAd ja
	WHERE ja.contactPersonId = p.[id]
)
GO
