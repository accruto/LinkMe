IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNewMessageCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetNewMessageCount]
GO

CREATE PROCEDURE [dbo].[GetNewMessageCount](@recipientUserId UNIQUEIDENTIFIER, @exceptFlags INT)
AS
BEGIN
	SET NOCOUNT ON

	SELECT COUNT(DISTINCT mt.id)
	FROM dbo.MessageThread mt 
	INNER JOIN dbo.UserMessage um
	ON mt.[id] = um.threadid AND um.senderId <> @recipientUserId
	INNER JOIN dbo.MessageThreadParticipant mtp
	ON mt.[id] = mtp.threadId AND mtp.userId = @recipientUserId
		AND (mtp.flags & @exceptFlags) <> @exceptFlags AND mtp.lastReadTime IS NULL
END
GO
