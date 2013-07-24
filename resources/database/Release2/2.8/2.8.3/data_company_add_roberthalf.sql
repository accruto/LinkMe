declare @companyId uniqueidentifier
declare @compName nvarchar(20)
set @compName = 'Westfield'

insert into linkme_owner.company
values ('717867C5-2226-4064-A88C-24EBFD597BB2', @compName)

set @companyId = (select id from linkme_owner.company where name = @compName)

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = @compName
or emailaddress = 'capper@lhr.com.au'
