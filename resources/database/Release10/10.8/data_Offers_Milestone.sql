UPDATE
	dbo.OfferCategory
SET
	name = 'Leadership & Sales Courses'
WHERE
	id = '016A322C-77F2-4B6D-B600-5E8629202E2B'

IF NOT EXISTS (SELECT * FROM dbo.OfferingCriteria WHERE id = '6AC924A5-F706-45DB-B641-4383B176D71F' AND name = 'JobTitle')
	INSERT
		dbo.OfferingCriteria (id, name, value)
	VALUES
		('6AC924A5-F706-45DB-B641-4383B176D71F', 'JobTitle', 'Sales Manager')
ELSE
	UPDATE
		dbo.OfferingCriteria
	SET
		value = 'Sales Manager'
	WHERE
		id = '6AC924A5-F706-45DB-B641-4383B176D71F'
		AND name = 'JobTitle'

IF NOT EXISTS (SELECT * FROM dbo.OfferingCriteria WHERE id = '9506C9FF-B408-41E9-A4CB-75960570D12C' AND name = 'Keywords')
	INSERT
		dbo.OfferingCriteria (id, name, value)
	VALUES
		('9506C9FF-B408-41E9-A4CB-75960570D12C', 'Keywords', '(Sales OR BDM OR "Account Manager")')
ELSE
	UPDATE
		dbo.OfferingCriteria
	SET
		value = '(Sales OR BDM OR "Account Manager")'
	WHERE
		id = '9506C9FF-B408-41E9-A4CB-75960570D12C'
		AND name = 'Keywords'


