
IF NOT EXISTS
	(
		SELECT
			*
		FROM
			sysreferences
		WHERE
			fkeyid = OBJECT_ID(N'[dbo].[GeographicalArea]')
			AND rkeyid = OBJECT_ID(N'[dbo].[NamedLocation]')
	)
BEGIN

	ALTER TABLE
		dbo.GeographicalArea
	ADD CONSTRAINT
		FK_GeographicalArea_NamedLocation
	FOREIGN KEY
		(id)
	REFERENCES
		dbo.NamedLocation (id)

	ALTER TABLE
		dbo.GeographicalArea
	CHECK CONSTRAINT
		FK_GeographicalArea_NamedLocation

END

GO