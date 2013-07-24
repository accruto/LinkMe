DECLARE @vertical NVARCHAR(256)
SET @vertical = 'The Nursing Centre Career Network'

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
    <a href="http://thenursingcentre.com.au"><img alt="The Nursing Centre" src="~/themes/communities/thenursingcentre/img/banner.jpg" style="border-style:none" /></a>
</div>
')

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
	(@id, 'String', 'RootFolder', '~/themes/communities/thenursingcentre/img/')

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
<div id="mascot-image"><img src="~/themes/communities/thenursingcentre/img/imagehomepage.jpg" alt="The Nursing Centre" width="140" height="187" /></div>
<div id="main-body-text">
<h1>A new direction in nursing career development and it''s FREE!</h1>
<p>The <strong>Nursing Centre Career Network</strong> is for <strong>ALL NURSES at ALL LEVELS </strong>whether you''re studying, just graduated, re-entering the workforce, in career transition or building your career.</p>
<p>Join in three easy steps to:</p>
<ul>
<li><strong>Manage your nursing career and resume</strong> with our extensive range of online tools.</li>
<li><strong>Track your Continuing Professional Development</strong> via our CPD portfolio.</li>
<li><strong>Connect with other nurses, colleagues and friends across Australia</strong> via online groups and networking tools.</li>
<li><strong>Get great nursing jobs</strong> when you need them.</li>
</ul>
<p>It only takes 1 minute to join and <strong>it''s FREE</strong> - you''re in control from start to finish, 24/7.</p>
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
	(@id, 'String', 'SectionTitle', 'Upcoming Events')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<p>Keen to get involved? Join us at these upcoming events:</p>
<ul>
<li><a href="http://mentoringinperth.eventbrite.com/" target="_blank">Mentoring Events in Perth (26 May 2009)</a></li>
<li><a href="http://excellenceinnursing.eventbrite.com/" target="_blank">Excellence in Nursing Breakfast (1 June 2009)</a></li>
<li><a href="http://excellenceinnursing.eventbrite.com/" target="_blank">The Magnet Principles and Your Organisation (1 June 2009)</a></li>
<li><a href="http://excellenceinnursing.eventbrite.com/" target="_blank">Relationship Based Care (2 June 2009)</a></li>
<li><a href="http://excellenceinnursing.eventbrite.com/" target="_blank">Mentoring Groups and Circles Breakfast (2 July 2009)</a></li>
<li><a href="http://mentoringgroupscircles.eventbrite.com/" target="_blank">Mentoring Program Coordinator Workshop (26 August 2009)</a></li>
</ul>')

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
	('Home page left of left section', NULL, 'SectionContentItem', @vertical, 0, @date, @date, 1, @page)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'SectionTitle', 'Grow Your Workforce')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<p>The Nursing Centre provides a range of services for organisations to <strong>grow their nursing workforce</strong>. Talk to us now about the following programs and packages:</p>
<ul>
<li>Mentoring Program Package</li>
<li>Nurse Unit Manager Leadership Program</li>
<li>Preceptor Leadership Program</li>
<li>Recruitment Package</li>
</ul>
<p>Here''s what nurses have already said about our programs:</p>
<ul>
<li>''I think this was a new fresh approach to senior nurse leadership.''</li>
<li>''I think the program is starting to get everyone thinking a bit differently and challenging how they do things - and I am happy to admit that I am checking my behaviour and the way I ask staff for their opinion or involvement in a task.''</li>
</ul>
<p>Contact us on 1800 100 848 or email <a href="http://thenursingcentre.com.au/">info@thenursingcentre.com.au</a>.</p>')

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