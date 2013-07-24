IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'credit_card_surcharge'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

PRINT 'primary key errors are ok unless this is the first time run'

	INSERT INTO linkme_owner.credit_card_surcharge 
	VALUES
	('17e9f58cb7eb448ea95c17503cc08fc7',2,1, 0.0345, 0)


	INSERT INTO linkme_owner.credit_card_surcharge 
	VALUES
	('5dc15749c9f2491b801fc8ae3013c129',4,1, 0.0293, 0)
END
GO