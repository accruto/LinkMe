DECLARE @verticalId UNIQUEIDENTIFIER
SET @verticalId = '4733F4F8-5B34-4130-BDFB-82E50BD4F4BD'

DECLARE @id UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @itemId UNIQUEIDENTIFIER

-- Delete all previous content

DELETE
	ContentDetail
FROM
	ContentDetail AS D
INNER JOIN
	ContentItem AS I ON D.itemId = I.id
INNER JOIN
	ContentItem AS P ON I.parentId = P.id
WHERE
	P.verticalId = @verticalId

DELETE
	ContentDetail
FROM
	ContentDetail AS D
INNER JOIN
	ContentItem AS I ON D.itemId = I.id
WHERE
	I.verticalId = @verticalId

DELETE
	ContentItem
WHERE
	parentId IN (SELECT id FROM ContentItem AS I WHERE I.verticalId = @verticalId)

DELETE
	ContentItem
WHERE
	verticalId = @verticalId

-- Add new content

-- Header

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Page header', 'CommunityHeaderContentItem', 1, 0, @verticalId)

SET @parentId = @id
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'Content', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '<div>
    <a href="http://www.nextstepaustralia.com/"><img alt="Next Step Australia" src="~/themes/communities/nextstep/img/next_step_header.jpg" style="border-style:none;" /></a>
</div>')

-- Footer

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Page footer', 'CommunityFooterContentItem', 0, 0, @verticalId)

SET @parentId = @id
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'Content', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '')

-- CandidateImage

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Candidate logo', 'ImageContentItem', 0, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'RootFolder', 'String', '~/themes/communities/nextstep/img/')

SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'RelativePath', 'String', 'logo.jpg')

-- Homepage main section

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Home page main section', 'HtmlContentItem', 1, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '<div>
<div id="mascot-image"><img src="~/themes/communities/nextstep/img/linkme02.jpg" alt="Next Step Australia" width="149" height="198" /></div>
<div id="main-body-text">
<h1>Direct Exposure to the Australian Job Market</h1>
<p>Next Step Australia''s job portal connects you with employers and recruiters across Australia.</p>
<ul>
<li>Simply upload your resume, set your career objectives and have employers and recruiters from all industries calling you about relevant job opportunities</li>
<li>Listing your details is simple and takes just a couple of minutes to complete!</li>
<li>Each week over 550 recruiters and employers list 60,000 jobs</li>
<li>Access resources and tools to help you secure work</li>
<li>Join the community and learn about working in Australia</li>
<li>Retain full control of your privacy and what information employers see about you</li>
</ul>
</div>
</div>')

-- Member sidebar section

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Member sidebar section', 'SectionContentItem', 0, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'SectionTitle', 'String', '')

SET @parentId = @itemId
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'SectionContent', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '')

-- HomePageLeftSection

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Home page left section', 'SectionContentItem', 0, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'SectionTitle', 'String', '')

SET @parentId = @itemId
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'SectionContent', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '')

-- HomePageRightSection

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Home page right section', 'SectionContentItem', 0, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'SectionTitle', 'String', '')

SET @parentId = @itemId
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'SectionContent', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '')

-- HomePageLeftOfLeftSection

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Home page left of left section', 'SectionContentItem', 0, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'SectionTitle', 'String', '')

SET @parentId = @itemId
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'SectionContent', 'HtmlContentItem', 0, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '')

-- LoggedInHomePageLeftSection

SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Logged in home page left section', 'SectionContentItem', 0, 0, @verticalId)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'SectionTitle', 'String', '')

SET @parentId = @itemId
SET @id = NEWID()
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, @parentId, 'SectionContent', 'HtmlContentItem', 1, 0, NULL)

SET @itemId = @id
SET @id = NEWID()
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '')