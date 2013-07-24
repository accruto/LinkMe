UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = '' OR industryType LIKE '.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE industryType IS NULL

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'retailandconsumerproducts' OR industryType LIKE 'retailandconsumerproducts.%') AND ind.displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'itandtelecommunications' OR industryType LIKE 'itandtelecommunications.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'hospitalityandtourism' OR industryType LIKE 'hospitalityandtourism.%') AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'tradesandservices' OR industryType LIKE 'tradesandservices.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'administration' OR industryType LIKE 'administration.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'healthcareandmedical' OR industryType LIKE 'healthcareandmedical.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'educationandtraining' OR industryType LIKE 'educationandtraining.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salesandmarketing' OR industryType LIKE 'salesandmarketing.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'advertisingmediaentertainment' OR industryType LIKE 'advertisingmediaentertainment.%') AND ind.displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'manufacturingandoperations' OR industryType LIKE 'manufacturingandoperations.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'government' OR industryType LIKE 'government.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'accounting' OR industryType LIKE 'accounting.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'bankingandfinancialservices' OR industryType LIKE 'bankingandfinancialservices.%') AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'callcentreandcustomerservice' OR industryType LIKE 'callcentreandcustomerservice.%') AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'construction' OR industryType LIKE 'construction.%') AND ind.displayName = 'Construction'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'self-employment' OR industryType LIKE 'self-employment.%') AND ind.displayName = 'Self-Employment'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'hrandrecruitment' OR industryType LIKE 'hrandrecruitment.%') AND ind.displayName = 'HR & Recruitment'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'communityandsport' OR industryType LIKE 'communityandsport.%') AND ind.displayName = 'Community & Sport'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'engineering' OR industryType LIKE 'engineering.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'miningoilandgas' OR industryType LIKE 'miningoilandgas.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'realestateandproperty' OR industryType LIKE 'realestateandproperty.%') AND ind.displayName = 'Real Estate & Property'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'consultingandcorporatestrategy' OR industryType LIKE 'consultingandcorporatestrategy.%') AND ind.displayName = 'Consulting & Corp. Strategy'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'primaryindustry' OR industryType LIKE 'primaryindustry.%') AND ind.displayName = 'Primary Industry'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'legal' OR industryType LIKE 'legal.%') AND ind.displayName = 'Legal'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'insuranceandsuperannuation' OR industryType LIKE 'insuranceandsuperannuation.%') AND ind.displayName = 'Insurance & Superannuation'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'other' OR industryType LIKE 'other.%') AND ind.displayName = 'Other'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'scienceandtechnology' OR industryType LIKE 'scienceandtechnology.%') AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'transportandlogistics' OR industryType LIKE 'transportandlogistics.%') AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'management' OR industryType LIKE 'management.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'retailsalesassistant' OR industryType LIKE 'retailsalesassistant.%') AND ind.displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'schools' OR industryType LIKE 'schools.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'customerservice' OR industryType LIKE 'customerservice.%') AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salesrepconsultant' OR industryType LIKE 'salesrepconsultant.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'analystprogrammer' OR industryType LIKE 'analystprogrammer.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'advertising' OR industryType LIKE 'advertising.%') AND ind.displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'governmentstate' OR industryType LIKE 'governmentstate.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'vocationaleduandtraining' OR industryType LIKE 'vocationaleduandtraining.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'helpdesksupport' OR industryType LIKE 'helpdesksupport.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'transport' OR industryType LIKE 'transport.%') AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'telecommunications' OR industryType LIKE 'telecommunications.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'accountant' OR industryType LIKE 'accountant.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'designgraphics' OR industryType LIKE 'designgraphics.%') AND ind.displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'communityservices' OR industryType LIKE 'communityservices.%') AND ind.displayName = 'Community & Sport'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'waitingstaff' OR industryType LIKE 'waitingstaff.%') AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'reception' OR industryType LIKE 'reception.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'automotive' OR industryType LIKE 'automotive.%') AND ind.displayName = 'Automotive'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'university' OR industryType LIKE 'university.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'associationsnon-profits' OR industryType LIKE 'associationsnon-profits.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'analyst' OR industryType LIKE 'analyst.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'accountsclerkadmin' OR industryType LIKE 'accountsclerkadmin.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'consultant' OR industryType LIKE 'consultant.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'businessdevelopment' OR industryType LIKE 'businessdevelopment.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'ageddisabledcare' OR industryType LIKE 'ageddisabledcare.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'projectmanagement' OR industryType LIKE 'projectmanagement.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'networksandsystems' OR industryType LIKE 'networksandsystems.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'accountmanagement' OR industryType LIKE 'accountmanagement.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'paexecutiveassistant' OR industryType LIKE 'paexecutiveassistant.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'barbeveragestaff' OR industryType LIKE 'barbeveragestaff.%') AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'earlychildhood' OR industryType LIKE 'earlychildhood.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'callcentreoperator' OR industryType LIKE 'callcentreoperator.%') AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'consulting' OR industryType LIKE 'consulting.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'maintenance' OR industryType LIKE 'maintenance.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'engineersoftware' OR industryType LIKE 'engineersoftware.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'workfromhome' OR industryType LIKE 'workfromhome.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'governmentfederal' OR industryType LIKE 'governmentfederal.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'bankingbranchstaff' OR industryType LIKE 'bankingbranchstaff.%') AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'traininganddevelopment' OR industryType LIKE 'traininganddevelopment.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'storepersonwarehousing' OR industryType LIKE 'storepersonwarehousing.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'filmradiotv' OR industryType LIKE 'filmradiotv.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'nursingmidwives' OR industryType LIKE 'nursingmidwives.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'storemanager' OR industryType LIKE 'storemanager.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'governmentlocal' OR industryType LIKE 'governmentlocal.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'officemanager' OR industryType LIKE 'officemanager.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'publishing' OR industryType LIKE 'publishing.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'chef' OR industryType LIKE 'chef.%') AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'automotivemechanic' OR industryType LIKE 'automotivemechanic.%') AND ind.displayName = 'Automotive'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'cleaning' OR industryType LIKE 'cleaning.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'laborers' OR industryType LIKE 'laborers.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'mortgage' OR industryType LIKE 'mortgage.%') AND ind.displayName = 'Real Estate & Property'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'journalism' OR industryType LIKE 'journalism.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'contractsadministration' OR industryType LIKE 'contractsadministration.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'secretarial' OR industryType LIKE 'secretarial.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'security' OR industryType LIKE 'security.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salesexecutive' OR industryType LIKE 'salesexecutive.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'internetmultimediadesign' OR industryType LIKE 'internetmultimediadesign.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salesmanager' OR industryType LIKE 'salesmanager.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'mechanicalengineer' OR industryType LIKE 'mechanicalengineer.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'recruitmentconsultant' OR industryType LIKE 'recruitmentconsultant.%') AND ind.displayName = 'HR & Recruitment'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'engineerhardware' OR industryType LIKE 'engineerhardware.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'businessanalyst' OR industryType LIKE 'businessanalyst.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'freelance' OR industryType LIKE 'freelance.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'cook' OR industryType LIKE 'cook.%') AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'publicrelations' OR industryType LIKE 'publicrelations.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'dataentrywpo' OR industryType LIKE 'dataentrywpo.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'generalhr' OR industryType LIKE 'generalhr.%') AND ind.displayName = 'HR & Recruitment'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'sales' OR industryType LIKE 'sales.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'financialplanning' OR industryType LIKE 'financialplanning.%') AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'marketingmanager' OR industryType LIKE 'marketingmanager.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'qatesters' OR industryType LIKE 'qatesters.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'army' OR industryType LIKE 'army.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'supervisor' OR industryType LIKE 'supervisor.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'corpfinanceinvbanking' OR industryType LIKE 'corpfinanceinvbanking.%') AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'marketingcommunications' OR industryType LIKE 'marketingcommunications.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'machineoperators' OR industryType LIKE 'machineoperators.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'caddrafting' OR industryType LIKE 'caddrafting.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'administrationadmissions' OR industryType LIKE 'administrationadmissions.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'processworkers' OR industryType LIKE 'processworkers.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'purchasing' OR industryType LIKE 'purchasing.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'plantmanagement' OR industryType LIKE 'plantmanagement.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'sportandrecreation' OR industryType LIKE 'sportandrecreation.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'hairdressing' OR industryType LIKE 'hairdressing.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'pharmaceuticals' OR industryType LIKE 'pharmaceuticals.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'pharmacy' OR industryType LIKE 'pharmacy.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'airlines' OR industryType LIKE 'airlines.%') AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'accountspayable' OR industryType LIKE 'accountspayable.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'bookkeeping' OR industryType LIKE 'bookkeeping.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'carpentrycabinetmaking' OR industryType LIKE 'carpentrycabinetmaking.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'housekeeping' OR industryType LIKE 'housekeeping.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salesexecsreps' OR industryType LIKE 'salesexecsreps.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'brandproductmanagement' OR industryType LIKE 'brandproductmanagement.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'propertymanagement' OR industryType LIKE 'propertymanagement.%') AND ind.displayName = 'Real Estate & Property'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'solicitorprivatepractice' OR industryType LIKE 'solicitorprivatepractice.%') AND ind.displayName = 'Legal'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'travelagents' OR industryType LIKE 'travelagents.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'engineernetwork' OR industryType LIKE 'engineernetwork.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'socialwork' OR industryType LIKE 'socialwork.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'electricalengineer' OR industryType LIKE 'electricalengineer.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salespre' OR industryType LIKE 'salespre.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'kitchensandwichhand' OR industryType LIKE 'kitchensandwichhand.%') AND ind.displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'engineer' OR industryType LIKE 'engineer.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'directmarketing' OR industryType LIKE 'directmarketing.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'lawclerksparalegals' OR industryType LIKE 'lawclerksparalegals.%') AND ind.displayName = 'Legal'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'psychologycounselling' OR industryType LIKE 'psychologycounselling.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'rehabilitation' OR industryType LIKE 'rehabilitation.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'retailassistantmanager' OR industryType LIKE 'retailassistantmanager.%') AND ind.displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'alliedhealth' OR industryType LIKE 'alliedhealth.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'computeroperators' OR industryType LIKE 'computeroperators.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'electrician' OR industryType LIKE 'electrician.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'sitemanagement' OR industryType LIKE 'sitemanagement.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'emergencyservices' OR industryType LIKE 'emergencyservices.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'projectmanager' OR industryType LIKE 'projectmanager.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'horticulture' OR industryType LIKE 'horticulture.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'internetmultimediadev' OR industryType LIKE 'internetmultimediadev.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'technician' OR industryType LIKE 'technician.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'trainers' OR industryType LIKE 'trainers.%') AND ind.displayName = 'Education & Training'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'broking' OR industryType LIKE 'broking.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'assemblylineworker' OR industryType LIKE 'assemblylineworker.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'claims' OR industryType LIKE 'claims.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'airconrefrigeration' OR industryType LIKE 'airconrefrigeration.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'assistantco-ordinator' OR industryType LIKE 'assistantco-ordinator.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'gaming' OR industryType LIKE 'gaming.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'printing' OR industryType LIKE 'printing.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'civilengineer' OR industryType LIKE 'civilengineer.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'surveying' OR industryType LIKE 'surveying.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'architecture' OR industryType LIKE 'architecture.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'frontoffice' OR industryType LIKE 'frontoffice.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'interiordesign' OR industryType LIKE 'interiordesign.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'research' OR industryType LIKE 'research.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'riskmanagement' OR industryType LIKE 'riskmanagement.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'bakerbutchergrocer' OR industryType LIKE 'bakerbutchergrocer.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'airforce' OR industryType LIKE 'airforce.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'planning' OR industryType LIKE 'planning.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'plumbing' OR industryType LIKE 'plumbing.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'communicationengineer' OR industryType LIKE 'communicationengineer.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'accountsreceivable' OR industryType LIKE 'accountsreceivable.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'architect' OR industryType LIKE 'architect.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'compliance' OR industryType LIKE 'compliance.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'agriculture' OR industryType LIKE 'agriculture.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'financialcontroller' OR industryType LIKE 'financialcontroller.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'underwriting' OR industryType LIKE 'underwriting.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'fashion' OR industryType LIKE 'fashion.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'teamleaders' OR industryType LIKE 'teamleaders.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'databasedevandadmin' OR industryType LIKE 'databasedevandadmin.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'policyanalystadviser' OR industryType LIKE 'policyanalystadviser.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'boilermakingwelding' OR industryType LIKE 'boilermakingwelding.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'fundsmanagment' OR industryType LIKE 'fundsmanagment.%') AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'actorsdancerssingers' OR industryType LIKE 'actorsdancerssingers.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'ecommerce' OR industryType LIKE 'ecommerce.%') AND ind.displayName = 'I.T. & T'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'beautytherapy' OR industryType LIKE 'beautytherapy.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'officeassistantjunior' OR industryType LIKE 'officeassistantjunior.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'medicalpractionaer' OR industryType LIKE 'medicalpractionaer.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'forepersonsupervisor' OR industryType LIKE 'forepersonsupervisor.%') AND ind.displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'telesales' OR industryType LIKE 'telesales.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'miningmaintenance' OR industryType LIKE 'miningmaintenance.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'fitness' OR industryType LIKE 'fitness.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'forestry' OR industryType LIKE 'forestry.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'painters' OR industryType LIKE 'painters.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'clientservices' OR industryType LIKE 'clientservices.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'libraryservices' OR industryType LIKE 'libraryservices.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'aviation' OR industryType LIKE 'aviation.%') AND ind.displayName = 'Transport & Logistics'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'veterinariananimalwelfare' OR industryType LIKE 'veterinariananimalwelfare.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'dental' OR industryType LIKE 'dental.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'environhealthandsafety' OR industryType LIKE 'environhealthandsafety.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'qualityassurance' OR industryType LIKE 'qualityassurance.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'estimating' OR industryType LIKE 'estimating.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'nannybabysitting' OR industryType LIKE 'nannybabysitting.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'legalsecretaries' OR industryType LIKE 'legalsecretaries.%') AND ind.displayName = 'Legal'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'textileclothingfootwear' OR industryType LIKE 'textileclothingfootwear.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'miningproduction' OR industryType LIKE 'miningproduction.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'offshoreoil' OR industryType LIKE 'offshoreoil.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'oilandgasmaintenance' OR industryType LIKE 'oilandgasmaintenance.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'envandnaturalresources' OR industryType LIKE 'envandnaturalresources.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'payroll' OR industryType LIKE 'payroll.%') AND ind.displayName = 'Accounting'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'aeronauticalengineer' OR industryType LIKE 'aeronauticalengineer.%') AND ind.displayName = 'Engineering'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'creditmanagement' OR industryType LIKE 'creditmanagement.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'medicallaboratory' OR industryType LIKE 'medicallaboratory.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'salespost' OR industryType LIKE 'salespost.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'changemanagement' OR industryType LIKE 'changemanagement.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'laborer' OR industryType LIKE 'laborer.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'procurementandinventory' OR industryType LIKE 'procurementandinventory.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'marketresearch' OR industryType LIKE 'marketresearch.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'merchandising' OR industryType LIKE 'merchandising.%') AND ind.displayName = 'Sales & Marketing'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'stockbroking' OR industryType LIKE 'stockbroking.%') AND ind.displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'telemarketing' OR industryType LIKE 'telemarketing.%') AND ind.displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'settlementsofficers' OR industryType LIKE 'settlementsofficers.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'photography' OR industryType LIKE 'photography.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'technical' OR industryType LIKE 'technical.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'solicitorinhouse' OR industryType LIKE 'solicitorinhouse.%') AND ind.displayName = 'Legal'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'packerfiller' OR industryType LIKE 'packerfiller.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'navy' OR industryType LIKE 'navy.%') AND ind.displayName = 'Government/Defence'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'workerscompensation' OR industryType LIKE 'workerscompensation.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'supervisorteamleader' OR industryType LIKE 'supervisorteamleader.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'consultants' OR industryType LIKE 'consultants.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'contractsadministrator' OR industryType LIKE 'contractsadministrator.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'ohands' OR industryType LIKE 'ohands.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'toolmaker' OR industryType LIKE 'toolmaker.%') AND ind.displayName = 'Trades & Services'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'demolishingexcavating' OR industryType LIKE 'demolishingexcavating.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'buying' OR industryType LIKE 'buying.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'safetycoordinatorofficer' OR industryType LIKE 'safetycoordinatorofficer.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'medicaltherapies' OR industryType LIKE 'medicaltherapies.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'drilling' OR industryType LIKE 'drilling.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'geoscience' OR industryType LIKE 'geoscience.%') AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'gardeninglandscaping' OR industryType LIKE 'gardeninglandscaping.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'treasury' OR industryType LIKE 'treasury.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'franchise' OR industryType LIKE 'franchise.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'networkmarketing' OR industryType LIKE 'networkmarketing.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'oilandgasproduction' OR industryType LIKE 'oilandgasproduction.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'miningmineralprocessing' OR industryType LIKE 'miningmineralprocessing.%') AND ind.displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'laboratory' OR industryType LIKE 'laboratory.%') AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'clinicalmedicalresearch' OR industryType LIKE 'clinicalmedicalresearch.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'florist' OR industryType LIKE 'florist.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'physiotherapy' OR industryType LIKE 'physiotherapy.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'policyandplanning' OR industryType LIKE 'policyandplanning.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'scientist' OR industryType LIKE 'scientist.%') AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'ambulanceparamedic' OR industryType LIKE 'ambulanceparamedic.%') AND ind.displayName = 'Healthcare & Medical'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'wineryviticulture' OR industryType LIKE 'wineryviticulture.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'fundraising' OR industryType LIKE 'fundraising.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'chemist' OR industryType LIKE 'chemist.%') AND ind.displayName = 'Science & Technology'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'valuation' OR industryType LIKE 'valuation.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'environmentalscience' OR industryType LIKE 'environmentalscience.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'superannuation' OR industryType LIKE 'superannuation.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'inspectors' OR industryType LIKE 'inspectors.%')

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'landscapearchitecture' OR industryType LIKE 'landscapearchitecture.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'companysecretary' OR industryType LIKE 'companysecretary.%') AND ind.displayName = 'Administration'

UPDATE linkme_owner.employer_profile
SET industryId = NULL, industryType = 'industrymigrated'
WHERE (industryType = 'strategy' OR industryType LIKE 'strategy.%')

UPDATE linkme_owner.employer_profile
SET industryId = ind.id, industryType = 'industrymigrated'
FROM linkme_owner.Industry ind
WHERE (industryType = 'miningengineer' OR industryType LIKE 'miningengineer.%') AND ind.displayName = 'Mining, Oil & Gas'

SELECT DISTINCT industryType
FROM linkme_owner.employer_profile
WHERE industryType <> 'industrymigrated'
ORDER BY industryType
