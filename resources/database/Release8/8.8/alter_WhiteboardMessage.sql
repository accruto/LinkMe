DECLARE WhiteboardMessageCursor CURSOR FOR
SELECT id, [time], senderId FROM WhiteboardMessage

OPEN WhiteboardMessageCursor

DECLARE @id UNIQUEIDENTIFIER
DECLARE @time DATETIME
DECLARE @posterId UNIQUEIDENTIFIER

FETCH NEXT FROM WhiteboardMessageCursor 
INTO @id, @time, @posterId

WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO UserContentItem 
	VALUES (@id, @time, 0, null, @posterId, null)

	FETCH NEXT FROM WhiteboardMessageCursor 
	INTO @id, @time, @posterId
END

CLOSE WhiteboardMessageCursor
DEALLOCATE WhiteboardMessageCursor

GO

ALTER TABLE WhiteboardMessage
DROP COLUMN [time]
GO

ALTER TABLE WhiteboardMessage
DROP CONSTRAINT FK_WhiteboardMessage_SenderNetworker

ALTER TABLE WhiteboardMessage
DROP COLUMN senderId
GO