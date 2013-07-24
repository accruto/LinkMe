DECLARE @title NVARCHAR(256)
SET @title = 'New Zealand'

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
	(@id, 'String', 'Text', '<div><link href="/ui/css/newZealand.css" rel="stylesheet" /><img src="/ui/images/universal/banner_co_nz.jpg" alt="Linkme.co.nz banner" /></div>')

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
	(@id, 'String', 'Text', '')

-- CandidateImage

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('Candidate logo', NULL, 'ImageContentItem', @title, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RootFolder', '~/themes/communities/nz/img/')

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'RelativePath', 'logo.jpg')

