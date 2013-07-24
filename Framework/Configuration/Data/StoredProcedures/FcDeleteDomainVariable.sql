-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcDeleteDomainVariable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcDeleteDomainVariable]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcDeleteDomainVariable
(
	@domainName AS VARCHAR(128),
	@name AS VARCHAR(128)
)
AS
BEGIN

SET NOCOUNT ON

DECLARE @error AS INT

-- Delete this category.

DELETE FROM FcDomainVariable
WHERE domainName = @domainName AND name = @name

SET @error = @@ERROR
IF @error != 0
BEGIN
	RAISERROR('Error %d occurred whilst trying to delete repository ''%s''.', 16, 1, @error, @name)
	RETURN 1
END

RETURN 0

END
GO
