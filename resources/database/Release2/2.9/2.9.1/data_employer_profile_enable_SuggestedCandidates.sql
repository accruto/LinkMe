UPDATE linkme_owner.employer_profile
SET flags = 5
FROM linkme_owner.employer_profile ep
INNER JOIN linkme_owner.user_profile up
ON ep.id = up.id
WHERE NOT (up.firstName = 'Linking' AND up.lastName = 'Node')
GO
