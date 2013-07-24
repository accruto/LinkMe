IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'linkme_viewstate'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN	

create table linkme_owner.linkme_viewstate
(
	viewStateKey  VARCHAR(100) NOT NULL,
	pageState    NTEXT NULL,
	UpdatedDate  DATETIME NOT NULL
)

END
GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'linkme_viewstate'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

ALTER TABLE [linkme_owner].[linkme_viewstate] ADD 
	CONSTRAINT [PK_linkme_viewstate] PRIMARY KEY  NONCLUSTERED 
	(
		[viewStateKey]
	)  ON [PRIMARY] 

END
GO