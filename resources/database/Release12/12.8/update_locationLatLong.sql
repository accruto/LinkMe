update locality
set centroidlatitude = lat, centroidlongitude = long
from tmpPostLatLong t
join namedlocation n on n.displayname = t.postcode
where n.id = locality.id
and exists
(select 1
from tmpPostLatLong t
join namedlocation n on n.displayname = t.postcode
where n.id = locality.id)
