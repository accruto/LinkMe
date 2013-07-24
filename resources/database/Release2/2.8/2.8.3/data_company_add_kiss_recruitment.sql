declare @companyId uniqueidentifier
declare @compName nvarchar(20)
set @compName = 'KISS Recruitment'

insert into linkme_owner.company
values ('D7079C5E-7A91-4876-B748-F9B07DA46256', @compName)

set @companyId = (select id from linkme_owner.company where name = @compName)

update linkme_owner.employer_profile
set companyId = @companyId
where organisationName = @compName