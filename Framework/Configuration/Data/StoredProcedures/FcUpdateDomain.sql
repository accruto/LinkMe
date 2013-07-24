-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateDomain]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateDomain]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateDomain
(
	@name AS VARCHAR(128),
	@description AS VARCHAR(512),
	@isDefault AS BIT
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the domain.

IF NOT EXISTS (SELECT * FROM dbo.FcDomain WHERE name = @name)
BEGIN
	-- Insert this domain.

	INSERT INTO dbo.FcDomain (name, description, isDefault)
	VALUES (@name, @description, @isDefault)
END
ELSE
BEGIN
	-- Domain exists - update it.

	UPDATE dbo.FcDomain
	SET
		description = @description,
		isDefault = @isDefault
	WHERE name = @name
END

RETURN 0

END
GO
