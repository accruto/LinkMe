IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VerticalFavicon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[VerticalFavicon]
GO

CREATE PROCEDURE [dbo].[VerticalFavicon]
(
	@vertical NVARCHAR(256),
	@visible INT,
	@rootFolder NVARCHAR(256),
	@relativePath NVARCHAR(256)
)
AS
BEGIN

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT

-- Favicon

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = 'Favicon' AND Vertical = @vertical)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible, Page)
	VALUES
		('Favicon', NULL, 'ImageContentItem', @vertical, 0, @date, @date, @visible, NULL)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'RootFolder', @rootFolder)

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'RelativePath', @relativePath)

END

END

GO

EXEC dbo.VerticalFavicon 'Scouts Australia', 0, '~/themes/communities/scouts/img/', ''
EXEC dbo.VerticalFavicon 'AHRI HRcareers Network', 0, '~/themes/communities/ahri/img/', ''
EXEC dbo.VerticalFavicon 'Association of Accounting Technicians Australia', 0, '~/themes/communities/aat/img/', ''
EXEC dbo.VerticalFavicon 'Astute Career Network', 0, '~/themes/communities/astutecareers/img/', ''
EXEC dbo.VerticalFavicon 'Australian Institute of Marine and Power Engineers', 0, '~/themes/communities/aimpe/img/', ''
EXEC dbo.VerticalFavicon 'Autopeople', 0, '~/themes/communities/autopeople/img/', ''
EXEC dbo.VerticalFavicon 'Crossroads Human Resources', 0, '~/themes/communities/crossroads/img/', ''
EXEC dbo.VerticalFavicon 'Finsia Career Network', 0, '~/themes/communities/finsia/img/', ''
EXEC dbo.VerticalFavicon 'golfjobs.com.au', 0, '~/themes/communities/pga/img/', ''
EXEC dbo.VerticalFavicon 'ITWire', 0, '~/themes/communities/itwire/img/', ''
EXEC dbo.VerticalFavicon 'Live In Australia Careers Community', 0, '~/themes/communities/liveInAustralia/img/', ''
EXEC dbo.VerticalFavicon 'Marketing Association of Australia and New Zealand', 0, '~/themes/communities/maanz/img/', ''
EXEC dbo.VerticalFavicon 'Monash University Graduate School of Business', 0, '~/themes/communities/monash/gsb/img/', ''
EXEC dbo.VerticalFavicon 'New Zealand', 0, '~/themes/communities/nz/img/', ''
EXEC dbo.VerticalFavicon 'RCSA Australia & New Zealand', 0, '~/themes/communities/rcsa/img/', ''
EXEC dbo.VerticalFavicon 'The Red Tent Woman', 0, '~/themes/communities/theredtentwoman/img/', ''
EXEC dbo.VerticalFavicon '1CN People', 0, '~/themes/communities/onecarenetwork/img/', ''

EXEC dbo.VerticalFavicon 'Business Spectator', 1, '~/themes/communities/businessspectator/img/', 'favicon.ico'

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VerticalFavicon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[VerticalFavicon]
GO
