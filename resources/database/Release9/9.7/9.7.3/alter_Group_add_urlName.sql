ALTER TABLE dbo.[Group]
ADD urlName NVARCHAR(250) NULL
GO

-- Account for special groups that are disabled.

UPDATE
	[dbo].[Group]
SET
	name = 'Hate my jobb'
WHERE
	name = 'Hate my job'
GO

UPDATE
	[dbo].[Group]
SET
	name = 'testX'
WHERE
	name = 'test'
	AND flags = 1
GO

-- Set the url name for existing groups

UPDATE
	[dbo].[Group]
SET
	urlName = REPLACE(REPLACE(REPLACE(RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(LOWER(name), 
	'&', ''),
	'"', ''),
	'?', ''), 
	',', ''), 
	'-', ''), 
	'.', ''), 
	'!', ''), 
	'/', ''), 
	'=', ''),
	'*', ''),
	'(', ''),
	')', ''),
	':', ''),
	'''', ''))), ' ', '-'), '---', '-'), '--', '-')
GO

-- Make the column not null

ALTER TABLE dbo.[Group]
ALTER COLUMN urlName NVARCHAR(250) NOT NULL
GO

