-- Copy data from Photo to Image.

DECLARE @defaultFormat tinyint

SET @defaultFormat = 1 -- JPEG

INSERT INTO Image(id, data, format)
SELECT CONVERT(uniqueidentifier, Id), Photo, @defaultFormat
FROM linkme_owner.Photo
