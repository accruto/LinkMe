-- Defect 1398 - map some delivery area postcodes to lat and lon of a real suburb.

INSERT INTO linkme_owner.locality (locality, postcode, state, lat, lon)
SELECT TOP 1 '', '1001', l.state, l.lat, l.lon
FROM linkme_owner.locality l
WHERE postcode = '2000'

INSERT INTO linkme_owner.locality (locality, postcode, state, lat, lon)
SELECT TOP 1 '', '1043', l.state, l.lat, l.lon
FROM linkme_owner.locality l
WHERE postcode = '2000'

INSERT INTO linkme_owner.locality (locality, postcode, state, lat, lon)
SELECT TOP 1 '', '3004', l.state, l.lat, l.lon
FROM linkme_owner.locality l
WHERE postcode = '3000'

GO