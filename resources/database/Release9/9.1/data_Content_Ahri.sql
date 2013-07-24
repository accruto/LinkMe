DECLARE @community NVARCHAR(256)
SET @community = 'AHRI HRcareers Network'

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
	(@id, 'String', 'Text', '<div>
    <a href="http://www.ahri.com.au"><img alt="AHRI HRcareers Network" src="~/themes/communities/ahri/img/banner.jpg" style="border-style:none;" /></a>
</div>')

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
	(@id, 'String', 'RootFolder', '~/themes/communities/ahri/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', 'logo.jpg')

-- Homepage main section

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Community, Page)
VALUES
	('Home page main section', NULL, 'HtmlContentItem', NULL, 0, @date, @date, 1, @community, 'au.com.venturelogic.linkme.web.default')

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
<div id="mascot-image">
	<img src="~/themes/communities/ahri/img/homepage.jpg" alt="AHRI HRcareers Network" />
</div>
<div id="main-body-text">
	<h1>Upload your resume, jobs come to you</h1>
	<p>AHRI HRcareers has created a Networking Community, enabling you to connect with 1000’s of employers and recruiters across Australia.</p>
    <p>This professional networking portal focuses on building a ‘career relationship’ with other HR community members.</p>
    <p>Build your career profile today, develop networks and join groups to connect with other community members.</p>
    <p>You control the visibility of your profile.</p>
    <p>It only takes 1 minute to join and it’s FREE</p>
    <p>Be seen, be heard, be hired !!!!</p>
</div>
</div>


')


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
	('Home page left section', NULL, 'SectionContentItem', @community, 0, @date, @date, 1, @page)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'SectionTitle', 'Featured Employers')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '            <table class="featured-employers" border="0" align="center">
	            <tbody>
		            <tr>
			            <td style="text-align: center;">
							<a href="http://www.hrcareers.com.au/search.cfm?cid=285">
								<img id="imgCmhr" alt="CMHR" title="CMHR" runat="server" src="~/themes/communities/ahri/img/CMHR.jpg" />
							</a>
							<a href="http://www.hrcareers.com.au/search.cfm?cid=78">
								<img id="imgHrpartners" alt="hr partners" title="hr partners" runat="server" src="~/themes/communities/ahri/img/hrpartners.gif" />
							</a>
			            </td>
		            </tr>
	            </tbody>
            </table>
')

-- HomePageRightSection

SET @page = 'au.com.venturelogic.linkme.web.default'

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page right section', NULL, 'SectionContentItem', @community, 0, @date, @date, 1, @page)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'SectionTitle', 'Hot Jobs')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
	<p>Adelaide: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=639">HR - Generalist</a></p>
    <p>Asia: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=771">Group Head of HR - Shanghai</a></p>
    <p>Brisbane: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=778">HR Advisor</a> | <a href="http://www.hrcareers.com.au/job/details.cfm?jid=759">Senior Human Resource Advisor</a></p>
    <p>Melbourne: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=556">HR Consultant</a></p>
    <p>Oceania: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=631">Human Resources Adviser</a></p>
    <p>Perth: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=634">General HR Strategic Services Positions</a></p>
    <p>Regional New South Wales: <a href="http://www.hrcareers.com.au/job/details.cfm?jid=636">HR Analyst - Performance and Reward</a> | <a href="http://www.hrcareers.com.au/job/details.cfm?jid=646">Senior Human Resources Officer</a></p>
</div>')

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