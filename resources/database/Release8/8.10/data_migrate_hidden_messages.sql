IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'WhiteboardMessage' and COLUMN_NAME = 'hidden' )
begin
	exec sp_executesql N'update usercontentitem set deleted = wm.hidden from usercontentitem uci2 INNER JOIN usercontentitem on usercontentitem.id = uci2.id INNER JOIN whiteboardmessage wm on wm.id = uci2.id where wm.hidden <> 0'

	update usercontentitem
		set deleterId = posterId
	where deleted <> 0 and deleterId is null
end
