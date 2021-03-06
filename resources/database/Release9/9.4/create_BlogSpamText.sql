IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.BlogSpamText') AND type in (N'U'))
DROP TABLE dbo.BlogSpamText
GO

CREATE TABLE dbo.BlogSpamText
(
	pattern NVARCHAR(200) NOT NULL
)
GO

ALTER TABLE dbo.BlogSpamText
ADD CONSTRAINT PK_BlogSpamText PRIMARY KEY NONCLUSTERED
(
	pattern
)
GO

INSERT dbo.BlogSpamText (pattern)
VALUES ('http://lowestmall.com')

INSERT dbo.BlogSpamText (pattern)
VALUES ('www.wikishoes.com')

INSERT dbo.BlogSpamText (pattern)
VALUES ('replica watches')

INSERT dbo.BlogSpamText (pattern)
VALUES ('Louis Vuitton handbags')

INSERT dbo.BlogSpamText (pattern)
VALUES ('wow gold')

INSERT dbo.BlogSpamText (pattern)
VALUES ('warcraft')
