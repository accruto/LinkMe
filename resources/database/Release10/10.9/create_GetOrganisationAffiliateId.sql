IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrganisationAffiliateId]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetOrganisationAffiliateId]
GO

CREATE FUNCTION [dbo].[GetOrganisationAffiliateId]
(
	@id AS UNIQUEIDENTIFIER
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @affiliateId UNIQUEIDENTIFIER
	IF (@id IS NULL)
		RETURN NULL

	SELECT
		@affiliateId = a.communityId
	FROM
		dbo.CommunityAssociation AS a
	INNER JOIN
		dbo.CommunityOrganisationalUnit AS o ON o.id = a.organisationalUnitId
	WHERE
		o.id = @id

	RETURN @affiliateId
END
GO

GRANT EXECUTE ON [dbo].[GetOrganisationAffiliateId] TO public
GO
