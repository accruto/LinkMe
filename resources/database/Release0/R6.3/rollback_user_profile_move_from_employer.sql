IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'employer_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

UPDATE    linkme_owner.employer_profile
SET              subRole =
	(SELECT up.subRole
	 FROM linkme_owner.user_profile up where linkme_owner.employer_profile.id = up.id)


END
GO
