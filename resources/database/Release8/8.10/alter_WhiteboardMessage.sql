IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'WhiteboardMessage' and COLUMN_NAME = 'hidden' )
	alter table [dbo].WhiteboardMessage
		drop column hidden