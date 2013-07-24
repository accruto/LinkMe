
IF NOT EXISTS
	(
		SELECT
			*
		FROM
			sysreferences
		WHERE
			fkeyid = OBJECT_ID(N'dbo.PostalCode')
			AND rkeyid = OBJECT_ID(N'dbo.NamedLocation')
	)
BEGIN

	ALTER TABLE
		dbo.PostalCode
	ADD CONSTRAINT
		FK_PostalCode_NamedLocation
	FOREIGN KEY
		(id)
	REFERENCES
		dbo.NamedLocation (id)

	ALTER TABLE
		dbo.PostalCode
	CHECK CONSTRAINT
		FK_PostalCode_NamedLocation

END

GO