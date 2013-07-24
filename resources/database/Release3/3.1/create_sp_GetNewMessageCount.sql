CREATE PROCEDURE dbo.GetNewMessageCount(@recipientUserId UNIQUEIDENTIFIER, @exceptFlags INT)
AS
BEGIN
	SET NOCOUNT ON

	SELECT COUNT(mt.subject)
	FROM MessageThread mt 
	INNER JOIN UserMessage um
	ON mt.[id] = um.threadid AND um.senderId <> @recipientUserId
	INNER JOIN MessageThreadParticipant mtp
	ON mt.[id] = mtp.threadId AND mtp.userId = @recipientUserId AND (mtp.flags & @exceptFlags) <> @exceptFlags AND mtp.lastReadTime IS NULL
END
GO
