DECLARE @community NVARCHAR(256)
SET @community = 'Finsia Career Network'

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
	(@id, 'String', 'Text', '<div id="finsiaContainer">
	<link rel="stylesheet" type="text/css" href="~/themes/communities/finsia/css/Finsia.css" />
	<table border="0" cellpadding="0" cellspacing="0" >             
		<tr>	    
			<td valign="top" class="interior_ca_1 interior_ca_1_style">

				<a href="http://www.finsia.com/"></a>
				<a href="http://www.finsia.com/">
					<span>
						<img alt="" src="~/themes/communities/finsia/img/Header.jpg" />
					</span>
				</a>		
			</td>       
		</tr>
		<tr>    
			<td class="primarynav_style" colspan="2" valign="center" align="center">
				
				<div Class="primarynavcontainer">
					<ul class="clearnavfix">
						<li>
							<img src="~/themes/communities/finsia/img/navSpacer1.gif" />
						</li>
						<li>
							<a href="http://www.finsia.com//About_Finsia/AM/ContentManagerNet/Default.aspx?Section=About_Finsia" id="ctl00_navmenuid_553" title="About Finsia">
							<img src="~/themes/communities/finsia/img/AboutFinsia.gif" id="i_About_Finsia" alt="About Finsia" Name="About_Finsia" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//Membership/AM/ContentManagerNet/Default.aspx?Section=Membership" id="ctl00_navmenuid_554" title="Membership">
								<img src="~/themes/communities/finsia/img/Membership.gif" id="i_Membership" alt="Membership" Name="Membership" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//Media_centre/AM/ContentManagerNet/Default.aspx?Section=Media_centre" id="ctl00_navmenuid_555" title="Media centre">
								<img src="~/themes/communities/finsia/img/MediaCentre.gif" id="i_Media_centre" alt="Media centre" Name="Media_centre" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//Professional_Develop/AM/ContentManagerNet/Default.aspx?Section=Professional_Develop" id="ctl00_navmenuid_557" title="Professional Development">
								<img src="~/themes/communities/finsia/img/ProDevelopment.gif" id="i_Professional_Develop" alt="Professional Development" Name="Professional_Develop" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//Policy_and_Advocacy/AM/ContentManagerNet/Default.aspx?Section=Policy_and_Advocacy" id="ctl00_navmenuid_708" title="Policy &amp; Advocacy">
								<img src="~/themes/communities/finsia/img/PolicyAdvocacy.gif" id="i_Policy_and_Advocacy" alt="Policy &amp; Advocacy" Name="Policy_and_Advocacy" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//Career_services/AM/ContentManagerNet/Default.aspx?Section=Career_services" id="ctl00_navmenuid_709"  title="Career services">
								<img src="~/themes/communities/finsia/img/CareersServices.gif" id="i_Career_services" alt="Career services" Name="Career_services" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//Information_services/AM/ContentManagerNet/Default.aspx?Section=Information_services" id="ctl00_navmenuid_710" title="Information services">
								<img src="~/themes/communities/finsia/img/InformationServices.gif" id="i_Information_services" alt="Information services" Name="Information_services" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<a href="http://www.finsia.com//My_Finsia1/AM/ContentManagerNet/Default.aspx?Section=My_Finsia1" id="ctl00_navmenuid_609" title="My Finsia (login)">
								<img src="~/themes/communities/finsia/img/MyFinsia.gif" id="i_My_Finsia1" alt="My Finsia (login)" Name="My_Finsia1" border="0" width="111" height="21" />
							</a>
						</li>
						<li>
							<img src="~/themes/communities/finsia/img/navSpacer.gif" />
						</li>
					</ul>
				</div>
			</td>
		</tr>
	</table>
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
	(@id, 'String', 'RootFolder', '~/themes/communities/finsia/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', '')

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
	<img src="~/themes/communities/finsia/img/HomepagePic.jpg" alt="Finsia career network" />
</div>
<div id="main-body-text">
	<h1>One site, thousands of possibilities</h1>
	<h1>Get connected with Finsia’s Career Network</h1>
	<p>Finsia’s Career Network gives you access to an online community of professionals working, or with an interest in the financial services industry. It’s like a Facebook for financial services professionals, where you can:</p>
	<ul>
	<li><strong>Manage and extend your personal business network</strong> by connecting with friends, associates and groups in a secure environment</li>
	<li><strong>Get headhunted with the click of a button.</strong> Finsia’s Career Network connects you with thousands of employers and recruiters, just set your visibility so they know you’re looking!</li>
	<li><strong>Improve your connectability and advance your career.</strong> Your profile stores your resume and can easily be updated and forwarded on to prospective employers.</li>
	<li><strong>Hedge your job prospects</strong> by using the range of online tools, such as rank your resume against your peers or conduct a self assessment.</li>
	</ul>
	<p><strong>Join Finsia’s Career Network in three easy steps … it’s your online financial services community.</strong></p>
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
	('Home page right section', NULL, 'SectionContentItem', @community, 0, @date, @date, 1, @page)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'SectionTitle', 'Finsia membership')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
	<p>Finsia has always provide member with fantastic opportunities for development – both professional and personal. My involvement goes back nearly 20 years, progressing from being a student to running lectures, to setting exams and being involved in speaking in at carers evenings, to now being on the Queensland Committee.</p>
	<p>Finsia is a true industry leader in bringing professional people together and providing mutual benefits along the way. I encourage everyone to be involved and help make a difference.</p>
	<p><strong>Noel Lord SA Fin</strong></p>
	<p>Division Director, Macquarie Adviser Services</p>
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