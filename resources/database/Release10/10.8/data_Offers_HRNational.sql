UPDATE
	dbo.OfferCategory
SET
	name = 'I am a recruitment professional seeking greater independance and the opportunity to make more money'
WHERE
	id = '7CA13286-941F-487A-A632-435236AAF803'

UPDATE
	dbo.Offering
SET
	name = 'I am a recruitment professional seeking greater independance and the opportunity to make more money'
WHERE
	id = 'C0A1738B-F857-40A0-B167-53AF4163AC3F'

UPDATE
	dbo.OfferCategory
SET
	enabled = 0
WHERE
	id IN ('6845262B-11F8-4A87-AE5A-E4E9A30019A8', 'D948209D-762F-4690-9A5B-7FB335275946', '16A03AF1-38B3-4165-B760-02B798ED1C17', 'C5F076D1-7EDF-4D09-9603-2F989477DE1F', 'CC440914-A16E-4EAF-8F8E-6B9F27C357B1', '00145E35-5E5B-4B54-AF83-5E0A235BE670', '81FD9B48-A758-4D9F-8DCC-2E5E5AE6F76A')

UPDATE
	dbo.Offering
SET
	enabled = 0
WHERE
	id IN ('5D26D39A-8854-4D38-BF66-B89974979DCF', 'D96675BE-3A31-4B0A-BD42-9BDC16E6E14C')