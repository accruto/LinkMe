declare @companyId uniqueidentifier
declare @compName nvarchar(20)
set @compName = 'Robert Half'

insert into linkme_owner.company
values ('FF96695C-41C5-4B8D-A0B6-45DC5BFE37D6', @compName)

set @companyId = (select id from linkme_owner.company where name = @compName)

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = @compName