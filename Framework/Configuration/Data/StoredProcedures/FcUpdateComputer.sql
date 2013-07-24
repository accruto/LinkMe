-------------------------------------------------------------------------------
-- Version: 1.0.0.0
-------------------------------------------------------------------------------


-------------------------------------------------------------------------------
-- Drop

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FcUpdateComputer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FcUpdateComputer]
GO

-------------------------------------------------------------------------------
-- Create

CREATE PROCEDURE dbo.FcUpdateComputer
(
	@domainName AS VARCHAR(128),
	@name AS VARCHAR(128),
	@description AS VARCHAR(512)
)
AS
BEGIN

SET NOCOUNT ON

-- Try to find the computer.

IF NOT EXISTS (SELECT * FROM dbo.FcComputer WHERE domainName = @domainName AND name = @name)
BEGIN
	-- Insert this computer.

	INSERT INTO dbo.FcComputer (domainName, name, description)
	VALUES (@domainName, @name, @description)
END
ELSE
BEGIN
	-- Computer exists - update it.

	UPDATE dbo.FcComputer
	SET
		description = @description
	WHERE domainName = @domainName AND name = @name
END

RETURN 0

END
GO
