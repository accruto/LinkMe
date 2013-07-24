CREATE PROCEDURE UpdateOffering
(
	@oldName NVARCHAR(200),
	@newname NVARCHAR(200)
)
AS

BEGIN
	UPDATE
		dbo.Offering
	SET
		name = @newName
	WHERE
		name = @oldName

	UPDATE
		dbo.OfferCategory
	SET
		name = @newName
	WHERE
		name = @oldName
END

GO

DECLARE @locationReferenceId UNIQUEIDENTIFIER

EXEC CreateOfferingLocation 'A9E8F910-F3F2-4EAB-9667-74E37E3E4501', '{A7735408-7A7F-44f1-B70E-8B48C00DBBA2}', 'VIC', 18
EXEC CreateOfferingLocation '933A71DD-32C5-4FC0-80D2-E84BB310E307', '{7A562D79-D2D9-4463-B311-DC83FE49FF5A}', 'VIC', 18
EXEC CreateOfferingLocation 'EF21A03E-5323-4A7F-8E19-FAD11BAF3CD8', '{F8ABBA06-3EC4-4559-A612-3394CEFA9E88}', 'VIC', 18

GO

DROP PROCEDURE UpdateOffering
