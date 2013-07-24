IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'employer_purchases'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	CREATE TABLE linkme_owner.employer_purchases (
		id VARCHAR(50),
		employerId VARCHAR(50),
		networkerId VARCHAR(50),
		purchaseDate datetime
		
		CONSTRAINT pk_employer_purchases PRIMARY KEY (id)
	)
END 
GO