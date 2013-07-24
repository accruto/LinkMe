set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER PROCEDURE [dbo].[GetNewMessageCount](@recipientUserId UNIQUEIDENTIFIER, @exceptFlags INT)
AS
BEGIN
	SET NOCOUNT ON

	SELECT COUNT(distinct (CAST(mt.id as NVARCHAR(36))))
	FROM MessageThread mt 
	INNER JOIN UserMessage um
	ON mt.[id] = um.threadid AND um.senderId <> @recipientUserId
	INNER JOIN MessageThreadParticipant mtp
	ON mt.[id] = mtp.threadId AND mtp.userId = @recipientUserId AND (mtp.flags & @exceptFlags) <> @exceptFlags AND mtp.lastReadTime IS NULL
END
