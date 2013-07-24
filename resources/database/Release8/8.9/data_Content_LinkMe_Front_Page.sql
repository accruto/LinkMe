
DECLARE @id INT

DECLARE @date DATETIME
SET @date = GETDATE()

DECLARE @name VARCHAR(256)
DECLARE @type VARCHAR(256)
DECLARE @page VARCHAR(256)
DECLARE @content NVARCHAR(MAX)

-- Homepage main section
SET	@name = 'Home page main section'
SET @type = 'HtmlContentItem'
SET @page = 'au.com.venturelogic.linkme.web.default'
SET @content = '<div>
	<div id="mascot-image" class="pngfix">
		<img src="~/ui/img/linkme_man_big_spring.png" alt="LinkMe man" width="149" height="195" />
	</div>
	<div id="main-body-text">
		<h1 style="font-size: 34px; font-weight: bolder; width: 400px;">Upload your resume, jobs come to you</h1>
		<p>
			<strong>LinkMe is a place where you upload your resume.</strong>
		</p>
		<ul>
			<li>Upload your resume and you can be found for jobs by thousands of employers and recruiters.</li>
			<li>Develop networks and join groups to give yourself the best chance of being discovered for new jobs.</li>
			<li>You can choose to be visible or anonymous.</li>
		</ul>
		<p><strong>Did you know that 60%* of jobs are never advertised?</strong></p>
		<p class="fineprint"><span>*</span>Source: LinkMe survey, 2007</p>
	</div>
	<div class="clearer"></div>
	</div>'

IF NOT EXISTS
(
	SELECT	*
	FROM	n2Item
	WHERE	Name = @name
	AND		Type = @type
	AND		Title = 'Default'
	AND		Page = @page
)

BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Community, Page)
	VALUES
		(@name, NULL, @type, 'Default', 0, @date, @date, 1, null, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', @content)
END

ELSE

BEGIN

UPDATE	n2Detail
SET		StringValue = @content
WHERE	itemId = (
	SELECT	id
	FROM	n2Item
	WHERE	Name = @name
	AND		Type = @type
	AND		Title = 'Default'
	AND		Page = @page
)

END


-- HomePageLeftSection

SET	@name = 'Home page left section'
SET @type = 'SectionContentItem'
SET @page = 'au.com.venturelogic.linkme.web.default'
SET @content = '<table class="featured-employers" border="0" align="center">
	<tbody>
	<tr>
	<td style="text-align: center;"><a href="http://www.telstra.com.au/abouttelstra/careers/index.cfm"><img title="Telstra" src="~/ui/images/home/featured-emprec/telstra.png" alt="Telstra" /></a><a href="http://www.virgin.com/Jobs/VirginEqualsPeople.aspx" target="_blank"><img title="Virgin Group" src="~/ui/images/home/featured-emprec/virgin-group.png" alt="Virgin Group" /></a></td>
	</tr>
	<tr>
	<td><a href="http://www.ingdirect.com.au/Careers/index.htm" target="_blank"><img title="ING Direct" src="~/ui/images/home/featured-emprec/ING_Direct.jpg" alt="ING Direct" /></a></td>
	</tr>
	</tbody>
	</table>'

IF NOT EXISTS
(
	SELECT	*
	FROM	n2Item
	WHERE	Name = @name
	AND		Type = @type
	AND		Title = 'Default'
	AND		Page = @page
)

BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@name, NULL, @type, 'Default', 0, @date, @date, 1, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', 'Featured Employers')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', @content)

END

ELSE

BEGIN

	UPDATE	n2Detail
	SET		StringValue = @content
	WHERE	itemId = (
		SELECT	id
		FROM	n2Item
		WHERE	type = 'HtmlContentItem'
		AND		parentid = (
			SELECT	id
			FROM	n2Item
			WHERE	Name = @name
			AND		Title = 'Default'
			AND		Page = 'au.com.venturelogic.linkme.web.default'
		)
	)

END


-- HomePageRightSection


SET	@name = 'Home page right section'
SET @type = 'SectionContentItem'
SET @page = 'au.com.venturelogic.linkme.web.default'
SET @content = '<div>
		        <div class="testimonial">
		            <img class="author" src="~/ui/images/home/featured-candidates/michael.png" alt="Michael" width="64" height="64"/>
			        <div class="quote">
			           I couldn''t believe how quickly I [got a job]. The next day there was an email 
					   from a recruitment agency in my area. They had a supply and distribution job 
					   for me. We spoke and I met with the company. I start in less than 4 weeks! Yay!
			        </div>
    		        	<div class="author">- Michael, VIC</div>
			</div>
		        
		        <div class="testimonial">
		            <img class="author" src="~/ui/images/home/featured-candidates/naveen_and_deepa.png" alt="Naveen and Deepa" width="64" height="64"/>
		            <div class="quote">
			            Our luck changed when we joined LinkMe and very quickly we were both offered
			            Civil Engineering roles with a construction company... we truly believe in LinkMe.
			        </div>
		            <div class="author">- Naveen and Deepa</div>
		        </div>
		</div>'




IF NOT EXISTS
(
	SELECT	*
	FROM	n2Item
	WHERE	Name = @name
	AND		Type = @type
	AND		Title = 'Default'
	AND		Page = @page
)

BEGIN

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible, Page)
	VALUES
		(@name, NULL, @type, 'Default', 0, @date, @date, 1, @page)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'SectionTitle', 'Testimonials')

	INSERT
		n2Item (Name, ParentID, Type, Title, SortOrder, Created, Updated, Visible)
	VALUES
		('SectionContent', @id, 'HtmlContentItem', NULL, 0, @date, @date, 1)

	SET @id = @@IDENTITY

	INSERT
		n2Detail (ItemID, Type, Name, StringValue)
	VALUES
		(@id, 'String', 'Text', @content)

END

ELSE

BEGIN

	UPDATE	n2Detail
	SET		StringValue = @content
	WHERE	itemId = (
		SELECT	id
		FROM	n2Item
		WHERE	type = 'HtmlContentItem'
		AND		parentid = (
			SELECT	id
			FROM	n2Item
			WHERE	Name = @name
			AND		Title = 'Default'
			AND		Page = 'au.com.venturelogic.linkme.web.default'
		)
	)

END