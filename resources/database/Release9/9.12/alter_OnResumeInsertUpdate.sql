ALTER TRIGGER [dbo].[OnResumeInsertUpdate]
ON [dbo].[Resume]
FOR INSERT, UPDATE
AS

-- Only run if the XML was actually updated - ignore changes to sha1sum, etc.
IF UPDATE(lensXml)
BEGIN
    -- Need to set this for NHibernate.
    SET NOCOUNT ON

    UPDATE dbo.Resume
       SET sha1sum = dbo.HashBytesNvarcharMax('SHA1', CONVERT(NVARCHAR(MAX), r.lensXml)),
           lensXmlIsNull = CASE WHEN r.lensXml IS NULL THEN 1 ELSE 0 END
      FROM dbo.Resume r
      JOIN inserted i ON r.[id] = i.[id]

    SET NOCOUNT OFF
END