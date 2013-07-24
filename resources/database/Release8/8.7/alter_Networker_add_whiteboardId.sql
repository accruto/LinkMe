ALTER TABLE Networker
ADD whiteboardId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE Networker ADD CONSTRAINT FK_Networker_Whiteboard
FOREIGN KEY (whiteboardId) REFERENCES Whiteboard ([id])
GO

/* 
 * Create a Whiteboard for each existing Networker
 */

DECLARE NetworkerCursor CURSOR FOR
SELECT DISTINCT boardOwnerId FROM WhiteboardMessage

OPEN NetworkerCursor

DECLARE @id UNIQUEIDENTIFIER
DECLARE @whiteboardId UNIQUEIDENTIFIER

FETCH NEXT FROM NetworkerCursor INTO @id

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @whiteboardId = NEWID()

	INSERT INTO Whiteboard VALUES (@whiteboardId)

	UPDATE Networker
	SET whiteboardId = @whiteboardId
	WHERE id = @id

	UPDATE WhiteboardMessage
	SET whiteboardId = @whiteboardId
	WHERE boardOwnerId = @id

	FETCH NEXT FROM NetworkerCursor INTO @id
END

CLOSE NetworkerCursor
DEALLOCATE NetworkerCursor

GO

-- Now we're finished with boardOwnerId, we can drop it.
ALTER TABLE WhiteboardMessage
DROP CONSTRAINT FK_WhiteboardMessage_BoardOwnerNetworker
GO

ALTER TABLE WhiteboardMessage
DROP COLUMN boardOwnerId
GO

-- We now have values for whiteboardId, so we can make it NOT NULL
ALTER TABLE WhiteboardMessage
ALTER COLUMN whiteboardId UNIQUEIDENTIFIER NOT NULL
GO

ALTER TABLE WhiteboardMessage ADD CONSTRAINT FK_WhiteboardMessage_Whiteboard
FOREIGN KEY (whiteboardId) REFERENCES Whiteboard ([id])
GO
