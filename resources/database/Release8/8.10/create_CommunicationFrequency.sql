
IF NOT EXISTS(SELECT * FROM dbo.systypes WHERE name = 'CommunicationFrequency')
	CREATE TYPE dbo.CommunicationFrequency FROM TINYINT NOT NULL