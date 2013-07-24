DECLARE @vertical NVARCHAR(256)
SET @vertical = 'PING Gateway'

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
	P.Vertical = @vertical

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
WHERE
	I.Vertical = @vertical

DELETE
	n2Item
WHERE
	ParentID IN (SELECT ID FROM n2Item AS I WHERE I.Vertical = @vertical)

DELETE
	n2Item
WHERE
	Vertical = @vertical

-- Add new content

-- Header

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible)
VALUES
	('Page header', NULL, 'CommunityHeaderContentItem', @vertical, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible)
VALUES
	('Content', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
    <a href="http://www.pinggateway.com/"><img alt="PING Gateway" src="~/themes/communities/ping/img/banner.jpg" style="border-style:none;" /></a>
</div>')

-- Footer

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible)
VALUES
	('Page footer', NULL, 'CommunityFooterContentItem', @vertical, 0, @date, @date, 0)

SET @id = @@IDENTITY

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible)
VALUES
	('Content', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '')

-- CandidateImage

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible)
VALUES
	('Candidate logo', NULL, 'ImageContentItem', @vertical, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RootFolder', '~/themes/communities/ping/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', 'logo.jpg')

-- Homepage main section

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Vertical, Page)
VALUES
	('Home page main section', NULL, 'HtmlContentItem', NULL, 0, @date, @date, 1, @vertical, 'au.com.venturelogic.linkme.web.default')

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
<div id="main-body-text">
<h1>Upload your resume, jobs come to you</h1>
<p>PING connects you with thousands of employers and recruiters across the globe.</p>
<p>Build your career profile today to be found for your next big opportunity. Develop networks and join groups to connect with other PING members.</p>
<p>You control full visibility of your profile.</p>
<p>It only takes 1 minute to join and it’s free!</p>
</div>
</div>')


-- Member sidebar section

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Vertical)
VALUES
	('Member sidebar section', NULL, 'SectionContentItem', NULL, 0, @date, @date, 0, @vertical)

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
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page left section', NULL, 'SectionContentItem', @vertical, 0, @date, @date, 0, @page)

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
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page right section', NULL, 'SectionContentItem', @vertical, 0, @date, @date, 0, @page)

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

-- HomePageLeftOfLeftSection

SET @page = 'au.com.venturelogic.linkme.web.default'

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page left of left section', NULL, 'SectionContentItem', @vertical, 0, @date, @date, 0, @page)

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
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Logged in home page left section', NULL, 'SectionContentItem', @vertical, 0, @date, @date, 0, @page)

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