DECLARE @community NVARCHAR(256)
DECLARE @itemName NVARCHAR(256)

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @id INT
DECLARE @page NVARCHAR(256)

-- AAT
set @community = 'Association of Accounting Technicians Australia'

-- Home Page Right Section

SET @page = 'au.com.venturelogic.linkme.web.default'
Set @itemName = 'Home page right section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Home Page Left Section

Set @itemName = 'Home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Homepage main section

Set @itemName = 'Home page main section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Community, Page)
	VALUES
		(@itemName, NULL, 'HtmlContentItem', NULL, 0, @date, @date, 0, @community, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Logged In HomePage Left Section

SET @page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'
Set @itemName = 'Logged in home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END


-- AIMPE
set @community = 'Australian Institute of Marine and Power Engineers'

-- Home Page Right Section

SET @page = 'au.com.venturelogic.linkme.web.default'
Set @itemName = 'Home page right section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Home Page Left Section

Set @itemName = 'Home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Logged In HomePage Left Section

SET @page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'
Set @itemName = 'Logged in home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END


-- Autopeople
set @community = 'Autopeople'

-- Home Page Right Section

SET @page = 'au.com.venturelogic.linkme.web.default'
Set @itemName = 'Home page right section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Home Page Left Section

Set @itemName = 'Home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Logged In HomePage Left Section

SET @page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'
Set @itemName = 'Logged in home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- MAANZ
set @community = 'Marketing Association of Australia and New Zealand'

-- Home Page Right Section

SET @page = 'au.com.venturelogic.linkme.web.default'
Set @itemName = 'Home page right section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Home Page Left Section

Set @itemName = 'Home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Homepage main section

Set @itemName = 'Home page main section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Community, Page)
	VALUES
		(@itemName, NULL, 'HtmlContentItem', NULL, 0, @date, @date, 0, @community, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END

-- Logged In HomePage Left Section

SET @page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'
Set @itemName = 'Logged in home page left section'

IF NOT EXISTS (SELECT * FROM n2Item WHERE Name = @itemName AND community = @community)
BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Community, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@itemName, NULL, 'SectionContentItem', @community, 0, @date, @date, 0, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', '')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', '')
END
