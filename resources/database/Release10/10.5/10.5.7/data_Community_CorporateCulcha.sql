
DECLARE @id UNIQUEIDENTIFIER
SET @id = '{BC492027-323B-417d-9CB2-DC31C63F06F5}'

EXEC dbo.CreateCommunity @id, 'Corporate Culcha Career Network', 'corporateculcha', 'careers.corporateculcha.com.au', 1

EXEC dbo.DeleteVerticalContent @id

-- Header

EXEC dbo.CreateVerticalContent @id, 'Page header', 'CommunityHeaderContentItem', 1, '<div>
    <a href="http://www.corporateculcha.com.au/"><img alt="Corporate Culcha" src="~/themes/communities/corporateculcha/img/corpculcha_950x75.jpg" style="border-style:none;" /></a>
</div>'

-- Footer

EXEC dbo.CreateVerticalContent @id, 'Page footer', 'CommunityFooterContentItem', 0, ''

-- Candidate logo

EXEC dbo.CreateVerticalImageContent @id, 'Candidate logo', 1, '~/themes/communities/corporateculcha/img/', 'corpculcha_170x30.jpg'

-- Home page main section

EXEC dbo.CreateVerticalHtmlContent @id, 'Home page main section', 1, '<div>
<div id="mascot-image"><img src="~/themes/communities/corporateculcha/img/corpculcha_150x300.jpg" alt="Corporate Culcha" /></div>
<div id="main-body-text">
<h1>Join the Corporate Culcha Career Network. Create an account, upload your resume and you''re ready!</h1>
<p>
  Corporate Culcha is an Indigenous owned and operated company, established to support Indigenous organisations,
  business and Australian industry, in building and developing sustainable Indigenous workforces.
</p>
<p>
  We work collaboratively with our clients to develop culturally competent strategies to engage,
  recruit and retain Indigenous talent. Our extensive suite of products support the enhancement
  of organisational cultures to be more inclusive of and accessible to Indigenous people.
  Packages are tailored to individual business needs.
</p>
<p>
  Our clientele are diverse and include some of Australia’s largest corporate entities,
  Government departments and Non-Government Organisations.
</p>
<p>
  Join now for FREE! and get started.
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

