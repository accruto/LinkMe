IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

UPDATE linkme_owner.networker_profile
SET matchEmployerSearches = 0
FROM linkme_owner.networker_profile np INNER JOIN linkme_owner.user_profile up ON up.id = np.id
where up.subRole = 'Passive'

UPDATE linkme_owner.networker_profile
SET matchEmployerSearches = 1
FROM linkme_owner.networker_profile np INNER JOIN linkme_owner.user_profile up ON up.id = np.id
where up.subRole = 'Active'


END
GO