DECLARE @community NVARCHAR(256)
SET @community = 'Crossroads Human Resources'

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT
DECLARE @page NVARCHAR(256)

-- HomePageLeftOfLeftSection

SET @page = 'au.com.venturelogic.linkme.web.default'

INSERT
	n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
VALUES
	('Home page left of left section', NULL, 'SectionContentItem', @community, 0, @date, @date, 1, @page)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'SectionTitle', 'IMPORTANT!')

INSERT
	n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
VALUES
	('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

SET @id = @@IDENTITY

INSERT
	n2Detail (ItemID, Type, Name, StringValue)
VALUES
	(@id, 'String', 'Text', '<div>
  <p>
    To make sure you really utilise LinkMe to its greatest advantage,
    ensure you have worked through the Steps 1 to 4 of the
    <a href="http://www.crossroadshr.com.au/index.php/outplacement/welcome.htm">Career Transition program</a>.
    Having your CV ready to go will ensure you get the most out of Career Network.
  </p>
</div>')

