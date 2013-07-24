-- since the lat/long update these localities now fall outside the 50km radius "city" postcode

-- Melbourne:
delete from localityregion
where regionid = 2814 
and localityid in (1034, 1402, 1419)

--Brisbane
delete from localityregion
where regionid = 2811 
and localityid in (1601)

-- Darwin
delete from localityregion
where regionid = 2810
and localityid in (29, 34)

-- Hobart
delete from localityregion
where regionid = 2813
and localityid in (2759, 2687, 2711, 2726)

-- Sydney
delete from localityregion
where regionid = 2809
and localityid in (523, 529, 647, 657)

-- Adelaide
delete from localityregion
where regionid = 2812
and localityid in (2053, 2127, 2175)
