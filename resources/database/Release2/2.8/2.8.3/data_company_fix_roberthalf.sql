declare @companyId uniqueidentifier
declare @compName nvarchar(20)
set @compName = 'Robert Half'

set @companyId = (select id from linkme_owner.company where name = @compName)

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = @compName

