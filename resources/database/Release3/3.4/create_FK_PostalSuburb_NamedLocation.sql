
IF NOT EXISTS
	(
		SELECT
			*
		FROM
			sysreferences
		WHERE
			fkeyid = OBJECT_ID(N'dbo.PostalSuburb')
			AND rkeyid = OBJECT_ID(N'dbo.NamedLocation')
	)
BEGIN

	ALTER TABLE
		dbo.PostalSuburb
	ADD CONSTRAINT
		FK_PostalSuburb_NamedLocation
	FOREIGN KEY
		(id)
	REFERENCES
		dbo.NamedLocation (id)

	ALTER TABLE
		dbo.PostalSuburb
	CHECK CONSTRAINT
		FK_PostalSuburb_NamedLocation

END

GO