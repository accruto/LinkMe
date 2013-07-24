IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VerticalHomePageTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[VerticalHomePageTitle]
GO

CREATE PROCEDURE [dbo].[VerticalHomePageTitle]
(
	@vertical NVARCHAR(256),
	@visible INT,
	@homePageTitle NVARCHAR(256)
)
AS
BEGIN

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT

-- Home page title

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = 'Home page title' AND Vertical = @vertical)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible, Page)
	VALUES
		('Home page title', NULL, 'TextContentItem', @vertical, 0, @date, @date, @visible, NULL)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', @homePageTitle)

END

END

GO

EXEC dbo.VerticalHomePageTitle 'Scouts Australia', 0, ''
EXEC dbo.VerticalHomePageTitle 'AHRI HRcareers Network', 0, ''
EXEC dbo.VerticalHomePageTitle 'Association of Accounting Technicians Australia', 0, ''
EXEC dbo.VerticalHomePageTitle 'Astute Career Network', 0, ''
EXEC dbo.VerticalHomePageTitle 'Australian Institute of Marine and Power Engineers', 0, ''
EXEC dbo.VerticalHomePageTitle 'Autopeople', 0, ''
EXEC dbo.VerticalHomePageTitle 'Crossroads Human Resources', 0, ''
EXEC dbo.VerticalHomePageTitle 'Finsia Career Network', 0, ''
EXEC dbo.VerticalHomePageTitle 'golfjobs.com.au', 0, ''
EXEC dbo.VerticalHomePageTitle 'ITWire', 0, ''
EXEC dbo.VerticalHomePageTitle 'Live In Australia Careers Community', 0, ''
EXEC dbo.VerticalHomePageTitle 'Marketing Association of Australia and New Zealand', 0, ''
EXEC dbo.VerticalHomePageTitle 'Monash University Graduate School of Business', 0, ''
EXEC dbo.VerticalHomePageTitle 'New Zealand', 0, ''
EXEC dbo.VerticalHomePageTitle 'RCSA Australia & New Zealand', 0, ''
EXEC dbo.VerticalHomePageTitle 'The Red Tent Woman', 0, ''
EXEC dbo.VerticalHomePageTitle '1CN People', 0, ''

EXEC dbo.VerticalHomePageTitle 'Business Spectator', 1, 'Business Spectator Executive Appointments - Senior jobs, executive recruitment'

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VerticalHomePageTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[VerticalHomePageTitle]
GO
