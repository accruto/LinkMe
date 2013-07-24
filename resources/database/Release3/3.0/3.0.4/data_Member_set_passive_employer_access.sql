UPDATE dbo.Member
SET employerAccess = 1
FROM dbo.Member new
INNER JOIN LinkMe29.linkme_owner.user_profile old
ON new.[id] = dbo.GuidFromString(old.[id])
WHERE old.subRole = 'Passive'
