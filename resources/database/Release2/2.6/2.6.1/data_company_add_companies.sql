declare @companyId uniqueidentifier

insert into linkme_owner.company
values ('98298CC5-E9FB-4f41-8CA7-F5754D2B80DE', 'Sensis')

insert into linkme_owner.company
values ('CE3C74C6-0BB2-457f-97DF-1319A723991C', 'IPA')

insert into linkme_owner.company
values ('55BD9815-A904-45c2-B17C-427135AF7FF4', 'Manpower Professional')

set @companyId = (select id from linkme_owner.company where name = 'Sensis')

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = 'sensis'
or organisationName = 'sensis pty ltd'

set @companyId = (select id from linkme_owner.company where name = 'Manpower Professional')

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = 'manpower'
or organisationName = 'Manpower Professional'
or organisationName = 'Manpower Services'

set @companyId = (select id from linkme_owner.company where name = 'IPA Personnel')

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = 'ipa'
or organisationName = 'ipa personnel'
or organisationName = 'IPA Personnel Pty Ltd'