ALTER TABLE [Group]
ADD whiteboardId UNIQUEIDENTIFIER NULL
GO

ALTER TABLE [Group] ADD CONSTRAINT FK_Group_Whiteboard
FOREIGN KEY (whiteboardId) REFERENCES Whiteboard ([id])
GO