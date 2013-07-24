
DECLARE @id UNIQUEIDENTIFIER
SET @id = 'F6FFA9E3-2A68-4aad-9C6D-65174C9D1A09'

EXEC dbo.CreateCommunity @id, 'e4employment Career and Networking Community', 'e4e', 'jobs.e4e.com.au', 1

EXEC dbo.DeleteVerticalContent @id

-- Header

EXEC dbo.CreateVerticalContent @id, 'Page header', 'CommunityHeaderContentItem', 1, '<div>
    <img alt="e4employment" src="~/themes/communities/e4e/img/e4e_950x75.jpg" style="border-style:none;" /></a>
</div>'

-- Footer

EXEC dbo.CreateVerticalContent @id, 'Page footer', 'CommunityFooterContentItem', 0, ''

-- Candidate logo

EXEC dbo.CreateVerticalImageContent @id, 'Candidate logo', 1, '~/themes/communities/e4e/img/', 'e4e_170x30.jpg'

-- Home page main section

EXEC dbo.CreateVerticalHtmlContent @id, 'Home page main section', 1, '<div>
<div id="mascot-image"><img src="~/themes/communities/e4e/img/e4e_150x300.jpg" alt="e4employment" width="140" height="280" /></div>
<div id="main-body-text">
<h1>Upload your resume, jobs come to you</h1>
<p>jobs.e4e.com.au connects you with 1000s of recruiters across Australia.</p>
<p>Be in control of the visibility of your career profile.</p>
<p>Join other jobs.e4e.com.au networking groups.</p>
<p>It''s easy, it''s fast and it''s free!</p>
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







