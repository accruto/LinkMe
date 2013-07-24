-- Defect 1748 - manually set the correct extension for files that previously had different
-- extensions listed in the File table.

UPDATE linkme_owner.[File]
SET extension = '.jpeg'
WHERE dataId IN ('DBE38759-E7ED-4198-BDA4-081F4FB81888', 'E8ABA1E3-9488-4869-9867-266715334F18',
	'5CD9DB2E-4071-4844-9D0C-395F4C66C12B', '92A66A5C-B137-4627-B377-7424E21B640D',
	'D6D7EF77-5710-4A71-BF8E-7625D2450EE6', '6B9602BB-3250-455E-A3A7-82CB9B86D020',
	'154B073D-1434-4C98-908E-A23E79AFF54E', 'C542712E-7A39-4D5A-9073-BE8310678FA0',
	'A5B1E0F4-90D9-4804-8FF4-C81BEFB539BE', '87E99206-05F9-4507-B0A7-CE086613A135',
	'33EDA475-C67D-4A42-9801-D2FE4DC1BA96', 'A709D25A-79A8-499D-8B98-EA3B7DD249C9',
	'C1855632-7DE6-45FA-982D-EC0D85EB4F80', '82D8C6CD-6D67-497E-A0C9-F5F0F87BCB57',
	'EFB80A17-A101-4510-B1B2-5CF6C37D9EB8')

UPDATE linkme_owner.[File]
SET extension = '.gif'
WHERE dataId IN ('992FF286-2D38-4CF1-8266-3BA2879A058E')

-- Check that there are no more incorrect extensions. Once the old File table is dropped we won't be able
-- to find them anymore!

IF EXISTS
(
	SELECT dataId, COUNT(DISTINCT extension)
	FROM linkme_owner.[File]
	GROUP BY dataId
	HAVING COUNT(DISTINCT extension) > 1
)
RAISERROR('linkme_owner.[File] rows with incorrect extensions still exist. See FogBugz case 1748', 16, 1)

GO
