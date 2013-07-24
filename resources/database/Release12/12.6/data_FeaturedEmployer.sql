UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'IAG'

DECLARE @id UNIQUEIDENTIFIER
SET @id = '{FF1395B3-ACC2-4d3a-988A-B9318CF37391}'

IF EXISTS (SELECT * FROM dbo.FeaturedEmployer WHERE id = @id)
	UPDATE
		dbo.FeaturedEmployer
	SET
		name = 'Talent2',
		logoUrl = '~/content/images/homepage/jobs-by-employers/new-t2.png',
		logoOrder = 2,
		enabled = 1
	WHERE
		id = @id
ELSE
	INSERT
		dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
	VALUES
		(@id, 'Talent2', '~/content/images/homepage/jobs-by-employers/new-t2.png', 2, 1)

UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 3
WHERE
	name = 'Beilby'

UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 4
WHERE
	name = 'IPA'


UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 5
WHERE
	name = 'Vita Group'


UPDATE
	dbo.FeaturedEmployer
SET
	logoOrder = 6
WHERE
	name = 'Michael Page'

UPDATE
	dbo.FeaturedEmployer
SET
	enabled = 0
WHERE
	name = 'Michael Page'

IF NOT EXISTS (SELECT * FROM dbo.FeaturedEmployer WHERE id = '{DC147F53-C7F1-4e91-8291-7E4F74C16E54}')
	INSERT
		dbo.FeaturedEmployer (id, name, logoUrl, logoOrder, enabled)
	VALUES
		('{DC147F53-C7F1-4e91-8291-7E4F74C16E54}', 'Vodafone', '~/content/images/homepage/jobs-by-employers/vodafone.png', 6, 1)

SELECT
	*
FROM
	dbo.FeaturedEmployer

