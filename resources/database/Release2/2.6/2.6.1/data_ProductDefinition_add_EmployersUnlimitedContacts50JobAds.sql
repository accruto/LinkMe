declare @newProductId uniqueidentifier

set @newProductId = 'AF9B7FF1-CDBD-46f3-870F-C049038271B5'

insert into linkme_owner.ProductDefinition
values (@newProductId, 'Package', 'Employer Packages', '316224000000000', 1, 'EmployersUnlimitedContacts50JobAds', 0.00, 'Unlimited contact credits and 50 job ads for Employers')


select *
from linkme_owner.productdefinition