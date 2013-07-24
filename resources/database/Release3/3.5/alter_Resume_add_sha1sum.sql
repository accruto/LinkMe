IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Resume') AND NAME = 'sha1sum')
BEGIN

    ALTER TABLE dbo.Resume
        ADD sha1sum BINARY(20) NULL

END

GO

IF NOT EXISTS (SELECT * FROM syscolumns WHERE id = OBJECT_ID('Resume') AND NAME = 'lensXmlIsNull')
BEGIN

    ALTER TABLE dbo.Resume
        ADD lensXmlIsNull BIT NULL

END

GO

IF NOT EXISTS (SELECT * FROM sysindexes WHERE name = 'IX_Resume_lensXmlIsNull')
BEGIN

    CREATE INDEX IX_Resume_lensXmlIsNull
        ON Resume
           (lensXmlIsNull)

END

GO

-- Populate the new column piecemeal.
-- It seems to struggle with a single big fat transaction.
WHILE 1=1
BEGIN

    UPDATE Resume
       SET lensXmlIsNull = CASE WHEN r.lensXml IS NULL THEN 1 ELSE 0 END
      FROM Resume r
      JOIN (
            SELECT TOP 200 *
              FROM Resume
             WHERE lensXmlIsNull IS NULL
           ) s ON r.id = s.id

    IF @@ROWCOUNT = 0 BREAK

END

GO
