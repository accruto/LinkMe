DECLARE @vertical NVARCHAR(256)
SET @vertical = '1CN People'

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
    <a href="http://www.onecarenetwork.com.au"><img alt="One Care Network" src="~/themes/communities/onecarenetwork/img/partnerbanner.png" style="border-style:none" /></a>
</div>')

-- Footer

INSERT
	n2Item (Name, ParentID, Type, Vertical, SortOrder, Created, Updated, Visible)
VALUES
	('Page footer', NULL, 'CommunityFooterContentItem', @vertical, 0, @date, @date, 1)

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
	(@id, 'String', 'RootFolder', '~/themes/communities/onecarenetwork/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', 'partnerlogo.png')

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
<div id="mascot-image">
	<img src="~/themes/communities/onecarenetwork/img/imagehomepage.jpg" width="140" height="187" alt="1CN People" />
</div>
<div id="main-body-text">
	<h1>Upload your resume and reach new heights in your career today</h1>
	<p>1CNPeople connects you with 1000''s of employers and recruiters across Australia. Committed to cultivating new talent within the health and aged care sector, 1CNPeople connects you with those who count in career progression. Join our growing network of aged care professionals and engage in on-line networking with progressive industry leaders.</p>
	<p>Build your career profile today and watch your next big opportunity come your way.</p>
	<p>1CN will help you develop an extensive network of contacts by exposing you to other professional network groups who will introduce you to new contacts, extending your career development potential by the minute.</p>
	<p>Once your resume has been uploaded and your profile developed, you control who views your career information.</p>
	<p>It only takes 1 minute to join and it''s free.</p>
	<p><em>1CNPeople is a One Care Network Pty Limited value solution... cultivating excellence through people and technology.</em></p>
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
	('Home page left section', NULL, 'SectionContentItem', @vertical, 0, @date, @date, 1, @page)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'SectionTitle', 'Sponsors')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '
	<p>
		<div style="text-align: left; margin-bottom: 26px;">
			<a href="http://www.derwentexecutive.com.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/hartmann.png" alt="" />
			</a>
			<a href="http://www.shk.net.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/bunzl.png" alt="" />
			</a>
		</div>
		<div style="text-align: left; margin-bottom: 26px;">
			<a href="http://www.executivesonline.com.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/tunstall.png" alt="" />
			</a>
			<a href="http://www.moirgroup.com.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/epicor.png" width="100" height="26" alt="" />
			</a>
		</div>
		<div style="text-align: center;">
			<a href="http://www.moirgroup.com.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/icare1.png" width="100" height="100" alt="" />
			</a>
		</div>
		<div>
			<a href="http://www.moirgroup.com.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/johnsondiversey.png" alt="" />
			</a>
			<a href="http://www.moirgroup.com.au/">
				<img style="border: medium none;" src="~/themes/communities/onecarenetwork/img/ils.png" width="240" height="76" alt="" />
			</a>
		</div>
	</p>')

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