-- Defect 1486 - map postcode 6830 to the lat and lon of .

INSERT INTO linkme_owner.locality (locality, postcode, state, lat, lon)
SELECT TOP 1 '', '6830', l.state, l.lat, l.lon
FROM linkme_owner.locality l
WHERE postcode = '6000'

GO