if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnResumeUpdateClearHash]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnResumeUpdateClearHash]
GO


CREATE TRIGGER OnResumeUpdateClearHash
ON dbo.Resume
FOR UPDATE

AS

SET NOCOUNT ON

IF UPDATE(lensXml)
BEGIN
	UPDATE dbo.Resume
	SET sha1sum = NULL, lensXmlIsNull = CASE WHEN r.lensXml IS NULL THEN 1 ELSE 0 END
	FROM dbo.Resume r
	INNER JOIN inserted i
	ON r.[id] = i.[id]
END

SET NOCOUNT OFF
