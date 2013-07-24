IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_profile'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

UPDATE    linkme_owner.user_profile
SET              subRole =
	(SELECT ep.subRole
	 FROM linkme_owner.employer_profile ep where linkme_owner.user_profile.id = ep.id)


END
GO
