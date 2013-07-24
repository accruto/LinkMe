DECLARE @title NVARCHAR(256)
SET @title = 'ITWire'

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT

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
	P.Title = @title

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
WHERE
	I.Title = @title

DELETE
	n2Item
WHERE
	ParentID IN (SELECT ID FROM n2Item AS I WHERE I.Title = @title)

DELETE
	n2Item
WHERE
	Title = @title

-- Add new content

-- Header

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Page header', NULL, 'CommunityHeaderContentItem', @title, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Content', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
<meta name="description" content="Technology news, views and jobs." />
<meta name="keywords" content="IT News, Technology News, ICT News Australia, Telecommunications News Australia, IT NEWS Australia, Google, Apple, Microsoft, Linux, IT Jobs, Ubuntu, Windows, Mac, Mac OS X, Leopard, iPhone, iPod, Open Source, Dell, HP, Sony, PlayStation, PS3, PS2, PSP, Nintendo, Wii, Nintendo DS, Xbox 360,Telstra Share holders, telstra shares, IT&amp;T News Australia, Stan Beer, Melbourne, Sydney, Stuart Corner, Information Technology, Paul Hosking, The Electric Communication Company, aint cool iT news, Optus, Voip, O2, ADSL2, Broadband, whirlpool, Primus, ADSL2+, iinet, Sergey Brinn, skype, ebay, yahoo, melbourne 2006,Science, Games,, WiMAX,bigair,clever communications,allegro networks, Games,employment,Australian bureau of statistics,abs, national broadband network, expert panel, fttn, internode, hackett, adsl2+, vdsl, isp, google, application engine, app engine, web application, hosted, sdk, python, preview, saas, software as a service, f5, firepass, common criteria, eal2+, consoles, Xbox, Wii, Playstation, Careers, IT Jobs, recruitment, Olivier, Best IT Talent Index, games, consoles, ps3, ps2, microsoft, xbox 360, sony, nintendo, wii, sales, australia, smarthouse, Telstra, Internet, Australia, global networks, RSA, Russian Federation Space Agency, NASA, Yi So-yeon, South Korea, TMA-12, Soyuz, South Korea, Republic of Korea, ASUS, Eee PC, Linux, Windows, Cassini, Students Exploring the Red Planet, Mars, NASA, MRO, Mars Reconnaissance Orbiter, Saturn, Cassini Scientist for a Day, dementia, Alzheimer''s disease, Breteler, Netherlands, Erasmus University, microsoft, protocols, interoperability, open source, sharepoint, office, exchange, outlook, patent, pledge, Nokia,iPhone,apple,cellphones, microsoft, patch tuesday, update, bulletin, windows, 2000, xp, server, 2003, 20008, vista, emf, wmf, vbscript, jscript, kill bit, yahoo, jukebopx, project, 2002, visio, 2007, office accounting, genuine advantage, office, outlook, junk mail, filter, live writer, Ares, Constellation Project, Orion, astronaut, NASA, crew launch vehicle, rocket, solid rocket booster, SRB, security, Facebook, social networking" />
<meta name="robots" content="index, follow" />
<link href="~/themes/communities/itwire/css/itwire.css" rel="stylesheet" />

<div id="center" align="center">
    <div id="advert1"></div>
    <div id="brandlogo">
        <a id="logo-link" href="http://www.itwire.com/" alt="iTWire - Information Technology news" title="iTWire - Information Technology news"></a>
		<div id="brandheader">
            <script type="text/javascript">
		<!--
                if(typeof(ffxAds)=="undefined")var ffxAds = [];
                var ad = {
                  width: "468",
                  height: "60",
                  adtype: "panorama",
                  isiframe: "yes"
                };
                ffxAds.push(ad);
                document.write("<div id=''ffxad"+ffxAds.length+"''></div>");
		//-->
            </script>
        </div>
	</div>
	<div class="clearer"></div>
    <div id="top_outer">
        <div align="left" id="top_inner">
		    <table width="100%" cellspacing="1" cellpadding="0" border="0">
		        <tbody>
		            <tr>
		                <td nowrap="nowrap">
		                    <a class="mainlevel-nav" href="http://www.itwire.com/index.php">Today on iTWire</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/component/option,com_tag/tag,CRM/tag_id,26603/">CRM</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/24/963/">IT News</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/0/965/">Telecommunications</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/18/966/">Tech Deals</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/14/967/">People</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/view/5378/986/">Our Blogs</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/23/1031/">Tech Lifestyle</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/25/1041/">Releases</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/content/blogsection/39/1081/">Science</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/component/option,com_extcalendar/Itemid,1108/">Events</a>
		                    <a class="mainlevel-nav" href="http://www.itwire.com/jobswire/">JobsWire</a>
		                    <a id="active_menu-nav" class="mainlevel-nav" href="http://myprofile.itwire.com/">MyProfile</a>
		                </td>
		            </tr>
		        </tbody>
		    </table>
	    </div>
    </div>
</div>
	<div class="clearer"></div>

<!-- FFXD Adcode  -->

<!--end of page identifier-->


<script type="text/javascript">
var ffxpageVars = {
cat:"jobswire",
ctype: "index",
site:"itwire"
};
</script>
<script type="text/javascript" src="http://fdimages.fairfax.com.au/crtvs/code/itwire/ffxutils.js"></script>
</div>')

-- Footer

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Page footer', NULL, 'CommunityFooterContentItem', @title, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Content', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div nowrap="nowrap">
    <div>
        <a href="http://www.itwire.com/" class="mainlevel">Technology News</a>
        <span class="mainlevel"> | </span>
        <a href="http://www.itwire.com/content/blogsection/0/965/" class="mainlevel">Telecommunications News</a>
        <span class="mainlevel"> | </span>
        <a href="http://www.itwire.com/content/blogsection/14/967/" class="mainlevel">Technology Recruitment News</a>
        <span class="mainlevel"> | </span>
        <a href="http://www.itwire.com/content/blogsection/18/598/" class="mainlevel">Australian Business News</a>
        <span class="mainlevel"> | </span>
        <a href="http://www.itwire.com/jobswire/" class="mainlevel">IT jobs Australia</a>
        <span class="mainlevel"> | </span>
    </div>
</div>')

-- CandidateImage

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Candidate logo', NULL, 'ImageContentItem', @title, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RootFolder', '~/themes/communities/itwire/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', 'candidate-logo.jpg')

-- HomePageRightSection

DECLARE @pageId INT

SELECT @pageId = ID FROM n2Item WHERE Name = 'au.com.venturelogic.linkme.web.default' AND Type = 'PageContentItem'

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Home page right section', @pageId, 'SectionContentItem', @title, 0, @date, @date, 0)

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

SELECT @pageId = ID FROM n2Item WHERE Name = 'au.com.venturelogic.linkme.web.default' AND Type = 'PageContentItem'

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Home page left section', @pageId, 'SectionContentItem', @title, 0, @date, @date, 0)

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

SELECT @pageId = ID FROM n2Item WHERE Name = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome' AND Type = 'PageContentItem'

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Logged in home page left section', @pageId, 'SectionContentItem', @title, 0, @date, @date, 0)

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

