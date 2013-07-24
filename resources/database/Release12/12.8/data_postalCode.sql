-- create the postalCode records. By convention the localityId is the higher of the two values in NamedLocation

insert into postalcode select min(id), max(id) from namedlocation where displayname = '0841'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '2769'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '2808'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '3027'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '3086'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '3586'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '3785'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '6180'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '6209'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '6211'
insert into postalcode select min(id), max(id) from namedlocation where displayname = '7466'
