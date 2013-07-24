
DECLARE @id UNIQUEIDENTIFIER
SET @id = '{2035D542-7D48-4841-B7BC-4DB9E154B6F9}'

EXEC dbo.CreateCommunity @id, 'AES Indigenous Careers Portal', 'aes', 'careerseeker.aes.org.au', 1

EXEC dbo.DeleteVerticalContent @id

-- Header

EXEC dbo.CreateVerticalContent @id, 'Page header', 'CommunityHeaderContentItem', 1, '<div>
    <a href="http://www.aes.org.au/"><img alt="Aboriginal Employment Strategy" src="~/themes/communities/aes/img/AESBanner.gif" style="border-style:none;" /></a>
</div>'

-- Footer

EXEC dbo.CreateVerticalContent @id, 'Page footer', 'CommunityFooterContentItem', 0, ''

-- Candidate logo

EXEC dbo.CreateVerticalImageContent @id, 'Candidate logo', 0, '', ''

-- Home page main section

EXEC dbo.CreateVerticalHtmlContent @id, 'Home page main section', 1, '<div>
<div id="mascot-image"><img src="~/themes/communities/aes/img/AESHomepagelogo.jpg" alt="AES" /></div>
<div id="main-body-text">
<h1>Upload your resume, jobs come to you</h1>
<p>
  AES Indigenous careers connects you with 1000’s of employers and recruiters across Australia. 
</p>
<p>
  Build your career profile today to be found for your next big opportunity.
  Develop networks and join groups to connect with other PARTNER members.
</p>
<p>
  You control the visibility of your profile.
</p>
<p>
  It only takes 1 minute to join and it''s free
</p>
</div>
</div>'

-- Member sidebar section

EXEC dbo.CreateVerticalSectionContent @id, 'Member sidebar section', 0, '', ''

-- HomePageLeftSection

EXEC dbo.CreateVerticalSectionContent @id, 'Home page left section', 0, '', ''

-- HomePageRightSection

EXEC dbo.CreateVerticalSectionContent @id, 'Home page right section', 0, '', ''

-- HomePageLeftOfLeftSection

EXEC dbo.CreateVerticalSectionContent @id, 'Home page left of left section', 0, '', ''

-- LoggedInHomePageLeftSection

EXEC dbo.CreateVerticalSectionContent @id, 'Logged in home page left section', 0, '', ''

