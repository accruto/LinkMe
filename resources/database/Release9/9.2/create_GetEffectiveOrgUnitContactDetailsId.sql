IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].GetEffectiveOrgUnitContactDetailsId') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].GetEffectiveOrgUnitContactDetailsId
GO

CREATE FUNCTION dbo.GetEffectiveOrgUnitContactDetailsId(@orgUnitId AS UNIQUEIDENTIFIER)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @primaryContactId UNIQUEIDENTIFIER
	DECLARE @parentId UNIQUEIDENTIFIER

	IF (@orgUnitId IS NULL)
		RETURN NULL

	SELECT @primaryContactId = primaryContactId, @parentId = parentId
	FROM dbo.OrganisationalUnit
	WHERE [id] = @orgUnitId

	IF (@primaryContactId IS NOT NULL)
		RETURN @primaryContactId

	RETURN dbo.GetEffectiveOrgUnitContactDetailsId(@parentId)
END
GO
