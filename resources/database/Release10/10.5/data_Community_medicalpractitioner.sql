
DECLARE @id UNIQUEIDENTIFIER
SET @id = '{CF0F2D31-8F58-4d5b-9872-3AA1EE87D5C7}'

EXEC dbo.CreateCommunity @id, 'Medical Practitioner Careers', 'medicalpractitioner', 'medicalpractitionercareers.com.au', 1

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

