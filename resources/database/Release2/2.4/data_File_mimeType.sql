-- Set mime types for existing files.

UPDATE linkme_owner.[File]
SET mimeType = 'application/msword'
WHERE extension = '.doc'

UPDATE linkme_owner.[File]
SET mimeType = 'application/rtf'
WHERE extension = '.rtf'

GO

SELECT *
FROM linkme_owner.[File]
WHERE mimeType IS NULL

GO
