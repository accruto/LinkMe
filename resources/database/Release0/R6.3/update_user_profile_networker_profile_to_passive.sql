IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

UPDATE linkme_owner.networker_profile
SET receiveJobEmails = 0
FROM linkme_owner.networker_profile np INNER JOIN linkme_owner.user_profile up ON up.id = np.id
where np.matchEmployerSearches = 0 and up.subRole is NULL

UPDATE linkme_owner.user_profile
SET subRole = 'Passive'
FROM linkme_owner.networker_profile np INNER JOIN linkme_owner.user_profile up ON up.id = np.id
where np.matchEmployerSearches = 0 and up.subRole is NULL

UPDATE linkme_owner.user_profile
SET subRole = 'Active'
FROM linkme_owner.networker_profile np INNER JOIN linkme_owner.user_profile up ON up.id = np.id
where np.matchEmployerSearches = 1 and up.subRole is NULL

END
GO