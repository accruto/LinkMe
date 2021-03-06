-- Create tables.

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.BlogComment') AND type in (N'U'))
DROP TABLE dbo.BlogComment
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.BlogPostTag') AND type in (N'U'))
DROP TABLE dbo.BlogPostTag
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.BlogTag') AND type in (N'U'))
DROP TABLE dbo.BlogTag
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.BlogPost') AND type in (N'U'))
DROP TABLE dbo.BlogPost
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Blog') AND type in (N'U'))
DROP TABLE dbo.Blog
GO

CREATE TABLE dbo.Blog
(
	id UNIQUEIDENTIFIER NOT NULL,
	oldId INT NULL,
	name NVARCHAR(50) NOT NULL,
	alias NVARCHAR(20) NOT NULL,
	description NTEXT NULL,
	externalUrl NVARCHAR(200) NULL,
	whoCanView INT NOT NULL,
	whoCanComment INT NOT NULL,
	isCareerBlog BIT NOT NULL,
	CONSTRAINT PK_Blog PRIMARY KEY NONCLUSTERED 
	(
		id
	)
)
GO

CREATE TABLE dbo.BlogPost
(
	id UNIQUEIDENTIFIER NOT NULL,
	blogId UNIQUEIDENTIFIER NOT NULL,
	oldId INT NULL,
	title NVARCHAR(500) NULL,
	text NTEXT NULL,
	createdTime DATETIME NOT NULL,
	deleted BIT NOT NULL,
	CONSTRAINT PK_BlogPost PRIMARY KEY NONCLUSTERED
	(
		id
	)
)
GO

ALTER TABLE dbo.BlogPost
ADD CONSTRAINT FK_BlogPost_Blog FOREIGN KEY (blogId)
REFERENCES dbo.Blog (id)
GO

CREATE TABLE dbo.BlogTag
(
	id UNIQUEIDENTIFIER NOT NULL,
	oldId INT NULL,
	text NVARCHAR(100) NOT NULL
	CONSTRAINT PK_BlogTag PRIMARY KEY NONCLUSTERED
	(
		id
	)
)
GO

ALTER TABLE dbo.BlogTag
ADD CONSTRAINT UQ_BlogTag_text UNIQUE (text)
GO

CREATE TABLE dbo.BlogPostTag
(
	postId UNIQUEIDENTIFIER NOT NULL,
	tagId UNIQUEIDENTIFIER NOT NULL
	CONSTRAINT PK_BlogPostTag PRIMARY KEY NONCLUSTERED
	(
		postId,
		tagId
	)
)
GO

ALTER TABLE dbo.BlogPostTag
ADD CONSTRAINT FK_BlogPostTag_BlogPost FOREIGN KEY (postId)
REFERENCES dbo.BlogPost (id)
GO

ALTER TABLE dbo.BlogPostTag
ADD CONSTRAINT FK_BlogPostTag_BlogTag FOREIGN KEY (tagId)
REFERENCES dbo.BlogTag (id)
GO

CREATE TABLE dbo.BlogComment
(
	id UNIQUEIDENTIFIER NOT NULL,
	postId UNIQUEIDENTIFIER NOT NULL,
	oldId INT NULL,
	text NTEXT NULL,
	createdTime DATETIME NOT NULL,
	deleted BIT NOT NULL,
	contributorId UNIQUEIDENTIFIER NULL,
	alias NVARCHAR(100) NULL
	CONSTRAINT PK_BlogComment PRIMARY KEY NONCLUSTERED
	(
		id
	)
)
GO

ALTER TABLE dbo.BlogComment
ADD CONSTRAINT FK_BlogComment_BlogPost FOREIGN KEY (postId)
REFERENCES dbo.BlogPost (id)
GO

-- Migrate data

INSERT
	dbo.Blog (id, oldId, name, alias, description, externalUrl, whoCanView, whoCanComment, isCareerBlog)
SELECT
	UserId,
	id,
	Name,
	Alias,
	Description,
	[External],
	Visibility,
	CASE Security WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 0 END,
	Publish
FROM
	dbo.tbl_Blog
GO

INSERT
	dbo.BlogPost (id, blogId, oldId, title, text, createdTime, deleted)
SELECT
	NEWID(),
	(SELECT id FROM dbo.Blog AS b WHERE b.oldId = op.Blog),
	op.id,
	op.Title,
	op.Content,
	op.Date,
	op.Removed
FROM
	dbo.tbl_Post AS op
GO

INSERT
	dbo.BlogTag (id, oldId, text)
SELECT
	NEWID(), Id, Tag
FROM
	dbo.tbl_Tag
GO

INSERT
	dbo.BlogPostTag (postId, tagId)
SELECT
	p.Id, t.id
FROM
	dbo.tbl_Association AS oa
INNER JOIN
	dbo.BlogPost AS p ON p.oldId = oa.InstanceId
INNER JOIN
	dbo.BlogTag AS t ON t.oldId = oa.TagId
GO

INSERT
	dbo.BlogComment (id, postId, oldId, text, createdTime, deleted, contributorId, alias)
SELECT
	NEWID(),
	(SELECT id FROM dbo.BlogPost WHERE oldId = Parent),
	id,
	Comment,
	Date,
	Removed,
	UserId,
	AnonymousName
FROM
	dbo.tbl_Comment
GO

