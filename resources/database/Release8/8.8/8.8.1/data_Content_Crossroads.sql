DECLARE @community NVARCHAR(256)
SET @community = 'Crossroads Human Resources'

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT
DECLARE @page NVARCHAR(256)

-- Delete all previous content

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
INNER JOIN
	n2Item AS P ON I.ParentID = P.ID
WHERE
	P.Community = @community

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
WHERE
	I.Community = @community

DELETE
	n2Item
WHERE
	ParentID IN (SELECT ID FROM n2Item AS I WHERE I.Community = @community)

DELETE
	n2Item
WHERE
	Community = @community

-- Add new content

-- Header

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible)
VALUES
	('Page header', NULL, 'CommunityHeaderContentItem', @community, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible)
VALUES
	('Content', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div><img alt="Crossroads" src="~/themes/communities/crossroads/img/banner.jpg" style="border-style:none;" />  </div>')

-- Footer

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible)
VALUES
	('Page footer', NULL, 'CommunityFooterContentItem', @community, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible)
VALUES
	('Content', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '')

-- CandidateImage

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible)
VALUES
	('Candidate logo', NULL, 'ImageContentItem', @community, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RootFolder', '~/themes/communities/crossroads/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', 'logo.jpg')

-- Homepage main section

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Community, Page)
VALUES
	('Home page main section', NULL, 'HtmlContentItem', NULL, 0, @date, @date, 0, @community, 'au.com.venturelogic.linkme.web.default')

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '')


-- Member sidebar section

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Community)
VALUES
	('Member sidebar section', NULL, 'SectionContentItem', NULL, 0, @date, @date, 0, @community)

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


-- HomePageLeftSection

SET @page = 'au.com.venturelogic.linkme.web.default'

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page left section', NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

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

-- HomePageRightSection

SET @page = 'au.com.venturelogic.linkme.web.default'

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page right section', NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

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

-- LoggedInHomePageLeftSection

SELECT @page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Logged in home page left section', NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

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