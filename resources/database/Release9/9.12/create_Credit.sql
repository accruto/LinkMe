IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Credit') AND type in (N'U'))
DROP TABLE dbo.Credit
GO

CREATE TABLE dbo.Credit
(
	id UNIQUEIDENTIFIER NOT NULL,
	name NVARCHAR(50) NOT NULL,
	displayName NVARCHAR(100) NOT NULL,
	description NVARCHAR(200) NOT NULL
)

ALTER TABLE dbo.Credit
ADD CONSTRAINT PK_Credit PRIMARY KEY NONCLUSTERED
(
	id
)

CREATE UNIQUE INDEX [IX_Credit_name] ON [dbo].[Credit]
(
	[name]
)

CREATE UNIQUE INDEX [IX_Credit_displayName] ON [dbo].[Credit]
(
	[displayName]
)

INSERT
	Credit (id, name, displayName, description)
SELECT
	id, name, displayName, shortDescription
FROM
	ProductDefinition
WHERE
	type in ('ContactCredit', 'JobBoardAccess', 'JobAd')
