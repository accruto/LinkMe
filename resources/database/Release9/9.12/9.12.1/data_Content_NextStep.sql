DECLARE @verticalId UNIQUEIDENTIFIER
SET @verticalId = '4733F4F8-5B34-4130-BDFB-82E50BD4F4BD'

DECLARE @id UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @itemId UNIQUEIDENTIFIER

-- Delete old content

SET @id = '{4E4CC514-472C-4e8b-991A-F72B48330FB6}'
DELETE
	ContentDetail
WHERE
	id = @id

SET @id = '{8F311FD3-1B73-4273-8249-E667DB87566F}'
DELETE
	ContentItem
WHERE
	id = @id

SET @id = '{923BF244-BBBD-48b8-895D-7771E29120A4}'
DELETE
	ContentDetail
WHERE
	id = @id

SET @id = '{B7D0D9D1-C62C-4541-B454-E0E8C2D47452}'
DELETE
	ContentItem
WHERE
	id = @id

-- Homepage splash image

SET @id = '{8F311FD3-1B73-4273-8249-E667DB87566F}'
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Home page splash image', 'HtmlContentItem', 1, 0, @verticalId)

SET @itemId = @id
SET @id = '{4E4CC514-472C-4e8b-991A-F72B48330FB6}'
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '<div>
            <div id="splash-image">
                <div id="splash-image-1" class="pngfix">
	                <a href="~/join.aspx"><img src="~/ui/images/home/banner_step_1_upload-your-resume.png" alt="Job Search - Upload your resume for free" width="237" height="147" border="0" /></a>
                </div>
                <div id="splash-image-2" class="pngfix">
                    <img src="~/ui/images/home/banner_step_arrow.png" width="67" height="147"/>
                </div>
                <div id="splash-image-3" class="pngfix">
	                <a href="~/employers/employer.aspx"><img src="~/ui/images/home/banner_step_2.png" alt="Job Search - Employers search for you" width="239" height="147" border="0" /></a>
                </div>
            </div>
        </div>')

-- Homepage join header

SET @id = '{B7D0D9D1-C62C-4541-B454-E0E8C2D47452}'
INSERT
	ContentItem (id, parentId, name, type, enabled, deleted, verticalId)
VALUES
	(@id, NULL, 'Home page join header', 'HtmlContentItem', 1, 0, @verticalId)

SET @itemId = @id
SET @id = '{923BF244-BBBD-48b8-895D-7771E29120A4}'
INSERT
	ContentDetail (id, itemId, name, type, stringValue)
VALUES
	(@id, @itemId, 'Text', 'String', '<div id="front-page-join-header">
                        <h1>Join the Job Portal</h1>
                    </div>')


