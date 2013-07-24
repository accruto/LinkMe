-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateDomainVariable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateDomainVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateDomainVariable
(
	@domainName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@description AS VARCHAR(512),
	@type AS VARCHAR(128),
	@value AS TEXT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcDomainVariable WHERE domainName = @domainName AND name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcDomainVariable (domainName, name, description, type, value)
	VALUES (@domainName, @name, @description, @type, @value)
END
ELSE
BEGIN
	-- DomainVariable exists - update it.

	UPDATE dbo.FcDomainVariable
	SET
		description = @description,
		type = @type,
		value = @value
	WHERE domainName = @domainName AND name = @name
END

RETURN 0

END
GO
