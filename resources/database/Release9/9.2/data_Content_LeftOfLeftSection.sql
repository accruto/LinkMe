IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommunityLeftOfLeftSection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommunityLeftOfLeftSection]
GO

CREATE PROCEDURE [dbo].[CommunityLeftOfLeftSection]
(
	@community NVARCHAR(256)
)
AS
BEGIN

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT
DECLARE @page NVARCHAR(256)

SET @page = 'au.com.venturelogic.linkme.web.default'

-- HomePageLeftOfLeftSection

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = 'Home page left of left section' AND Community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		('Home page left of left section', NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')

END

END

GO

EXEC dbo.CommunityLeftOfLeftSection 'Scouts Australia'
EXEC dbo.CommunityLeftOfLeftSection 'AHRI HRcareers Network'
EXEC dbo.CommunityLeftOfLeftSection 'Association of Accounting Technicians Australia'
EXEC dbo.CommunityLeftOfLeftSection 'Astute Career Network'
EXEC dbo.CommunityLeftOfLeftSection 'Australian Institute of Marine and Power Engineers'
EXEC dbo.CommunityLeftOfLeftSection 'Autopeople'
EXEC dbo.CommunityLeftOfLeftSection 'Business Spectator'
EXEC dbo.CommunityLeftOfLeftSection 'Crossroads Human Resources'
EXEC dbo.CommunityLeftOfLeftSection 'Finsia Career Network'
EXEC dbo.CommunityLeftOfLeftSection 'golfjobs.com.au'
EXEC dbo.CommunityLeftOfLeftSection 'ITWire'
EXEC dbo.CommunityLeftOfLeftSection 'Live In Australia Careers Community'
EXEC dbo.CommunityLeftOfLeftSection 'Marketing Association of Australia and New Zealand'
EXEC dbo.CommunityLeftOfLeftSection 'Monash University Graduate School of Business'
EXEC dbo.CommunityLeftOfLeftSection 'New Zealand'
EXEC dbo.CommunityLeftOfLeftSection 'RCSA Australia & New Zealand'
EXEC dbo.CommunityLeftOfLeftSection 'The Red Tent Woman'

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommunityLeftOfLeftSection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommunityLeftOfLeftSection]
GO
