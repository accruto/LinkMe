
DECLARE @id UNIQUEIDENTIFIER
SET @id = '{3F82DB31-58EE-4ebc-8819-D81D473031EE}'

EXEC dbo.CreateCommunity @id, 'The Junction Works Jobs Portal', 'tjw', 'jobsportal.thejunctionworks.org', 1, 0, null

EXEC dbo.DeleteVerticalContent @id

-- Header

EXEC dbo.CreateVerticalContent @id, 'Page header', 'CommunityHeaderContentItem', 0, ''

-- Footer

EXEC dbo.CreateVerticalContent @id, 'Page footer', 'CommunityFooterContentItem', 0, ''

-- Candidate logo

EXEC dbo.CreateVerticalImageContent @id, 'Candidate logo', 0, '', ''

-- Home page main section

EXEC dbo.CreateVerticalHtmlContent @id, 'Home page main section', 0, ''

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
