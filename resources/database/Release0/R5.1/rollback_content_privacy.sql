IF EXISTS (SELECT 1 FROM linkme_owner.Content WHERE contentKey = 'aboutus.privacy')
BEGIN

	DELETE FROM linkme_owner.Content WHERE contentKey = 'aboutus.privacy'

END
