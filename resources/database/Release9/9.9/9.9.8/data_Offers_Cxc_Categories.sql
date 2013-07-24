DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = 'B05178AD-D16B-42FE-94B6-19D6D3BF4C54'

DECLARE @grandParentCategoryId UNIQUEIDENTIFIER
SET @grandParentCategoryId = 'ADD463FA-0C7D-4E75-8AE3-27F35ED28C9D'

-- Update the name

UPDATE
	OfferCategory
SET
	name = 'Contractor Remuneration & Tax Solutions'
WHERE
	id = @grandParentCategoryId

-- Disable one level

UPDATE
	OfferCategory
SET
	enabled = 0
WHERE
	id = 'BE7B1038-5C72-41A6-930B-667C347C5E33'

-- Update the names

UPDATE
	OfferCategory
SET
	name = 'Are you interested in maximising your cash in hand and minimising tax?'
WHERE
	id = '542FA7DD-AFCE-4693-BC0F-B77E93EDF6F6'

UPDATE
	OfferCategory
SET
	name = '(Professional) Help me find a tax effective payroll solution to increase my take home pay'
WHERE
	id = '246FDF7C-D619-4E83-B065-AEC219C8B249'

UPDATE
	OfferCategory
SET
	name = '(Trade) Help me find a tax effective payroll solution to increase my take home pay'
WHERE
	id = 'D45193AD-A2B5-495B-911E-06806C2932C3'