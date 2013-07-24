declare @productDefinitionId uniqueidentifier
declare @contactCreditId uniqueidentifier
declare @jobAdCreditId uniqueidentifier

set @productDefinitionId = (select id from linkme_owner.ProductDefinition where name = 'EmployersUnlimitedContacts50JobAds')
set @contactCreditId = (select id from linkme_owner.ProductDefinition where name = 'ContactCredit')
set @jobAdCreditId = (select id from linkme_owner.ProductDefinition where name = 'JobAd')

insert into linkme_owner.ProductPackageAssociation
values ('8436A309-5C44-4a91-841B-F42411378645', '2000000000', @productDefinitionId, @contactCreditId)

insert into linkme_owner.ProductPackageAssociation
values ('CF6D1E20-D56F-477a-A0B5-2D526E08646D', '50', @productDefinitionId, @jobAdCreditId)

select *
from linkme_owner.ProductPackageAssociation
where packageDefinitionId = @productDefinitionId