
DECLARE @id UNIQUEIDENTIFIER
SET @id = '{57B22B9F-7509-419b-B4E0-205CDBC7A78B}'

EXEC dbo.CreateCommunity @id, 'University of Melbourne Arts', 'unimelbarts', NULL, 1, 0, 'http://www.arts.unimelb.edu.au/graduate/careerportal'
--EXEC dbo.CreateCommunity @id, 'University of Melbourne Arts', 'unimelbarts', 'careerportal.graduate.arts.unimelb.edu.au', 1, 1, 'http://www.arts.unimelb.edu.au/graduate/careerportal'

EXEC dbo.DeleteVerticalContent @id

-- Header

EXEC dbo.CreateVerticalContent @id, 'Page header', 'CommunityHeaderContentItem', 1, '<div>
    <a href="http://www.arts.unimelb.edu.au/graduate/careerportal/"><img alt="Graduate School of Humanities &amp; Social Sciences" src="~/themes/communities/unimelbarts/img/LinkMe-GSHSS-Banner-v3.png" style="border-style:none;" /></a>
</div>'

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
