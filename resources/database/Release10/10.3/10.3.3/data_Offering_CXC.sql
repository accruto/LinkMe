UPDATE
	dbo.OfferCategory
SET
	parentId = 'ADD463FA-0C7D-4E75-8AE3-27F35ED28C9D'
WHERE
	name = '(Professional) Help me find a tax effective payroll solution to increase my take home pay'
	OR name = '(Trade) Help me find a tax effective payroll solution to increase my take home pay'	

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	name = 'Are you interested in maximising your cash in hand and minimising tax?'

EXEC dbo.CreateOffering '{C6C4C131-A432-44f4-9115-44C8BFED9797}', '(Trade) Help me find a tax effective payroll solution to increase my take home pay', 'B05178AD-D16B-42FE-94B6-19D6D3BF4C54', 'D45193AD-A2B5-495B-911E-06806C2932C3', 'ADD463FA-0C7D-4E75-8AE3-27F35ED28C9D'

