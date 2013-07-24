
IF EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Community') AND NAME = 'flags')
BEGIN

	UPDATE Community SET flags = 1 WHERE name = 'golfjobs.com.au'
	UPDATE Community SET flags = 1 WHERE name = 'RCSA Australia & New Zealand'
	UPDATE Community SET flags = 3 WHERE name = 'Monash University Graduate School of Business'
	UPDATE Community SET flags = 1 WHERE name = 'ITWire'
	UPDATE Community SET flags = 1 WHERE name = 'Scouts Australia'
	UPDATE Community SET flags = 2 WHERE name = 'Autopeople'
END
