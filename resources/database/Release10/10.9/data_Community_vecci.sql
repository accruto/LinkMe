
DECLARE @id UNIQUEIDENTIFIER
SET @id = '{D134B0C4-4CEF-433b-BCDB-B2C9507BF803}'

EXEC dbo.CreateCommunity @id, 'VECCI', 'vecci', NULL, 2, 0, NULL

EXEC dbo.DeleteVerticalContent @id

-- Header

EXEC dbo.CreateVerticalContent @id, 'Page header', 'CommunityHeaderContentItem', 0, ''

-- Footer

EXEC dbo.CreateVerticalContent @id, 'Page footer', 'CommunityFooterContentItem', 0, ''

-- Candidate logo

EXEC dbo.CreateVerticalImageContent @id, 'Candidate logo', 0, '', ''

-- Home page main section

EXEC dbo.CreateVerticalHtmlContent @id, 'Home page main section', 0, ''

-- Employer home page main section

EXEC dbo.CreateVerticalHtmlContent @id, 'Employer home page main section', 1, '
<img src="~/themes/communities/vecci/img/vecci_logo.gif" style="position: relative; left: 123px; top: 35px;" />
<h1 style="position: relative; left: 123px; width: 400px; height: 20px; top: 35px;">VECCI Online Recruitment Offer</h1>
<h2 style="position: relative; left: 123px; width: 400px; height: 78px; top: 35px; margin-bottom: 70px; *margin-bottom: 35px;">
  VECCI has teamed with LinkMe to provide an online recruitment solution that also includes job posting to the VECCI website. You receive a 20% discount.
</h2>
'

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

EXEC dbo.CreateVerticalHtmlContent @id, 'Head', 0, ''
