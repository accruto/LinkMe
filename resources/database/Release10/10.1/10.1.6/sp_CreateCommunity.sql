IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateCommunity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateCommunity]
GO

CREATE PROCEDURE CreateCommunity
(
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(100),
	@url NVARCHAR(100),
	@host NVARCHAR(100),
	@flags TINYINT
)
AS

DECLARE @emailDomain NVARCHAR(100)
DECLARE @countryId INT

SET @emailDomain = null
SET @countryId = NULL

-- Vertical

IF NOT EXISTS (SELECT * FROM dbo.Vertical WHERE id = @id)
	INSERT
		dbo.Vertical ( id, name, url, host, countryId )
	VALUES
		( @id, @name, @url, @host, @countryId )
ELSE
	UPDATE
		dbo.Vertical
	SET
		name = @name, url = @url, host = @host, countryId = @countryId
	WHERE
		id = @id

-- Community

IF NOT EXISTS (SELECT * FROM dbo.Community WHERE id = @id)
	INSERT
		dbo.Community ( id, name, emailDomain, flags )
	VALUES
		( @id, @name, @emailDomain, @flags )
ELSE
	UPDATE
		dbo.Community
	SET
		name = @name, emailDomain = @emailDomain, flags = @flags
	WHERE
		id = @id

GO
