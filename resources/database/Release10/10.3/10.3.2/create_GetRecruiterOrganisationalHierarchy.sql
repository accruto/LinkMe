if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetRecruiterOrganisationalHierarchy]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetRecruiterOrganisationalHierarchy]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetOrganisationalHierarchy]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetOrganisationalHierarchy]
GO

CREATE FUNCTION dbo.GetOrganisationalHierarchy(@organisationId AS UNIQUEIDENTIFIER, @rank INT)
RETURNS @hierarchy TABLE (id UNIQUEIDENTIFIER, idRank INT)
AS
BEGIN

	-- Organisation.

	INSERT @hierarchy (id, idRank) VALUES (@organisationId, @rank)

	-- Parent.

	DECLARE @parentId UNIQUEIDENTIFIER
	SELECT
		@parentId = parentId
	FROM
		dbo.OrganisationalUnit
	WHERE
		id = @organisationId

	IF NOT @parentId IS NULL
		INSERT
			@hierarchy (id, idRank)
		SELECT
			id, idRank
		FROM
			dbo.GetOrganisationalHierarchy(@parentId, @rank + 1)

	RETURN
END
GO

CREATE FUNCTION dbo.GetRecruiterOrganisationalHierarchy(@recruiterId AS UNIQUEIDENTIFIER)
RETURNS @hierarchy TABLE (id UNIQUEIDENTIFIER)
AS
BEGIN

	DECLARE @rankedHierarchy TABLE (id UNIQUEIDENTIFIER, idRank INT)

	-- Recruiter.

	INSERT @rankedHierarchy (id, idRank) VALUES (@recruiterId, 0)

	-- Organisations.

	DECLARE @organisationId UNIQUEIDENTIFIER
	SELECT
		@organisationId = organisationId
	FROM
		dbo.Employer
	WHERE
		id = @recruiterId

	IF NOT @organisationId IS NULL
		INSERT
			@rankedHierarchy (id, idRank)
		SELECT
			id, idRank
		FROM
			dbo.GetOrganisationalHierarchy(@organisationId, 1)

	INSERT INTO
		@hierarchy
	SELECT
		id
	FROM
		@rankedHierarchy
	ORDER BY
		idRank

	RETURN
END
GO
