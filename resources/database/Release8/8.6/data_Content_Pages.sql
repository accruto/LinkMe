DECLARE @page NVARCHAR(256)
SET @page = 'au.com.venturelogic.linkme.web.default'

DECLARE @date DATETIME
SET @date = GETDATE()

-- Delete all previous content

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
INNER JOIN
	n2Item AS P ON I.ParentID = P.ID
INNER JOIN
	n2Item AS G ON P.ParentID = G.ID
WHERE
	G.Name = @page

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
INNER JOIN
	n2Item AS P ON I.ParentID = P.ID
WHERE
	P.Name = @page

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
WHERE
	I.Name = @page

DELETE
	n2Item
WHERE
	ParentID IN
	(
		SELECT ID FROM n2Item AS I WHERE I.ParentID IN
		(
			SELECT ID FROM n2Item AS P WHERE p.ParentID IS NULL AND P.Name = @page
		)
	)

DELETE
	n2Item
WHERE
	ParentID IN (SELECT ID FROM n2Item AS I WHERE I.ParentID IS NULL AND I.Name = @page)

DELETE
	n2Item
WHERE
	ParentID IS NULL AND Name = @page

-- Add new content

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	(@page, NULL, 'PageContentItem', NULL, 0, @date, @date, 1)


SET @page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'

-- Delete all previous content

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
INNER JOIN
	n2Item AS P ON I.ParentID = P.ID
INNER JOIN
	n2Item AS G ON P.ParentID = G.ID
WHERE
	G.Name = @page

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
INNER JOIN
	n2Item AS P ON I.ParentID = P.ID
WHERE
	P.Name = @page

DELETE
	n2Detail
FROM
	n2Detail AS D
INNER JOIN
	n2Item AS I ON D.ItemID = I.ID
WHERE
	I.Name = @page

DELETE
	n2Item
WHERE
	ParentID IN
	(
		SELECT ID FROM n2Item AS I WHERE I.ParentID IN
		(
			SELECT ID FROM n2Item AS P WHERE p.ParentID IS NULL AND P.Name = @page
		)
	)

DELETE
	n2Item
WHERE
	ParentID IN (SELECT ID FROM n2Item AS I WHERE I.ParentID IS NULL AND I.Name = @page)

DELETE
	n2Item
WHERE
	ParentID IS NULL AND Name = @page

-- Add new content

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	(@page, NULL, 'PageContentItem', NULL, 0, @date, @date, 1)
