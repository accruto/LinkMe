UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = ''

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry IS NULL

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'retailandconsumerproducts' AND ind.displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'itandtelecommunications' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'hospitalityandtourism' AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'tradesandservices' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'administration' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'healthcareandmedical' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'educationandtraining' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salesandmarketing' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'advertisingmediaentertainment' AND ind.displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'manufacturingandoperations' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'government' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'accounting' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'bankingandfinancialservices' AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'callcentreandcustomerservice' AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'construction' AND ind.displayName = 'Construction'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'self-employment' AND ind.displayName = 'Self-Employment'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'hrandrecruitment' AND ind.displayName = 'HR & Recruitment'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'communityandsport' AND ind.displayName = 'Community & Sport'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'engineering' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'miningoilandgas' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'realestateandproperty' AND ind.displayName = 'Real Estate & Property'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'consultingandcorporatestrategy' AND ind.displayName = 'Consulting & Corp. Strategy'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'primaryindustry' AND ind.displayName = 'Primary Industry'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'legal' AND ind.displayName = 'Legal'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'insuranceandsuperannuation' AND ind.displayName = 'Insurance & Superannuation'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'other' AND ind.displayName = 'Other'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'scienceandtechnology' AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'transportandlogistics' AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'management'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'retailsalesassistant' AND ind.displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'schools' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'customerservice' AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salesrepconsultant' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'analystprogrammer' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'advertising' AND ind.displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'governmentstate' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'vocationaleduandtraining' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'helpdesksupport' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'transport' AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'telecommunications' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'accountant' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'designgraphics' AND ind.displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'communityservices' AND ind.displayName = 'Community & Sport'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'waitingstaff' AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'reception' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'automotive' AND ind.displayName = 'Automotive'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'university' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'associationsnon-profits'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'analyst'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'accountsclerkadmin' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'consultant'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'businessdevelopment' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'ageddisabledcare' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'projectmanagement'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'networksandsystems' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'accountmanagement' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'paexecutiveassistant' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'barbeveragestaff' AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'earlychildhood' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'callcentreoperator' AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'consulting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'maintenance'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'engineersoftware' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'workfromhome'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'governmentfederal' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'bankingbranchstaff' AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'traininganddevelopment' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'storepersonwarehousing' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'filmradiotv'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'nursingmidwives' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'storemanager' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'governmentlocal' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'officemanager'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'publishing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'chef' AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'automotivemechanic' AND ind.displayName = 'Automotive'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'cleaning'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'laborers'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'mortgage' AND ind.displayName = 'Real Estate & Property'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'journalism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'contractsadministration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'secretarial' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'security'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salesexecutive' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'internetmultimediadesign' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salesmanager' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'mechanicalengineer' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'recruitmentconsultant' AND ind.displayName = 'HR & Recruitment'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'engineerhardware'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'businessanalyst'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'freelance'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'cook' AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'publicrelations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'dataentrywpo' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'generalhr' AND ind.displayName = 'HR & Recruitment'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'sales' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'financialplanning' AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'marketingmanager' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'qatesters'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'army' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'supervisor'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'corpfinanceinvbanking' AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'marketingcommunications' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'machineoperators' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'caddrafting' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'administrationadmissions'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'processworkers'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'purchasing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'plantmanagement' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'sportandrecreation'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'hairdressing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'pharmaceuticals' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'pharmacy' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'airlines' AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'accountspayable' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'bookkeeping' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'carpentrycabinetmaking' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'housekeeping'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salesexecsreps' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'brandproductmanagement' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'propertymanagement' AND ind.displayName = 'Real Estate & Property'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'solicitorprivatepractice' AND ind.displayName = 'Legal'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'travelagents'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'engineernetwork' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'socialwork'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'electricalengineer' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salespre' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'kitchensandwichhand' AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'engineer' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'directmarketing' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'lawclerksparalegals' AND ind.displayName = 'Legal'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'psychologycounselling' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'rehabilitation' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'retailassistantmanager' AND ind.displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'alliedhealth' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'computeroperators' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'electrician' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'sitemanagement'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'emergencyservices'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'projectmanager'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'horticulture'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'internetmultimediadev' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'technician' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'trainers' AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'broking'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'assemblylineworker' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'claims'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'airconrefrigeration' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'assistantco-ordinator'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'gaming'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'printing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'civilengineer' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'surveying' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'architecture'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'frontoffice'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'interiordesign'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'research'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'riskmanagement'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'bakerbutchergrocer'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'airforce' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'planning'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'plumbing' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'communicationengineer' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'accountsreceivable' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'architect'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'compliance'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'agriculture'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'financialcontroller'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'underwriting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'fashion'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'teamleaders'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'databasedevandadmin' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'policyanalystadviser'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'boilermakingwelding' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'fundsmanagment' AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'actorsdancerssingers'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'ecommerce' AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'beautytherapy'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'officeassistantjunior' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'medicalpractionaer' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'forepersonsupervisor' AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'telesales' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'miningmaintenance' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'fitness'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'forestry'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'painters' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'clientservices'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'libraryservices'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'aviation' AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'veterinariananimalwelfare'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'dental' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'environhealthandsafety'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'qualityassurance'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'estimating'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'nannybabysitting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'legalsecretaries' AND ind.displayName = 'Legal'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'textileclothingfootwear'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'miningproduction' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'offshoreoil' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'oilandgasmaintenance' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'envandnaturalresources'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'payroll' AND ind.displayName = 'Accounting'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'aeronauticalengineer' AND ind.displayName = 'Engineering'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'creditmanagement'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'medicallaboratory' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'salespost' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'changemanagement'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'laborer'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'procurementandinventory'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'marketresearch' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'merchandising' AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'stockbroking' AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'telemarketing' AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'settlementsofficers'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'photography'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'technical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'solicitorinhouse' AND ind.displayName = 'Legal'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'packerfiller'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'navy' AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'workerscompensation'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'supervisorteamleader'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'consultants'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'contractsadministrator'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'ohands'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'toolmaker' AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'demolishingexcavating'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'buying'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'safetycoordinatorofficer'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'medicaltherapies' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'drilling'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'geoscience' AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'gardeninglandscaping'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'treasury'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'franchise'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'networkmarketing'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'oilandgasproduction' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'miningmineralprocessing' AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'laboratory' AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'clinicalmedicalresearch' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'florist'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'physiotherapy' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'policyandplanning'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'scientist' AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'ambulanceparamedic' AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'wineryviticulture'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'fundraising'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'chemist' AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'valuation'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'environmentalscience'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'superannuation'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'inspectors'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'landscapearchitecture'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'companysecretary' AND ind.displayName = 'Administration'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = NULL, currentJobIndustry = 'industrymigrated'
WHERE currentJobIndustry = 'strategy'

UPDATE linkme_owner.user_profile_ideal_job
SET currentJobIndustryId = ind.id, currentJobIndustry = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE currentJobIndustry = 'miningengineer' AND ind.displayName = 'Mining, Oil & Gas'

SELECT DISTINCT currentJobIndustry
FROM linkme_owner.user_profile_ideal_job
WHERE currentJobIndustry <> 'industrymigrated'
ORDER BY currentJobIndustry
