IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 0)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(0, 'Other')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 1)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(1, 'Outdoor advertisement')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 2)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(2, 'Conference')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 3)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(3, 'TV')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 4)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(4, 'Handout')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 5)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(5, 'Email')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 6)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(6, 'Newspaper')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 8)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(8, 'Radio')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 9)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(9, 'Friend or colleague')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 10)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(10, 'Internet/Web')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 11)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(11, 'Virgin Blue - inflight ad')

IF NOT EXISTS (SELECT * FROM dbo.ReferralSource WHERE id = 12)
	INSERT
		dbo.ReferralSource (id, displayName)
	VALUES
		(12, 'Trading Post print ad')