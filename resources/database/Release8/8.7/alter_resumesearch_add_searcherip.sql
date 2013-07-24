/* Add searcherIp to DB if neccesary */

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ResumeSearch' AND COLUMN_NAME = 'searcherIp')
	ALTER TABLE ResumeSearch
	ADD searcherIp VARCHAR(15) DEFAULT NULL
ELSE
	SELECT 'searcherIp column already present in ResumeSearch. Nothing to do'