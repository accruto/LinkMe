-- Processing worksheet "Verified company merges"...

-- Move from verified company 'Affinity IT' to existing verified company 'Affinity IT Recruitment'
UPDATE Employer SET companyId = '97f70ac5-6a68-4f7d-8496-ffa16b53e754' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Affinity IT'
-- Move from verified company 'Aspire Solutions' to existing verified company 'Aspire Solutions International'
UPDATE Employer SET companyId = 'e0d28aa4-d9c9-420c-9136-1fa39731f09b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Aspire Solutions'
-- Move from verified company 'AusOptic International' to existing verified company 'AusOptic'
UPDATE Employer SET companyId = 'ed1ff273-66d4-4e08-91e6-7205ec917d79' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'AusOptic International'
-- Rename verified company 'Command recruitment' to 'Command Recruitment Group'
UPDATE Company SET [name] = 'Command Recruitment Group' WHERE [name] = 'Command recruitment' AND verifiedById IS NOT NULL
GO

-- Move from verified company 'Dowrick' to existing verified company 'Dowrick Recruitment'
UPDATE Employer SET companyId = '2e980714-043c-48e8-8e87-ec95ba4c0d8b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Dowrick'
-- Move from verified company 'Inspire Recruitment Pty Ltd' to existing verified company 'Inspire Recruitment'
UPDATE Employer SET companyId = 'dac18c53-bfb5-48c6-a872-fbe4c55bd47a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Inspire Recruitment Pty Ltd'
-- Rename verified company 'IPA' to 'IPA Personnel'
UPDATE Company SET [name] = 'IPA Personnel' WHERE [name] = 'IPA' AND verifiedById IS NOT NULL
GO

-- Move from verified company 'IPA Recruitment' to existing verified company 'IPA Personnel'
UPDATE Employer SET companyId = 'ce3c74c6-0bb2-457f-97df-1319a723991c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'IPA Recruitment'
-- Move from verified company 'Link Me' to existing verified company 'LinkMe'
UPDATE Employer SET companyId = '161210e4-44e3-4537-bc4d-24eed58f2324' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Link Me'
-- Move from verified company 'LinkMe Pty Ltd' to existing verified company 'LinkMe'
UPDATE Employer SET companyId = '161210e4-44e3-4537-bc4d-24eed58f2324' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'LinkMe Pty Ltd'
-- Move from verified company 'Market Search' to existing verified company 'Market Search Recruitment'
UPDATE Employer SET companyId = '905173b1-00d1-45ea-a833-95a0ccadffbc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Market Search'
-- Move from verified company 'McArthur Management Services (QLD)' to existing verified company 'McArthur Management Services'
UPDATE Employer SET companyId = '3f433b31-c152-4d8d-b3b4-704d3cd37b33' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'McArthur Management Services (QLD)'
-- Processing worksheet "Email domains"...

-- Rename verified company 'Pursuit' to 'Pursuit Recruitment'
UPDATE Company SET [name] = 'Pursuit Recruitment' WHERE [name] = 'Pursuit' AND verifiedById IS NOT NULL
GO

-- Move from verified company 'Pursuit Recruitment (VIC)' to existing verified company 'Pursuit Recruitment'
UPDATE Employer SET companyId = 'f956fcdb-e377-444a-ab08-ac32818f18b9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Pursuit Recruitment (VIC)'
-- Rename verified company 'Accel Group' to 'Accel'
UPDATE Company SET [name] = 'Accel' WHERE [name] = 'Accel Group' AND verifiedById IS NOT NULL
GO

-- Move from verified company 'Aslan' to existing verified company 'Aslan Human Resources'
UPDATE Employer SET companyId = '92f2c3ed-5d22-427e-a7db-81a48bf87613' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Aslan'
-- Move from verified company 'BSI People Group' to existing verified company 'BSI People'
UPDATE Employer SET companyId = '2b479351-fe5a-424d-9230-8943f74a4d11' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'BSI People Group'
-- Move from verified company 'Candle NZ' to existing verified company 'Candle ICT (NZ)'
UPDATE Employer SET companyId = '6253c2dc-7c41-4897-9528-32a04d146755' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Candle NZ'
-- Move from verified company 'Clements Recruitment (QLD)' to existing verified company 'Clements Recruitment'
UPDATE Employer SET companyId = '95326e4a-9316-4bb9-8986-eb6a87fc242c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Clements Recruitment (QLD)'
-- Move from verified company 'Connect Personnel (NSW)' to existing verified company 'Connect Personnel'
UPDATE Employer SET companyId = '0dc4d7b1-c451-4278-958f-49c55f1d8ec1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Connect Personnel (NSW)'
-- Move from verified company 'DFP Recruitment Services (NSW)' to existing verified company 'DFP Recruitment Services'
UPDATE Employer SET companyId = '25afadac-2647-4929-adb5-30880c160475' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'DFP Recruitment Services (NSW)'
-- Move from verified company 'Downing Teal (WA)' to existing verified company 'Downing Teal'
UPDATE Employer SET companyId = '7744aba8-6adf-4e1c-8bda-53b3e75e0554' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Downing Teal (WA)'
-- Move from verified company 'Downing Teal' to existing verified company 'Downing Teal (DT Workforce)'
UPDATE Employer SET companyId = '4195d230-0db0-4bbf-a929-eeabeb5066d9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Downing Teal'
-- Move from verified company 'Ethos Corporation (NSW)' to existing verified company 'Ethos Corporation'
UPDATE Employer SET companyId = 'e1dc1e60-4d2a-4347-80eb-7f1ee0eac0b2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Ethos Corporation (NSW)'
-- Move from verified company 'Ethos Corporation (NSW)' to existing verified company 'Ethos Corporation'
UPDATE Employer SET companyId = 'e1dc1e60-4d2a-4347-80eb-7f1ee0eac0b2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Ethos Corporation (NSW)'
-- Move from verified company 'GMT Recruitment (QLD)' to existing verified company 'GMT Recruitment'
UPDATE Employer SET companyId = '7494d5db-73ba-4747-9f0b-68b0b565500b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'GMT Recruitment (QLD)'
-- Move from verified company 'Hamilton James Bruce (NSW)' to existing verified company 'Hamilton James and Bruce (NSW)'
UPDATE Employer SET companyId = '2cfbce45-fd96-4966-9694-ad9e2b72c08b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Hamilton James Bruce (NSW)'
-- Move from verified company 'Hostec (NSW)' to existing verified company 'Hostec'
UPDATE Employer SET companyId = '354d7910-7707-47fe-b235-68ff525bdef1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Hostec (NSW)'
-- Move from verified company 'HR National (VIC)' to existing verified company 'HR National'
UPDATE Employer SET companyId = '3bbb703a-a0cf-4df9-bd1b-a4611c92a167' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'HR National (VIC)'
-- Move from verified company 'Sensis' to existing verified company 'Sensis (Careers Centre)'
UPDATE Employer SET companyId = 'e12255a9-0aa0-4c68-b744-601f28626e8e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Sensis'
-- Rename verified company 'TAD (WA)' to 'TAD'
UPDATE Company SET [name] = 'TAD' WHERE [name] = 'TAD (WA)' AND verifiedById IS NOT NULL
GO

-- Move from verified company 'Intepro (NSW)' to existing verified company 'Interpro'
UPDATE Employer SET companyId = 'b18ff458-232b-4240-87f1-6d0a93f89cc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Intepro (NSW)'
-- Move from verified company 'Interpro (QLD)' to existing verified company 'Interpro'
UPDATE Employer SET companyId = 'b18ff458-232b-4240-87f1-6d0a93f89cc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Interpro (QLD)'
-- Move from verified company 'Interpro (VIC)' to existing verified company 'Interpro'
UPDATE Employer SET companyId = 'b18ff458-232b-4240-87f1-6d0a93f89cc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Interpro (VIC)'
-- Move from verified company 'IPA Personnel (Brisbane)' to existing verified company 'IPA Personnel (QLD)'
UPDATE Employer SET companyId = '67a58967-f2ae-4c70-b62d-61027316f17b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'IPA Personnel (Brisbane)'
-- Move from verified company 'IT Matters' to existing verified company 'IT Matters (NSW)'
UPDATE Employer SET companyId = '29b6ef8f-d2bd-4c18-85b3-5924c5d89c3a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'IT Matters'
-- Move from verified company 'MACRO Recruitment (VIC)' to existing verified company 'MACRO Recruitment'
UPDATE Employer SET companyId = '0d7bcb2d-9351-4b31-b7f1-ad1009df404f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'MACRO Recruitment (VIC)'
-- Move from verified company 'McCormack Employment Service' to existing verified company 'McCormack Employment Services'
UPDATE Employer SET companyId = 'd0792d47-0c2f-4ac0-83cc-c2632547d5dc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'McCormack Employment Service'
-- Move from verified company 'Mindworx' to existing verified company 'Mindworx (NSW)'
UPDATE Employer SET companyId = 'b1e11890-8cae-4af5-8ce0-f42f798147a3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Mindworx'
-- Move from verified company 'MYOB' to existing verified company 'MYOB (VIC)'
UPDATE Employer SET companyId = 'cd393305-ab1e-4014-9f4c-8d49d172c92a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'MYOB'
-- Move from verified company 'Online Recruitment (VIC)' to existing verified company 'Online Recruitment'
UPDATE Employer SET companyId = 'a6390343-a293-4939-ac63-f6b1b17f24f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Online Recruitment (VIC)'
-- Move from verified company 'Online Personnel (VIC)' to existing verified company 'Online Personnel'
UPDATE Employer SET companyId = '03efe3c7-1179-42c5-a5ed-02d947a32769' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Online Personnel (VIC)'
-- Move from verified company 'People2People (NSW)' to existing verified company 'People2People'
UPDATE Employer SET companyId = '573e238c-5923-499c-8984-385fc397543d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'People2People (NSW)'
-- Move from verified company 'Prime Appointments (VIC)' to existing verified company 'Prime Appointments'
UPDATE Employer SET companyId = 'dd0a78b0-6010-4cb9-9fe3-8a952aef1adb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Prime Appointments (VIC)'
-- Move from verified company 'Quadrant Recruitment (NSW)' to existing verified company 'Quadrant Recruitment'
UPDATE Employer SET companyId = 'e035fce4-3b09-4cb9-858c-9e847a4aed4e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Quadrant Recruitment (NSW)'
-- Move from verified company 'Telstra Careers Centre (NSW)' to existing verified company 'Telstra Careers Centre'
UPDATE Employer SET companyId = '056cea94-14f9-4c41-a8bc-bb2848ae9f71' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Telstra Careers Centre (NSW)'
-- Move from verified company 'Telstra Careers Centre (QLD)' to existing verified company 'Telstra Careers Centre'
UPDATE Employer SET companyId = '056cea94-14f9-4c41-a8bc-bb2848ae9f71' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Telstra Careers Centre (QLD)'
-- Move from verified company 'Telstra Careers Centre (VIC)' to existing verified company 'Telstra Careers Centre'
UPDATE Employer SET companyId = '056cea94-14f9-4c41-a8bc-bb2848ae9f71' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Telstra Careers Centre (VIC)'
-- Rename verified company 'Xpand (NSW)' to 'Xpand IT Group'
UPDATE Company SET [name] = 'Xpand IT Group' WHERE [name] = 'Xpand (NSW)' AND verifiedById IS NOT NULL
GO

-- Move from verified company 'Xpand (VIC)' to existing verified company 'Xpand IT Group'
UPDATE Employer SET companyId = 'ab021935-9beb-4b4f-80a2-913b70aacd40' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Xpand (VIC)'
-- Move from verified company 'Xpand Recruitment' to existing verified company 'Xpand IT Group'
UPDATE Employer SET companyId = 'ab021935-9beb-4b4f-80a2-913b70aacd40' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] WHERE c.[name] = 'Xpand Recruitment'
-- Processing worksheet "Unverified company merges" (1st column set)...

-- Move from unverified to verified company '4impact' for email domain '@4impact.com.au'
UPDATE Employer SET companyId = '63ed7222-82a7-4fbf-ad58-6ba3b4a983a2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = '4impact' AND c.verifiedById IS NULL AND emailAddress LIKE '%@4impact.com.au'
GO

-- Move from unverified to verified company 'ABT Recruitment' for email domain '@abtrecruitment.com.au'
UPDATE Employer SET companyId = 'ba47f2ce-be2d-4fc6-807d-cdc8290052c0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ABT Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@abtrecruitment.com.au'
GO

-- Move from unverified to verified company 'Accel Group' for email domain '@accelgroup.com.au'
UPDATE Employer SET companyId = '56bae21a-f06a-4a9c-9f4b-6ca15d1e6e27' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Accel Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@accelgroup.com.au'
GO

-- Move from unverified to verified company 'Active Recruitment' for email domain '@activerecruitment.com.au'
UPDATE Employer SET companyId = 'dbec4eb6-1cc0-4b88-983c-8e7a0b8995e3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Active Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@activerecruitment.com.au'
GO

-- Move from unverified to verified company 'Ashdown Consulting' for email domain '@ashdownconsulting.com.au'
UPDATE Employer SET companyId = '15d09377-48ca-4912-8d23-e676df7581f1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ashdown Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ashdownconsulting.com.au'
GO

-- Move from unverified to verified company 'Astute People Solutions' for email domain '@astutepeople.com.au'
UPDATE Employer SET companyId = '210aec1d-c16e-41fd-b597-8f119fbf0ccd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Astute People Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@astutepeople.com.au'
GO

-- Move from unverified to verified company 'AustCorp Consulting' for email domain '@austcc.com.au'
UPDATE Employer SET companyId = '13a290ed-5c2b-4a52-b875-ec0db0417f31' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'AustCorp Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@austcc.com.au'
GO

-- Move from unverified to verified company 'Balance Recruitment' for email domain '@balancerecruitment.com.au'
UPDATE Employer SET companyId = 'd800b959-a0b5-41e0-83e2-a33b4a748ec1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Balance Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@balancerecruitment.com.au'
GO

-- Move from unverified to verified company 'Beilby' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'f29ab7c1-2797-4784-91ee-8ad8640caa50' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

-- Move from unverified to verified company 'Blackwoods' for email domain '@wisau.com.au'
UPDATE Employer SET companyId = '5397bb7c-5935-4e38-aab3-b789eabe22cc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Blackwoods' AND c.verifiedById IS NULL AND emailAddress LIKE '%@wisau.com.au'
GO

-- Move from unverified to verified company 'Boomerang Executive' for email domain '@boomerangexecutive.com.au'
UPDATE Employer SET companyId = '5ecbea0d-254c-4b2a-83cb-5ac524adc390' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Boomerang Executive' AND c.verifiedById IS NULL AND emailAddress LIKE '%@boomerangexecutive.com.au'
GO

-- Move from unverified to verified company 'BSI People' for email domain '@bsipeople.com'
UPDATE Employer SET companyId = '2b479351-fe5a-424d-9230-8943f74a4d11' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'BSI People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bsipeople.com'
GO

-- Move from unverified to verified company 'BSI People Group' for email domain '@bsipeople.com'
UPDATE Employer SET companyId = '86b6f1b4-5ee6-4ec4-8d25-7f2c247e1b6b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'BSI People Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bsipeople.com'
GO

-- Move from unverified to verified company 'Candle ICT (NSW)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = '1ba74377-518c-40a5-9d11-e95bd5ab7ba9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

-- Move from unverified to verified company 'Candle ICT (QLD)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = 'e04cc2f5-c613-46f7-a9e7-a80486dd3371' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

-- Move from unverified to verified company 'Candle ICT (WA)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = 'f483ec38-bbef-4d16-bfe5-b6a18e5db3f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

-- Move from unverified to verified company 'CITI Recruitment' for email domain '@citirecruitment.com'
UPDATE Employer SET companyId = 'c8b04f1f-2c81-43dc-b173-9ad03e0a0aad' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'CITI Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@citirecruitment.com'
GO

-- Move from unverified to verified company 'Clements Recruitment' for email domain '@clements.com.au'
UPDATE Employer SET companyId = '95326e4a-9316-4bb9-8986-eb6a87fc242c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Clements Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@clements.com.au'
GO

-- Move from unverified to verified company 'Clements Recruitment (QLD)' for email domain '@clements.com.au'
UPDATE Employer SET companyId = 'bdca8814-87f0-4c82-94d5-aa7254623c0f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Clements Recruitment (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@clements.com.au'
GO

-- Move from unverified to verified company 'Collective Resources' for email domain '@collectiveresources.com.au'
UPDATE Employer SET companyId = '7328ab72-a182-4349-a264-7e8cec1f62b1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Collective Resources' AND c.verifiedById IS NULL AND emailAddress LIKE '%@collectiveresources.com.au'
GO

-- Move from unverified to verified company 'Command Recruitment Group' for email domain '@command.com.au'
UPDATE Employer SET companyId = '46fafee5-11e1-45b2-afff-b407a8060e13' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Command Recruitment Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@command.com.au'
GO

-- Move from unverified to verified company 'Credit Recruitment' for email domain '@creditrecruitment.com'
UPDATE Employer SET companyId = '60c95b21-a2ee-4970-9ead-6daeb0c8b10e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Credit Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@creditrecruitment.com'
GO

-- Move from unverified to verified company 'Credit Union Australia' for email domain '@cua.com.au'
UPDATE Employer SET companyId = '0c2ebc7b-c021-4e5a-b326-9fcae3522701' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Credit Union Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@cua.com.au'
GO

-- Move from unverified to verified company 'Davidson Recruitment' for email domain '@davidsongroup.com.au'
UPDATE Employer SET companyId = '4c2994fb-0b6c-4932-b857-c4ed897d4a6b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Davidson Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@davidsongroup.com.au'
GO

-- Move from unverified to verified company 'DFP (ACT)' for email domain '@dfp.com.au'
UPDATE Employer SET companyId = '3f35114e-db0e-453f-b43f-37e12ea3aefd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DFP (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dfp.com.au'
GO

-- Move from unverified to verified company 'DFP (VIC)' for email domain '@dfp.com.au'
UPDATE Employer SET companyId = '9db32a19-16f0-4bd4-a9db-c71ef2ae5969' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DFP (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dfp.com.au'
GO

-- Move from unverified to verified company 'Downing Teal' for email domain '@downingteal.com'
UPDATE Employer SET companyId = '7744aba8-6adf-4e1c-8bda-53b3e75e0554' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Downing Teal' AND c.verifiedById IS NULL AND emailAddress LIKE '%@downingteal.com'
GO

-- Move from unverified to verified company 'Downing Teal (WA)' for email domain '@downingteal.com'
UPDATE Employer SET companyId = '9f704916-ddd8-4f94-8746-5efdf499edb9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Downing Teal (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@downingteal.com'
GO

-- Move from unverified to verified company 'Dowrick' for email domain '@dowrick.com.au'
UPDATE Employer SET companyId = 'dedeb924-2434-4f9a-b916-283a6af3b945' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Dowrick' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dowrick.com.au'
GO

-- Move from unverified to verified company 'Dowrick Recruitment' for email domain '@dowrick.com.au'
UPDATE Employer SET companyId = '2e980714-043c-48e8-8e87-ec95ba4c0d8b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Dowrick Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dowrick.com.au'
GO

-- Move from unverified to verified company 'DWS' for email domain '@dws.com.au'
UPDATE Employer SET companyId = '5424dc68-40f3-4eae-a547-f82ebcbf958a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DWS' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dws.com.au'
GO

-- Move from unverified to verified company 'Eclipse Computing' for email domain '@eclipsecomputing.com.au'
UPDATE Employer SET companyId = '62c89d56-72ed-49af-94a9-7230c794ccdb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Eclipse Computing' AND c.verifiedById IS NULL AND emailAddress LIKE '%@eclipsecomputing.com.au'
GO

-- Move from unverified to verified company 'Employ' for email domain '@employ.com.au'
UPDATE Employer SET companyId = '81640b01-8f03-4e25-943e-2a4371223f62' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Employ' AND c.verifiedById IS NULL AND emailAddress LIKE '%@employ.com.au'
GO

-- Move from unverified to verified company 'ERS' for email domain '@ersaustralia.com.au'
UPDATE Employer SET companyId = '952874b0-8349-4c17-beb8-ddebfd4f5256' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ERS' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ersaustralia.com.au'
GO

-- Move from unverified to verified company 'ESE Consulting' for email domain '@eseconsulting.com.au'
UPDATE Employer SET companyId = 'bbbcb3f3-3efe-43f9-a03d-6500bdb69010' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ESE Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@eseconsulting.com.au'
GO

-- Move from unverified to verified company 'Ethos Corporation' for email domain '@ethoscorporation.com.au'
UPDATE Employer SET companyId = 'e1dc1e60-4d2a-4347-80eb-7f1ee0eac0b2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ethos Corporation' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ethoscorporation.com.au'
GO

-- Move from unverified to verified company 'Eurolink Consulting Australia (trading as Aristotle)' for email domain '@aristotlecorp.com.au'
UPDATE Employer SET companyId = 'eb89ec51-7352-47ce-8226-931d584d6f98' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Eurolink Consulting Australia (trading as Aristotle)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@aristotlecorp.com.au'
GO

-- Move from unverified to verified company 'Finlay Partners' for email domain '@finlaypartners.com.au'
UPDATE Employer SET companyId = '0825bd42-ede8-44e1-898d-2010102a5a92' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Finlay Partners' AND c.verifiedById IS NULL AND emailAddress LIKE '%@finlaypartners.com.au'
GO

-- Move from unverified to verified company 'Fox Symes Recruitment' for email domain '@fsarecruitment.com.au'
UPDATE Employer SET companyId = '42d49b43-0678-4c9d-8704-209092c84285' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Fox Symes Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@fsarecruitment.com.au'
GO

-- Move from unverified to verified company 'Hallis (NSW)' for email domain '@hallis.com.au'
UPDATE Employer SET companyId = '0a0a366d-b123-49be-964d-92ee51463517' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hallis (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hallis.com.au'
GO

-- Move from unverified to verified company 'Hallis (VIC)' for email domain '@hallis.com.au'
UPDATE Employer SET companyId = '76a1b62f-661e-480b-9e8c-1ba09fab711f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hallis (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hallis.com.au'
GO

-- Move from unverified to verified company 'Hostec' for email domain '@hostec.com.au'
UPDATE Employer SET companyId = '354d7910-7707-47fe-b235-68ff525bdef1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hostec' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hostec.com.au'
GO

-- Move from unverified to verified company 'HR National' for email domain '@hrnational.com.au'
UPDATE Employer SET companyId = '3bbb703a-a0cf-4df9-bd1b-a4611c92a167' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HR National' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hrnational.com.au'
GO

-- Move from unverified to verified company 'HR National (VIC)' for email domain '@hrnational.com.au'
UPDATE Employer SET companyId = '3714bc9f-5017-4c28-abd3-d4323777c80a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HR National (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hrnational.com.au'
GO

-- Move from unverified to verified company 'HR National (VIC)' for email domain '@salesstaff.com.au'
UPDATE Employer SET companyId = '3714bc9f-5017-4c28-abd3-d4323777c80a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HR National (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@salesstaff.com.au'
GO

-- Move from unverified to verified company 'Icon Recruitment' for email domain '@iconrec.com.au'
UPDATE Employer SET companyId = '5d359b7d-9288-48eb-9788-284fcdfc9bd4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Icon Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@iconrec.com.au'
GO

-- Move from unverified to verified company 'Insight Group' for email domain '@insightgroup.com.au'
UPDATE Employer SET companyId = 'f66dc7c3-4c1c-4e34-b6d9-612cefd2bb7a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Insight Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@insightgroup.com.au'
GO

-- Move from unverified to verified company 'Interpro' for email domain '@interpro.com.au'
UPDATE Employer SET companyId = 'b18ff458-232b-4240-87f1-6d0a93f89cc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Interpro' AND c.verifiedById IS NULL AND emailAddress LIKE '%@interpro.com.au'
GO

-- Move from unverified to verified company 'IPA Personnel (Brisbane)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'cf459c95-c7a7-462a-abf7-7db8122bd8c2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Brisbane)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified to verified company 'IPA Personnel (QLD)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '67a58967-f2ae-4c70-b62d-61027316f17b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified to verified company 'IPA Personnel (VIC)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'c6aeb474-4f13-4c7e-a464-dfab6a45ff0c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified to verified company 'iPeople Recruitment (NSW)' for email domain '@i-people.com.au'
UPDATE Employer SET companyId = '16b2bb32-8c02-47a0-b33d-edffe2a216e7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'iPeople Recruitment (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@i-people.com.au'
GO

-- Move from unverified to verified company 'IQBS' for email domain '@iqbs.com.au'
UPDATE Employer SET companyId = '7837ebfe-0f73-4e04-aefa-3be246bbd988' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IQBS' AND c.verifiedById IS NULL AND emailAddress LIKE '%@iqbs.com.au'
GO

-- Move from unverified to verified company 'IXP3' for email domain '@ixp3.com'
UPDATE Employer SET companyId = 'b5d5d6c6-9846-4fa3-8b08-f6761ba234a8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IXP3' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ixp3.com'
GO

-- Move from unverified to verified company 'K2 Recruitment' for email domain '@k2.net.au'
UPDATE Employer SET companyId = '40b3fe16-1724-4311-9025-1ee229fad61f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'K2 Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@k2.net.au'
GO

-- Move from unverified to verified company 'K2 Recruitment' for email domain '@rec2rec.net.au'
UPDATE Employer SET companyId = '40b3fe16-1724-4311-9025-1ee229fad61f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'K2 Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rec2rec.net.au'
GO

-- Move from unverified to verified company 'Key People' for email domain '@keypeople.com.au'
UPDATE Employer SET companyId = '073a1147-f443-4b05-8c44-f2707b8be5d4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Key People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@keypeople.com.au'
GO

-- Move from unverified to verified company 'Kiss Recruitment' for email domain '@kissrecruitment.com'
UPDATE Employer SET companyId = 'd7079c5e-7a91-4876-b748-f9b07da46256' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Kiss Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@kissrecruitment.com'
GO

-- Move from unverified to verified company 'Laminex' for email domain '@laminex.com.au'
UPDATE Employer SET companyId = 'b5335b9e-9c59-478d-80c4-27d1f0bc010e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Laminex' AND c.verifiedById IS NULL AND emailAddress LIKE '%@laminex.com.au'
GO

-- Move from unverified to verified company 'M&T Resources' for email domain '@mtr.com.au'
UPDATE Employer SET companyId = 'ca25a681-6bcf-446b-be27-b5a35187d707' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'M&T Resources' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mtr.com.au'
GO

-- Move from unverified to verified company 'Macquarie People' for email domain '@macquariepeople.com'
UPDATE Employer SET companyId = '91bc156d-f9d3-4e98-82ca-3913a1c0589c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Macquarie People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@macquariepeople.com'
GO

-- Move from unverified to verified company 'Marker Consulting' for email domain '@markerconsulting.com'
UPDATE Employer SET companyId = 'ac6f5296-fe29-49b9-b3b7-ae054b2f36b0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Marker Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@markerconsulting.com'
GO

-- Move from unverified to verified company 'Match 2 Personnel' for email domain '@match2.com.au'
UPDATE Employer SET companyId = '3424440e-a60c-4fc4-99e6-0496fca7d76f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Match 2 Personnel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@match2.com.au'
GO

-- Move from unverified to verified company 'McArthur Management Services' for email domain '@mcarthur.com.au'
UPDATE Employer SET companyId = '3f433b31-c152-4d8d-b3b4-704d3cd37b33' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'McArthur Management Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mcarthur.com.au'
GO

-- Move from unverified to verified company 'McCormack Employment Services' for email domain '@mccormackemployment.com.au'
UPDATE Employer SET companyId = 'd0792d47-0c2f-4ac0-83cc-c2632547d5dc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'McCormack Employment Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mccormackemployment.com.au'
GO

-- Move from unverified to verified company 'Medirecruit' for email domain '@medirecruit.com'
UPDATE Employer SET companyId = 'b5f34974-e74e-4d5c-8d9b-b7f17e030150' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Medirecruit' AND c.verifiedById IS NULL AND emailAddress LIKE '%@medirecruit.com'
GO

-- Move from unverified to verified company 'Mindworx' for email domain '@mindworx.com.au'
UPDATE Employer SET companyId = 'bb349b9b-0e7e-40ec-8349-0c3aa9f3ab80' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mindworx' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mindworx.com.au'
GO

-- Move from unverified to verified company 'Mindworx (NSW)' for email domain '@mindworx.com.au'
UPDATE Employer SET companyId = 'b1e11890-8cae-4af5-8ce0-f42f798147a3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mindworx (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mindworx.com.au'
GO

-- Move from unverified to verified company 'Mindworx (QLD)' for email domain '@mindworx.com.au'
UPDATE Employer SET companyId = 'a0b51df1-732c-44d3-8bde-2625824c5a20' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mindworx (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mindworx.com.au'
GO

-- Move from unverified to verified company 'Mitchellake' for email domain '@mitchellake.com'
UPDATE Employer SET companyId = '9f179da7-81b4-43bb-a023-2ddcf3013432' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mitchellake' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mitchellake.com'
GO

-- Move from unverified to verified company 'Monroe Consulting (NSW)' for email domain '@monroeconsulting.com'
UPDATE Employer SET companyId = 'bd2e32e8-6f0d-4128-8e92-0d5e2ef69b77' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Monroe Consulting (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@monroeconsulting.com'
GO

-- Move from unverified to verified company 'Oakton' for email domain '@oakton.com.au'
UPDATE Employer SET companyId = 'f10262fa-53c3-4bae-b77c-32b6084028fe' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Oakton' AND c.verifiedById IS NULL AND emailAddress LIKE '%@oakton.com.au'
GO

-- Move from unverified to verified company 'Olivier' for email domain '@olivier.com.au'
UPDATE Employer SET companyId = '98bc24d8-fa76-4179-aa40-b9bbf796fb30' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Olivier' AND c.verifiedById IS NULL AND emailAddress LIKE '%@olivier.com.au'
GO

-- Move from unverified to verified company 'Online Recruitment' for email domain '@onrec.com.au'
UPDATE Employer SET companyId = 'a6390343-a293-4939-ac63-f6b1b17f24f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Online Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@onrec.com.au'
GO

-- Move from unverified to verified company 'Optimum Recruitment' for email domain '@ogroup.com.au'
UPDATE Employer SET companyId = 'd53668ba-0cf1-489d-8d8b-954bfd6609c0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Optimum Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ogroup.com.au'
GO

-- Move from unverified to verified company 'People2People' for email domain '@people2people.com.au'
UPDATE Employer SET companyId = '573e238c-5923-499c-8984-385fc397543d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People2People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@people2people.com.au'
GO

-- Move from unverified to verified company 'People2People (NSW)' for email domain '@people2people.com.au'
UPDATE Employer SET companyId = 'b6950315-0d74-44a1-901d-e1ee26b6bcb1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People2People (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@people2people.com.au'
GO

-- Move from unverified to verified company 'Peoplebank (VIC)' for email domain '@peoplebank.com.au'
UPDATE Employer SET companyId = 'b7e8d713-80d3-404c-bf6e-49f02230d257' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Peoplebank (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplebank.com.au'
GO

-- Move from unverified to verified company 'PeopleBank (WA)' for email domain '@gryphon.com.au'
UPDATE Employer SET companyId = 'e60f3f63-6197-4d36-8b08-dc547bb7e36b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'PeopleBank (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gryphon.com.au'
GO

-- Move from unverified to verified company 'Personnel Concept' for email domain '@personnelconcept.com.au'
UPDATE Employer SET companyId = '8e77fbce-75a4-4530-8747-f500f01c460b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Personnel Concept' AND c.verifiedById IS NULL AND emailAddress LIKE '%@personnelconcept.com.au'
GO

-- Move from unverified to verified company 'Prime Appointments' for email domain '@jrsglobal.com'
UPDATE Employer SET companyId = 'dd0a78b0-6010-4cb9-9fe3-8a952aef1adb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Prime Appointments' AND c.verifiedById IS NULL AND emailAddress LIKE '%@jrsglobal.com'
GO

-- Move from unverified to verified company 'Prime Appointments' for email domain '@primeappointments.com'
UPDATE Employer SET companyId = 'dd0a78b0-6010-4cb9-9fe3-8a952aef1adb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Prime Appointments' AND c.verifiedById IS NULL AND emailAddress LIKE '%@primeappointments.com'
GO

-- Move from unverified to verified company 'Primus' for email domain '@primustel.com.au'
UPDATE Employer SET companyId = 'c99df3dd-0a1c-4f95-bcc0-fd12d40d42af' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Primus' AND c.verifiedById IS NULL AND emailAddress LIKE '%@primustel.com.au'
GO

-- Move from unverified to verified company 'Professional Golfers Association' for email domain '@pga.org.au'
UPDATE Employer SET companyId = 'd8a23a5e-0049-429b-919f-3eb968538156' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Golfers Association' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pga.org.au'
GO

-- Move from unverified to verified company 'Quadrant Recruitment' for email domain '@quadrantrecruit.com.au'
UPDATE Employer SET companyId = 'e035fce4-3b09-4cb9-858c-9e847a4aed4e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quadrant Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@quadrantrecruit.com.au'
GO

-- Move from unverified to verified company 'RMA Online' for email domain '@rmaonline.com.au'
UPDATE Employer SET companyId = '428624dd-2241-4f98-8858-1b611512ac59' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'RMA Online' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rmaonline.com.au'
GO

-- Move from unverified to verified company 'RSP Recruitment' for email domain '@rsprecruitment.com.au'
UPDATE Employer SET companyId = '2a9b62d4-cf2a-4327-a4f1-dddd6f7299ac' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'RSP Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rsprecruitment.com.au'
GO

-- Move from unverified to verified company 'Salmat' for email domain '@mailus.com.au'
UPDATE Employer SET companyId = '62c36205-f6e8-4eb8-b7fa-d143d8017c5d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Salmat' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mailus.com.au'
GO

-- Move from unverified to verified company 'Select Accountancy' for email domain '@selectaccountancy.com.au'
UPDATE Employer SET companyId = '539747e8-e3cd-4930-b75d-c91af860d528' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Accountancy' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectaccountancy.com.au'
GO

-- Move from unverified to verified company 'Select Accountancy (QLD)' for email domain '@selectaccountancy.com.au'
UPDATE Employer SET companyId = 'cdca491c-1727-42ab-a51c-e3de32665756' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Accountancy (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectaccountancy.com.au'
GO

-- Move from unverified to verified company 'Select Appointments' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '29cba0bf-bcce-4ef4-8bb8-b2cd1f7102ad' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

-- Move from unverified to verified company 'Select Appointments (NSW)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '46b7bbee-01b2-466a-90b6-7552e301f40f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

-- Move from unverified to verified company 'Select Appointments (Paramatta)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '4ab39d78-3d8b-4122-8b2f-4f0d46bb69f6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (Paramatta)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

-- Move from unverified to verified company 'Select Appointments (QLD)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = 'c4149ffd-3f85-43b8-aa55-89feca6f2fbe' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

-- Move from unverified to verified company 'Select Appointments (VIC)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '4e424608-64bd-45f3-b85a-0963bcb18cf2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

-- Move from unverified to verified company 'Sensis' for email domain '@sensis.com.au'
UPDATE Employer SET companyId = '98298cc5-e9fb-4f41-8ca7-f5754d2b80de' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sensis' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sensis.com.au'
GO

-- Move from unverified to verified company 'Skandia' for email domain '@skandia.com'
UPDATE Employer SET companyId = '029985ad-2607-4cfe-bc5a-945dd7b82543' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Skandia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@skandia.com'
GO

-- Move from unverified to verified company 'St George Bank' for email domain '@stgeorge.com.au'
UPDATE Employer SET companyId = 'e2176c3d-cb63-4c66-a051-8d503c2142b7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'St George Bank' AND c.verifiedById IS NULL AND emailAddress LIKE '%@stgeorge.com.au'
GO

-- Move from unverified to verified company 'Stellar Recruitment' for email domain '@stellarrecruitment.com.au'
UPDATE Employer SET companyId = '91912f78-5fab-4744-b549-d0a5dbad71b4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Stellar Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@stellarrecruitment.com.au'
GO

-- Move from unverified to verified company 'Talent International' for email domain '@talentinternational.com.au'
UPDATE Employer SET companyId = 'ef93155e-9874-4252-8fa8-149a355ee5ca' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talentinternational.com.au'
GO

-- Move from unverified to verified company 'Talent Solutions International' for email domain '@talentsolutions.com.au'
UPDATE Employer SET companyId = '3fa03cf9-1781-46de-99f8-9b4b7e087f89' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent Solutions International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talentsolutions.com.au'
GO

-- Move from unverified to verified company 'Talent2 (NSW)' for email domain '@talent2.com'
UPDATE Employer SET companyId = 'f0bf1d55-7054-4a23-9441-80cee4a8c8ff' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2 (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com'
GO

-- Move from unverified to verified company 'Talent2 (QLD)' for email domain '@talent2.com'
UPDATE Employer SET companyId = '92fe5079-8bd5-434c-94f1-c859c21cc40e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2 (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com'
GO

-- Move from unverified to verified company 'Talent2 (SA)' for email domain '@talent2.com'
UPDATE Employer SET companyId = 'e2083f8d-3ebc-4106-84a9-6fa69d7fccdf' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2 (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com'
GO

-- Move from unverified to verified company 'Talent2 (VIC)' for email domain '@talent2.com'
UPDATE Employer SET companyId = '4d7d671e-3ed3-43fd-8278-de41265fbd71' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2 (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com'
GO

-- Move from unverified to verified company 'TCG Careers and Management' for email domain '@tcgcareers.com'
UPDATE Employer SET companyId = 'b55e487b-0437-4956-8234-30a5512980ca' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'TCG Careers and Management' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tcgcareers.com'
GO

-- Move from unverified to verified company 'Technical Focus' for email domain '@technicalfocus.com.au'
UPDATE Employer SET companyId = '25925e97-68d0-406d-a4f6-61ddc2fa22f4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Technical Focus' AND c.verifiedById IS NULL AND emailAddress LIKE '%@technicalfocus.com.au'
GO

-- Move from unverified to verified company 'Telstra Careers Centre' for email domain '@team.telstra.com'
UPDATE Employer SET companyId = '056cea94-14f9-4c41-a8bc-bb2848ae9f71' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Telstra Careers Centre' AND c.verifiedById IS NULL AND emailAddress LIKE '%@team.telstra.com'
GO

-- Move from unverified to verified company 'Telstra Careers Centre (NSW)' for email domain '@team.telstra.com'
UPDATE Employer SET companyId = '52c9185a-18eb-4b33-b9c6-a4dc330dd228' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Telstra Careers Centre (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@team.telstra.com'
GO

-- Move from unverified to verified company 'The i-Group' for email domain '@theigroup.com.au'
UPDATE Employer SET companyId = '26a2f2a0-5b91-4b14-8044-f0a0f25386b6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The i-Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@theigroup.com.au'
GO

-- Move from unverified to verified company 'Vantage Recruitment' for email domain '@vantagerecruitment.com.au'
UPDATE Employer SET companyId = '1bd33dd3-940d-466f-b171-1e5633b71c23' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Vantage Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@vantagerecruitment.com.au'
GO

-- Move from unverified to verified company 'Zelda Recruitment' for email domain '@zeldarecruitment.com.au'
UPDATE Employer SET companyId = '91462c6f-682b-4f51-96b2-dd80c717ae0d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Zelda Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@zeldarecruitment.com.au'
GO

-- Processing worksheet "Unverified company merges" (2nd column set)...

INSERT INTO Company ([id], [name], verifiedById) VALUES ('dc8aa403-ac94-44e2-b5e9-b6ab66f98799', 'Affinity IT (ACT)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Affinity IT (ACT)' for email domain '@affinityit.com.au'
UPDATE Employer SET companyId = 'dc8aa403-ac94-44e2-b5e9-b6ab66f98799' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Affinity IT (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@affinityit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7cf522e2-d965-4701-af33-c0e170783b75', 'Affinity IT (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Affinity IT (NSW)' for email domain '@affinityit.com.au'
UPDATE Employer SET companyId = '7cf522e2-d965-4701-af33-c0e170783b75' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Affinity IT (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@affinityit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('39374d47-14aa-48de-95aa-e404ed3b963d', 'Affinity IT (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Affinity IT (SA)' for email domain '@affinityit.com.au'
UPDATE Employer SET companyId = '39374d47-14aa-48de-95aa-e404ed3b963d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Affinity IT (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@affinityit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('39cce276-3ac7-457b-8df5-8f15f83b5039', 'Affinity IT (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Affinity IT (WA)' for email domain '@affinityit.com.au'
UPDATE Employer SET companyId = '39cce276-3ac7-457b-8df5-8f15f83b5039' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Affinity IT (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@affinityit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('15445da9-e5e6-48e6-874e-3a7d89a7085f', 'Ambit (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ambit (NSW)' for email domain '@peoplebank.com.au'
UPDATE Employer SET companyId = '15445da9-e5e6-48e6-874e-3a7d89a7085f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplebank.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8904823e-921e-424b-977a-6fb883fdd85b', 'Axis HR', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Axis HR' for email domain '@axishr.com.au'
UPDATE Employer SET companyId = '8904823e-921e-424b-977a-6fb883fdd85b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Axis HR' AND c.verifiedById IS NULL AND emailAddress LIKE '%@axishr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d7997043-8953-404d-9369-87b6226619ec', 'Beaumont Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Beaumont Consulting' for email domain '@beaumontconsulting.com.au'
UPDATE Employer SET companyId = 'd7997043-8953-404d-9369-87b6226619ec' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beaumont Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beaumontconsulting.com.au'
GO

-- Move from unverified to verified company 'Beilby (NSW)' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'dd8adf47-dc2e-415d-bd96-b2d07b6ef23f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d77e5556-1e51-4661-94c0-e647d9f42b49', 'Beilby (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Beilby (QLD)' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'd77e5556-1e51-4661-94c0-e647d9f42b49' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e00fb652-7c58-415e-8b31-c8e5a1adbeb0', 'Beilby (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Beilby (VIC)' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'e00fb652-7c58-415e-8b31-c8e5a1adbeb0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c154db3a-6307-4a24-b6ed-b284ef4de303', 'Beilby (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Beilby (WA)' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'c154db3a-6307-4a24-b6ed-b284ef4de303' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

-- Move from unverified company 'Beilby Corporation' to verified company 'Beilby' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'f29ab7c1-2797-4784-91ee-8ad8640caa50' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby Corporation' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

-- Move from unverified company 'Beilby Corporation Pty Ltd' to verified company 'Beilby' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'f29ab7c1-2797-4784-91ee-8ad8640caa50' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby Corporation Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

-- Move from unverified company 'Beilby Employment Network' to verified company 'Beilby' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'f29ab7c1-2797-4784-91ee-8ad8640caa50' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby Employment Network' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

-- Move from unverified company 'Beilby Staffing Services' to verified company 'Beilby' for email domain '@beilby.com.au'
UPDATE Employer SET companyId = 'f29ab7c1-2797-4784-91ee-8ad8640caa50' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Beilby Staffing Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@beilby.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4bae3309-a295-4f9b-9870-91feba176d5e', 'BigPond', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'BigPond' for email domain '@team.telstra.com'
UPDATE Employer SET companyId = '4bae3309-a295-4f9b-9870-91feba176d5e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'BigPond' AND c.verifiedById IS NULL AND emailAddress LIKE '%@team.telstra.com'
GO

-- Move from unverified company 'Boomerang' to verified company 'Boomerang Executive' for email domain '@boomerangexecutive.com.au'
UPDATE Employer SET companyId = '5ecbea0d-254c-4b2a-83cb-5ac524adc390' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Boomerang' AND c.verifiedById IS NULL AND emailAddress LIKE '%@boomerangexecutive.com.au'
GO

-- Move from unverified company 'BSI People Pty Limited' to verified company 'BSI People' for email domain '@bsipeople.com'
UPDATE Employer SET companyId = '2b479351-fe5a-424d-9230-8943f74a4d11' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'BSI People Pty Limited' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bsipeople.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bb32db03-bf42-483c-9bc3-7ec64157d67d', 'Candle ICT', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'Candle (NSW)' to verified company 'Candle ICT' for email domain '@candle.com.au'
UPDATE Employer SET companyId = 'bb32db03-bf42-483c-9bc3-7ec64157d67d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candle.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8b806931-f007-4e08-ab95-b519faea2e4d', 'Candle ICT (ACT)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Candle ICT (ACT)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = '8b806931-f007-4e08-ab95-b519faea2e4d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('90fb56ed-a8e7-43aa-8fa2-a4eb9207d176', 'Candle ICT (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Candle ICT (SA)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = '90fb56ed-a8e7-43aa-8fa2-a4eb9207d176' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('26f9238f-8a24-4fbf-b57c-3afea301c15e', 'Candle ICT (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Candle ICT (VIC)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = '26f9238f-8a24-4fbf-b57c-3afea301c15e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

-- Move from unverified company 'Candle Recruitment (WA)' to verified company 'Candle ICT (WA)' for email domain '@candlerecruit.com'
UPDATE Employer SET companyId = 'f483ec38-bbef-4d16-bfe5-b6a18e5db3f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle Recruitment (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b6c8c7a7-c953-43f7-ae26-92ccfcf14e76', 'Clements Recruitment (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Clements Recruitment (NSW)' for email domain '@clements.com.au'
UPDATE Employer SET companyId = 'b6c8c7a7-c953-43f7-ae26-92ccfcf14e76' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Clements Recruitment (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@clements.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6324d67a-d520-4c08-95f6-3263ca1f14eb', 'Clements Recruitment (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Clements Recruitment (SA)' for email domain '@clements.com.au'
UPDATE Employer SET companyId = '6324d67a-d520-4c08-95f6-3263ca1f14eb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Clements Recruitment (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@clements.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('27edca1b-1af3-4c26-9ccc-f58d3bdb36ef', 'Credit Recruitment (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Credit Recruitment (NSW)' for email domain '@creditrecruitment.com'
UPDATE Employer SET companyId = '27edca1b-1af3-4c26-9ccc-f58d3bdb36ef' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Credit Recruitment (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@creditrecruitment.com'
GO

-- Move from unverified company 'DeakinPrime' to verified company 'Deakin Prime' for email domain '@deakinprime.com'
UPDATE Employer SET companyId = 'd6540fee-4060-4539-ab02-76ca259db126' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DeakinPrime' AND c.verifiedById IS NULL AND emailAddress LIKE '%@deakinprime.com'
GO

-- Move from unverified to verified company 'Derwent Executive' for email domain '@derwentexec.com.au'
UPDATE Employer SET companyId = '982d4461-6f13-4eb9-9537-ca615e9f570c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Derwent Executive' AND c.verifiedById IS NULL AND emailAddress LIKE '%@derwentexec.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b0df61f0-4c64-4905-8e7b-2af6234334c5', 'DFP Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DFP Recruitment' for email domain '@dfp.com.au'
UPDATE Employer SET companyId = 'b0df61f0-4c64-4905-8e7b-2af6234334c5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DFP Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dfp.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('89e1c159-5c40-422e-be0a-48f18cac05b5', 'Directories Online', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Directories Online' for email domain '@brideonline.com.au'
UPDATE Employer SET companyId = '89e1c159-5c40-422e-be0a-48f18cac05b5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Directories Online' AND c.verifiedById IS NULL AND emailAddress LIKE '%@brideonline.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('092a8130-bf20-4949-8e70-5ca3afe74fc1', 'Downing Teal (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Downing Teal (NSW)' for email domain '@downingteal.com'
UPDATE Employer SET companyId = '092a8130-bf20-4949-8e70-5ca3afe74fc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Downing Teal (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@downingteal.com'
GO

-- Move from unverified to verified company 'Downing Teal (QLD)' for email domain '@dtworkforce.com'
UPDATE Employer SET companyId = '2748ad0b-b4bb-4e2d-ba3b-81f03b3cfbd8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Downing Teal (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dtworkforce.com'
GO

-- Move from unverified to verified company 'Downing Teal (QLD)' for email domain '@downingteal.com'
UPDATE Employer SET companyId = '2748ad0b-b4bb-4e2d-ba3b-81f03b3cfbd8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Downing Teal (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@downingteal.com'
GO

-- Move from unverified company 'Dowrick Pty. Ltd.' to verified company 'Dowrick' for email domain '@dowrick.com.au'
UPDATE Employer SET companyId = 'dedeb924-2434-4f9a-b916-283a6af3b945' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Dowrick Pty. Ltd.' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dowrick.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('26fc0dc2-7485-4227-9155-ff4d8fa22be6', 'DT Workforce', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DT Workforce' for email domain '@dtworkforce.com'
UPDATE Employer SET companyId = '26fc0dc2-7485-4227-9155-ff4d8fa22be6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DT Workforce' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dtworkforce.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8d31e092-81a1-410e-bc86-45a20b2223a4', 'DWS (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DWS (NSW)' for email domain '@dws.com.au'
UPDATE Employer SET companyId = '8d31e092-81a1-410e-bc86-45a20b2223a4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DWS (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dws.com.au'
GO

-- Move from unverified company 'eJobs Recruitment Specialists Pty Ltd' to verified company 'eJobs' for email domain '@ejobs.com.au'
UPDATE Employer SET companyId = 'dc4fd419-bb8c-4629-8b81-5cbd871dcae6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'eJobs Recruitment Specialists Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ejobs.com.au'
GO

-- Move from unverified company 'Ethos' to verified company 'Ethos Corporation' for email domain '@ethoscorporation.com.au'
UPDATE Employer SET companyId = 'e1dc1e60-4d2a-4347-80eb-7f1ee0eac0b2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ethos' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ethoscorporation.com.au'
GO

-- Move from unverified company 'Ethos' to verified company 'Ethos Corporation' for email domain '@ethoscorp.com.au'
UPDATE Employer SET companyId = 'e1dc1e60-4d2a-4347-80eb-7f1ee0eac0b2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ethos' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ethoscorp.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7f2b7531-35f2-4b7e-962f-ba8a72526510', 'Finlay Gooding & Partners', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Finlay Gooding & Partners' for email domain '@finlaypartners.com.au'
UPDATE Employer SET companyId = '7f2b7531-35f2-4b7e-962f-ba8a72526510' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Finlay Gooding & Partners' AND c.verifiedById IS NULL AND emailAddress LIKE '%@finlaypartners.com.au'
GO

-- Move from unverified company 'Fox Symes' to verified company 'Fox Symes Recruitment' for email domain '@fsarecruitment.com.au'
UPDATE Employer SET companyId = '42d49b43-0678-4c9d-8704-209092c84285' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Fox Symes' AND c.verifiedById IS NULL AND emailAddress LIKE '%@fsarecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3fc6ae8a-a5ed-465a-8529-9caf5fb3abf8', 'Future Prospects', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Future Prospects' for email domain '@macpeople.net'
UPDATE Employer SET companyId = '3fc6ae8a-a5ed-465a-8529-9caf5fb3abf8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Future Prospects' AND c.verifiedById IS NULL AND emailAddress LIKE '%@macpeople.net'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2b072d2e-5b3d-4767-b109-09515d32d403', 'GMT Recruitment (Brisbane)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'GMT (Brisbane)' to verified company 'GMT Recruitment (Brisbane)' for email domain '@gmtrecruitment.com.au'
UPDATE Employer SET companyId = '2b072d2e-5b3d-4767-b109-09515d32d403' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GMT (Brisbane)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gmtrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cc413a67-2578-4b5e-9d78-8a842bb1ea9f', 'GMT Recruitment (Gold Coast)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'GMT (Gold Coast)' to verified company 'GMT Recruitment (Gold Coast)' for email domain '@gmtrecruitment.com.au'
UPDATE Employer SET companyId = 'cc413a67-2578-4b5e-9d78-8a842bb1ea9f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GMT (Gold Coast)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gmtrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9c4d108d-64af-4812-bdec-6b2e3c01893b', 'GMT Recruitment (Melbourne)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'GMT (Melbourne)' to verified company 'GMT Recruitment (Melbourne)' for email domain '@gmtrecruitment.com.au'
UPDATE Employer SET companyId = '9c4d108d-64af-4812-bdec-6b2e3c01893b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GMT (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gmtrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9ce5753d-bb1f-4f06-8c53-0da3f742b9c2', 'GMT Recruitment (ACT)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'GMT Recruitment (ACT)' for email domain '@gmtrecruitment.com.au'
UPDATE Employer SET companyId = '9ce5753d-bb1f-4f06-8c53-0da3f742b9c2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GMT Recruitment (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gmtrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5cefbf66-f483-475c-b409-7a66e1e8a4e5', 'Hallis', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Hallis' for email domain '@hallis.com.au'
UPDATE Employer SET companyId = '5cefbf66-f483-475c-b409-7a66e1e8a4e5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hallis' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hallis.com.au'
GO

-- Move from unverified company 'Hallis (NSW' to verified company 'Hallis (NSW)' for email domain '@hallis.com.au'
UPDATE Employer SET companyId = '0a0a366d-b123-49be-964d-92ee51463517' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hallis (NSW' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hallis.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a470d0e7-8e92-47ae-a02a-87ccdf05abb4', 'Hamilton James & Bruce (ACT)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Hamilton James & Bruce (ACT)' for email domain '@hjb.com.au'
UPDATE Employer SET companyId = 'a470d0e7-8e92-47ae-a02a-87ccdf05abb4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hamilton James & Bruce (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hjb.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2372521b-e2f0-4ea8-a3b6-88e9c2d1fb5d', 'Hamilton James & Bruce (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Hamilton James & Bruce (NSW)' for email domain '@hjb.com.au'
UPDATE Employer SET companyId = '2372521b-e2f0-4ea8-a3b6-88e9c2d1fb5d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hamilton James & Bruce (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hjb.com.au'
GO

-- Move from unverified to verified company 'Hamilton James & Bruce (QLD)' for email domain '@hjb.com.au'
UPDATE Employer SET companyId = 'b9d058dd-366a-4cf1-998b-f102aaffde5f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hamilton James & Bruce (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hjb.com.au'
GO

-- Move from unverified to verified company 'Hamilton James & Bruce (QLD)' for email domain '@bowdens.com.au'
UPDATE Employer SET companyId = 'b9d058dd-366a-4cf1-998b-f102aaffde5f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hamilton James & Bruce (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bowdens.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1a4e35f2-0b4f-4a74-b8dc-f80e78401a8e', 'Hamilton James & Bruce (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Hamilton James & Bruce (VIC)' for email domain '@hjb.com.au'
UPDATE Employer SET companyId = '1a4e35f2-0b4f-4a74-b8dc-f80e78401a8e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hamilton James & Bruce (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hjb.com.au'
GO

-- Move from unverified company 'Hostec Global Search' to verified company 'Hostec' for email domain '@hostec.com.au'
UPDATE Employer SET companyId = '354d7910-7707-47fe-b235-68ff525bdef1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hostec Global Search' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hostec.com.au'
GO

-- Move from unverified company 'HR national (NSW)' to verified company 'HR National' for email domain '@hrnational.com.au'
UPDATE Employer SET companyId = '3bbb703a-a0cf-4df9-bd1b-a4611c92a167' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HR national (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hrnational.com.au'
GO

-- Move from unverified company 'HR National (QLD)' to verified company 'HR National' for email domain '@hrnational.com.au'
UPDATE Employer SET companyId = '3bbb703a-a0cf-4df9-bd1b-a4611c92a167' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HR National (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hrnational.com.au'
GO

-- Move from unverified company 'Interpro Pty Ltd' to verified company 'Interpro' for email domain '@interpro.com.au'
UPDATE Employer SET companyId = 'b18ff458-232b-4240-87f1-6d0a93f89cc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Interpro Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@interpro.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a9646eb8-0fdb-4d1c-be00-c4a950453952', 'Intersystems', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Intersystems' for email domain '@trakhealth.com'
UPDATE Employer SET companyId = 'a9646eb8-0fdb-4d1c-be00-c4a950453952' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Intersystems' AND c.verifiedById IS NULL AND emailAddress LIKE '%@trakhealth.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f2a81d73-a08b-4910-9a72-c38987e10134', 'IPA LawStaff', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA LawStaff' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'f2a81d73-a08b-4910-9a72-c38987e10134' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA LawStaff' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified company 'IPA Persnnel (VIC Industrial)' to verified company 'IPA Personnel (VIC)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'c6aeb474-4f13-4c7e-a464-dfab6a45ff0c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Persnnel (VIC Industrial)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified to verified company 'IPA Personnel' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'ce3c74c6-0bb2-457f-97df-1319a723991c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('108326fd-e9b6-4832-b3b2-a11a069334d7', 'IPA Personnel  (Corporate)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (Corporate)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '108326fd-e9b6-4832-b3b2-a11a069334d7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (Corporate)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1feeb50e-75d7-4222-8316-3f945d33d4d9', 'IPA Personnel  (CPE Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (CPE Sydney)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '1feeb50e-75d7-4222-8316-3f945d33d4d9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (CPE Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('73d911d0-59ad-49e1-8367-546716e4ecd1', 'IPA Personnel  (CPE Williamstown)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (CPE Williamstown)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '73d911d0-59ad-49e1-8367-546716e4ecd1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (CPE Williamstown)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8ca53f36-be20-40aa-bd09-a11d39f36fe9', 'IPA Personnel  (East Perth)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (East Perth)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '8ca53f36-be20-40aa-bd09-a11d39f36fe9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (East Perth)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('97a61ed5-efda-4798-8c50-34df2f72c4dc', 'IPA Personnel  (Emerald)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (Emerald)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '97a61ed5-efda-4798-8c50-34df2f72c4dc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (Emerald)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c2ad10f4-ad02-4d18-9469-0a72fbc8eb45', 'IPA Personnel  (Frankston)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (Frankston)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'c2ad10f4-ad02-4d18-9469-0a72fbc8eb45' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (Frankston)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8eb29a70-0d27-4fc8-bc99-080d9911be2d', 'IPA Personnel  (Gympie)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (Gympie)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '8eb29a70-0d27-4fc8-bc99-080d9911be2d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (Gympie)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7e630a12-bb46-4e98-8fd6-3c63cfce5cc6', 'IPA Personnel  (International WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (International WA)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '7e630a12-bb46-4e98-8fd6-3c63cfce5cc6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (International WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8fdbaa60-3ee6-4e40-9bf6-b215a4456dc8', 'IPA Personnel  (IPA Woolwoorths NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (IPA Woolwoorths NSW)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '8fdbaa60-3ee6-4e40-9bf6-b215a4456dc8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (IPA Woolwoorths NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f1a603bd-f2b9-4d5a-9025-24919bdca310', 'IPA Personnel  (IPA Woolwoorths QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (IPA Woolwoorths QLD)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'f1a603bd-f2b9-4d5a-9025-24919bdca310' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (IPA Woolwoorths QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('12aaa12f-113e-4edd-aac8-91f905e27407', 'IPA Personnel  (IST East Perth)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (IST East Perth)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '12aaa12f-113e-4edd-aac8-91f905e27407' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (IST East Perth)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9abad23f-4d19-4bd7-8170-314ca1321ace', 'IPA Personnel  (Melbourne)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel  (Melbourne)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '9abad23f-4d19-4bd7-8170-314ca1321ace' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel  (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7a52687a-6ab5-4d30-9b45-54cdc744964d', 'IPA Personnel (Adelaide)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Adelaide)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '7a52687a-6ab5-4d30-9b45-54cdc744964d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Adelaide)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('da6cc66f-e449-472f-acc0-cd6ce7a8a90a', 'IPA Personnel (Blacktown)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Blacktown)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'da6cc66f-e449-472f-acc0-cd6ce7a8a90a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Blacktown)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('991ea4d4-a45e-4cb4-8de4-4ff07d132ca7', 'IPA Personnel (Cairns)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Cairns)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '991ea4d4-a45e-4cb4-8de4-4ff07d132ca7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Cairns)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7f5b2a6f-0f68-4a37-80be-a0697daa1917', 'IPA Personnel (Clayton)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Clayton)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '7f5b2a6f-0f68-4a37-80be-a0697daa1917' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Clayton)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('aa4db743-2191-4df7-9012-49f8da39217a', 'IPA Personnel (IST Melbourne)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (IST Melbourne)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'aa4db743-2191-4df7-9012-49f8da39217a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (IST Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4e8ce5ba-6190-425b-9d2b-561ea635fc98', 'IPA Personnel (JN Bank Place)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (JN Bank Place)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '4e8ce5ba-6190-425b-9d2b-561ea635fc98' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (JN Bank Place)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('43080ca9-bdc4-4e87-9ce2-5ef002bc806b', 'IPA Personnel (JN Geelong)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (JN Geelong)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '43080ca9-bdc4-4e87-9ce2-5ef002bc806b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (JN Geelong)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('44b91236-8e4a-48df-8343-52c85ab76b7e', 'IPA Personnel (JN Newmarket)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (JN Newmarket)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '44b91236-8e4a-48df-8343-52c85ab76b7e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (JN Newmarket)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b5d4daf7-f08a-4dda-8cbe-eaf9afd83ab9', 'IPA Personnel (JN QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (JN QLD)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'b5d4daf7-f08a-4dda-8cbe-eaf9afd83ab9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (JN QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6a29e2f3-0245-4d4e-99ac-7fa1753d685a', 'IPA Personnel (JN VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (JN VIC)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '6a29e2f3-0245-4d4e-99ac-7fa1753d685a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (JN VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9689b39c-f30d-4cec-820f-cb82cc2c39af', 'IPA Personnel (Kawana)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Kawana)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '9689b39c-f30d-4cec-820f-cb82cc2c39af' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Kawana)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e0bebcbf-29aa-4fdd-9dec-7bf66ce50262', 'IPA Personnel (Knox)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Knox)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'e0bebcbf-29aa-4fdd-9dec-7bf66ce50262' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Knox)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('eed1e99e-05b5-4941-9924-a1e5f01fc051', 'IPA Personnel (Mackay)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Mackay)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'eed1e99e-05b5-4941-9924-a1e5f01fc051' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Mackay)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9439bb23-17a5-4574-8ae1-bc96ecad0be0', 'IPA Personnel (Maroochydore)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Maroochydore)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '9439bb23-17a5-4574-8ae1-bc96ecad0be0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Maroochydore)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a822d66e-3118-45aa-838e-2e0f3a2b1d99', 'IPA Personnel (Mount Isa)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Mount Isa)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'a822d66e-3118-45aa-838e-2e0f3a2b1d99' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Mount Isa)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d552300b-4960-4129-b8f2-a22324a8ee11', 'IPA Personnel (Newcastle)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Newcastle)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'd552300b-4960-4129-b8f2-a22324a8ee11' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Newcastle)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified to verified company 'IPA Personnel (NSW)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '615e05a9-f09a-4330-b30b-02de4cb65a5b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9f1373ba-3c4b-47fe-af26-9af0c68d9a56', 'IPA Personnel (Parramatta)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Parramatta)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '9f1373ba-3c4b-47fe-af26-9af0c68d9a56' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Parramatta)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7a041131-226e-4e4e-8df7-6e9c5a573226', 'IPA Personnel (Perth)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Perth)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '7a041131-226e-4e4e-8df7-6e9c5a573226' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Perth)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e748c946-c411-4a3c-a303-1d6e707e954d', 'IPA Personnel (QLD Consult)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (QLD Consult)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'e748c946-c411-4a3c-a303-1d6e707e954d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (QLD Consult)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7da3de3c-20dd-40e9-b35e-59edf16ecaa9', 'IPA Personnel (QLD Internal Temps)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (QLD Internal Temps)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '7da3de3c-20dd-40e9-b35e-59edf16ecaa9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (QLD Internal Temps)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e6b66183-d058-4014-91a1-1271910e5723', 'IPA Personnel (Rockhampton)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Rockhampton)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'e6b66183-d058-4014-91a1-1271910e5723' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Rockhampton)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('72fc19a4-a107-4140-9d72-cd13a7a1122b', 'IPA Personnel (Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Sydney)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '72fc19a4-a107-4140-9d72-cd13a7a1122b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('972d511f-958e-4131-92e3-691627046697', 'IPA Personnel (Toowoomba)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Toowoomba)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '972d511f-958e-4131-92e3-691627046697' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Toowoomba)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c48e0039-f3a6-47a9-9473-5611e59dab53', 'IPA Personnel (Townsville)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Townsville)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'c48e0039-f3a6-47a9-9473-5611e59dab53' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Townsville)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0ac82336-3f1c-444a-9adc-1d046e943715', 'IPA Personnel (VIC International)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (VIC International)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '0ac82336-3f1c-444a-9adc-1d046e943715' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (VIC International)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('755b3f81-6d86-474c-80fa-323a798b42c1', 'IPA Personnel (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (WA)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '755b3f81-6d86-474c-80fa-323a798b42c1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e85ba92c-93ea-4528-80f0-ec8137a89b8e', 'IPA Personnel (Woolloongabba)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA Personnel (Woolloongabba)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'e85ba92c-93ea-4528-80f0-ec8137a89b8e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Woolloongabba)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified company 'IPA Personnel (Woolwoorths VIC)' to verified company 'IPA Personnel (VIC)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'c6aeb474-4f13-4c7e-a464-dfab6a45ff0c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Woolwoorths VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

-- Move from unverified company 'IPA Personnel (Woolworths VIC)' to verified company 'IPA Personnel (VIC)' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = 'c6aeb474-4f13-4c7e-a464-dfab6a45ff0c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA Personnel (Woolworths VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6b037cd6-2bbe-4d78-9c14-b8e497d615ce', 'IPA QLD', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IPA QLD' for email domain '@ipa.com.au'
UPDATE Employer SET companyId = '6b037cd6-2bbe-4d78-9c14-b8e497d615ce' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IPA QLD' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ipa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6ab1c9cd-7c2e-4992-8c61-c581bcab60bd', 'iPeople', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'iPeople Recruitment (VIC)' to verified company 'iPeople' for email domain '@i-people.com.au'
UPDATE Employer SET companyId = '6ab1c9cd-7c2e-4992-8c61-c581bcab60bd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'iPeople Recruitment (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@i-people.com.au'
GO

-- Move from unverified company 'IXP3 Pty Ltd' to verified company 'IXP3' for email domain '@ixp3.com'
UPDATE Employer SET companyId = 'b5d5d6c6-9846-4fa3-8b08-f6761ba234a8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IXP3 Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ixp3.com'
GO

-- Move from unverified company 'K2 Recruitmen' to verified company 'K2 Recruitment' for email domain '@k2.net.au'
UPDATE Employer SET companyId = '40b3fe16-1724-4311-9025-1ee229fad61f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'K2 Recruitmen' AND c.verifiedById IS NULL AND emailAddress LIKE '%@k2.net.au'
GO

-- Move from unverified company 'Kathi May' to verified company 'Sensis' for email domain '@sensis.com.au'
UPDATE Employer SET companyId = '98298cc5-e9fb-4f41-8ca7-f5754d2b80de' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Kathi May' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sensis.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d708cc03-236e-4414-84ba-2b9944392312', 'Key People (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Key People (WA)' for email domain '@keypeople.com.au'
UPDATE Employer SET companyId = 'd708cc03-236e-4414-84ba-2b9944392312' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Key People (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@keypeople.com.au'
GO

-- Move from unverified to verified company 'Law Staff' for email domain '@axishr.com.au'
UPDATE Employer SET companyId = '1cb0766a-0194-4cc2-80f6-0fa35d9437be' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Law Staff' AND c.verifiedById IS NULL AND emailAddress LIKE '%@axishr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a1412276-77c1-44fc-91f5-1ba2b0565bfd', 'LinkMeBDM', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'LinkMeBDM' for email domain '@yahoo.com.au'
UPDATE Employer SET companyId = 'a1412276-77c1-44fc-91f5-1ba2b0565bfd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'LinkMeBDM' AND c.verifiedById IS NULL AND emailAddress LIKE '%@yahoo.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1dd8e9ea-e2c7-4833-b87b-13bf15249965', 'Marker Consulting (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Marker Consulting (QLD)' for email domain '@markerconsulting.com'
UPDATE Employer SET companyId = '1dd8e9ea-e2c7-4833-b87b-13bf15249965' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Marker Consulting (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@markerconsulting.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('91371041-923b-4234-a44d-45cbb5a617bf', 'Michael Page (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Michael Page (NSW)' for email domain '@michaelpage.com.au'
UPDATE Employer SET companyId = '91371041-923b-4234-a44d-45cbb5a617bf' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Michael Page (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@michaelpage.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('52854e85-2474-4936-844d-86c5e6b08c66', 'Michael Page (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Michael Page (VIC)' for email domain '@michaelpage.com.au'
UPDATE Employer SET companyId = '52854e85-2474-4936-844d-86c5e6b08c66' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Michael Page (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@michaelpage.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fd709979-891a-44b9-9198-399a62b00f38', 'Michael Page (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Michael Page (WA)' for email domain '@michaelpage.com.au'
UPDATE Employer SET companyId = 'fd709979-891a-44b9-9198-399a62b00f38' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Michael Page (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@michaelpage.com.au'
GO

-- Move from unverified company 'MitchelLake Consulting' to verified company 'Mitchellake' for email domain '@mitchellake.com'
UPDATE Employer SET companyId = '9f179da7-81b4-43bb-a023-2ddcf3013432' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'MitchelLake Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mitchellake.com'
GO

-- Move from unverified company 'Mitchellake Consulting (Sydney)' to verified company 'Mitchellake' for email domain '@mitchellake.com'
UPDATE Employer SET companyId = '9f179da7-81b4-43bb-a023-2ddcf3013432' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mitchellake Consulting (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mitchellake.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e076fc38-8868-47c4-855f-13783965c891', 'Nizza Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Nizza Recruitment' for email domain '@nizza.com.au'
UPDATE Employer SET companyId = 'e076fc38-8868-47c4-855f-13783965c891' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Nizza Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@nizza.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('573c0554-777e-4086-9c59-8e4ef89c40fc', 'Olivier Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Olivier Group' for email domain '@olivier.com.au'
UPDATE Employer SET companyId = '573c0554-777e-4086-9c59-8e4ef89c40fc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Olivier Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@olivier.com.au'
GO

-- Move from unverified company 'Olivier Group Pty Ltd' to verified company 'Olivier Group' for email domain '@olivier.com.au'
UPDATE Employer SET companyId = '573c0554-777e-4086-9c59-8e4ef89c40fc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Olivier Group Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@olivier.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9edf777a-0f64-4fe4-859e-630016826f9f', 'On Call Communications', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'On Call Communications' for email domain '@com2.com.au'
UPDATE Employer SET companyId = '9edf777a-0f64-4fe4-859e-630016826f9f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'On Call Communications' AND c.verifiedById IS NULL AND emailAddress LIKE '%@com2.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fc39ee49-0148-420f-aa5e-ea68a50d0c6e', 'People Bank', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'People Bank' for email domain '@peoplebank.com.au'
UPDATE Employer SET companyId = 'fc39ee49-0148-420f-aa5e-ea68a50d0c6e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People Bank' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplebank.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fd2a4b0b-3d24-4567-9930-d5033c3a7b81', 'People Bank (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'People Bank (VIC)' for email domain '@peoplebank.com.au'
UPDATE Employer SET companyId = 'fd2a4b0b-3d24-4567-9930-d5033c3a7b81' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People Bank (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplebank.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c3c9e2e6-f710-4fc4-a1bf-b51490cd3dd0', 'People Bank (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'People Bank (WA)' for email domain '@peoplebank.com.au'
UPDATE Employer SET companyId = 'c3c9e2e6-f710-4fc4-a1bf-b51490cd3dd0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People Bank (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplebank.com.au'
GO

-- Move from unverified to verified company 'People Bank (WA)' for email domain '@gryphon.com.au'
UPDATE Employer SET companyId = 'c3c9e2e6-f710-4fc4-a1bf-b51490cd3dd0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People Bank (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gryphon.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4ff63dc5-2cf9-49af-8df2-f5e379af3f5a', 'Perform Recruitment Solutions (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Perform Recruitment Solutions (VIC)' for email domain '@performrecruitment.com.au'
UPDATE Employer SET companyId = '4ff63dc5-2cf9-49af-8df2-f5e379af3f5a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Perform Recruitment Solutions (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@performrecruitment.com.au'
GO

-- Move from unverified to verified company 'Pursuit Recruitment' for email domain '@1qr.com.au'
UPDATE Employer SET companyId = 'f956fcdb-e377-444a-ab08-ac32818f18b9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Pursuit Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@1qr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a4d1e9a8-8ce1-4dae-b102-3efc566aab52', 'Quadrant Recruit', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Quadrant Recruit' for email domain '@quadrantrecruit.com.au'
UPDATE Employer SET companyId = 'a4d1e9a8-8ce1-4dae-b102-3efc566aab52' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quadrant Recruit' AND c.verifiedById IS NULL AND emailAddress LIKE '%@quadrantrecruit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('087bc004-6fea-43ed-95fa-2980478ccb10', 'Recruitment National (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Recruitment National (NSW)' for email domain '@recruitmentnational.com.au'
UPDATE Employer SET companyId = '087bc004-6fea-43ed-95fa-2980478ccb10' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Recruitment National (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@recruitmentnational.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('324f9c77-14e6-4118-854e-b416613522de', 'Right Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Right Recruitment' for email domain '@bigpond.com.au'
UPDATE Employer SET companyId = '324f9c77-14e6-4118-854e-b416613522de' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Right Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bigpond.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6b219266-d032-4905-a500-c0616d1dbcd5', 'RMA', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'RMA' for email domain '@rmaonline.com.au'
UPDATE Employer SET companyId = '6b219266-d032-4905-a500-c0616d1dbcd5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'RMA' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rmaonline.com.au'
GO

-- Move from unverified company 'Robert Half International' to verified company 'Robert Half' for email domain '@roberthalf.com.au'
UPDATE Employer SET companyId = 'ff96695c-41c5-4b8d-a0b6-45dc5bfe37d6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Robert Half International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@roberthalf.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7a332ef3-c056-43cb-b7ab-429b49bc2ae8', 'Rodon Transport', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Rodon Transport' for email domain '@bigpond.com.au'
UPDATE Employer SET companyId = '7a332ef3-c056-43cb-b7ab-429b49bc2ae8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Rodon Transport' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bigpond.com.au'
GO

-- Move from unverified company 'RSP' to verified company 'RSP Recruitment' for email domain '@rsprecruitment.com.au'
UPDATE Employer SET companyId = '2a9b62d4-cf2a-4327-a4f1-dddd6f7299ac' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'RSP' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rsprecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('98f030e6-d128-4269-9ab9-ca74060c81b7', 'Sales Staff', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Sales Staff' for email domain '@salesstaff.com.au'
UPDATE Employer SET companyId = '98f030e6-d128-4269-9ab9-ca74060c81b7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sales Staff' AND c.verifiedById IS NULL AND emailAddress LIKE '%@salesstaff.com.au'
GO

-- Move from unverified company 'Select' to verified company 'Select Appointments' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '29cba0bf-bcce-4ef4-8bb8-b2cd1f7102ad' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

-- Move from unverified company 'Select' to verified company 'Select Accountancy' for email domain '@selectaccountancy.com.au'
UPDATE Employer SET companyId = '539747e8-e3cd-4930-b75d-c91af860d528' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectaccountancy.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('71daa8aa-c22b-43f6-b9b0-381678324568', 'Select Accountancy (Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Accountancy (Sydney)' for email domain '@selectaccountancy.com.au'
UPDATE Employer SET companyId = '71daa8aa-c22b-43f6-b9b0-381678324568' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Accountancy (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectaccountancy.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bf80206f-b16a-4b10-92d5-a3f19172399f', 'Select Accountancy (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Accountancy (VIC)' for email domain '@selectaccountancy.com.au'
UPDATE Employer SET companyId = 'bf80206f-b16a-4b10-92d5-a3f19172399f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Accountancy (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectaccountancy.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3913227c-c030-4a27-be92-eb323529e34c', 'Select Appointment (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointment (VIC)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '3913227c-c030-4a27-be92-eb323529e34c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointment (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5c5f6ef3-18a1-4cc5-bfdd-13b945511df7', 'Select Appointments (ACT)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (ACT)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '5c5f6ef3-18a1-4cc5-bfdd-13b945511df7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5bf48669-64c9-4783-b15c-be49d656695d', 'Select Appointments (Canberra)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (Canberra)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '5bf48669-64c9-4783-b15c-be49d656695d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (Canberra)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('650ad48e-8783-411c-89a4-6effe7c43557', 'Select Appointments (Central Coast)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (Central Coast)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '650ad48e-8783-411c-89a4-6effe7c43557' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (Central Coast)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3cb085e1-d14f-4284-930e-12f843671dc6', 'Select Appointments (Newcastle)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (Newcastle)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '3cb085e1-d14f-4284-930e-12f843671dc6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (Newcastle)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9d659acf-a50e-498c-8456-4360794a7dd2', 'Select Appointments (North Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (North Sydney)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '9d659acf-a50e-498c-8456-4360794a7dd2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (North Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3bd33385-d6c8-4fae-9771-bca6b34e0339', 'Select Appointments (NSW)H', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (NSW)H' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '3bd33385-d6c8-4fae-9771-bca6b34e0339' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (NSW)H' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('41a5b795-b27e-4127-adaf-1c5867ef3663', 'Select Appointments (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (SA)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = '41a5b795-b27e-4127-adaf-1c5867ef3663' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fdb6771c-c162-4c3f-89d8-7802e0b1bc2b', 'Select Appointments (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments (WA)' for email domain '@selectappointments.com.au'
UPDATE Employer SET companyId = 'fdb6771c-c162-4c3f-89d8-7802e0b1bc2b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectappointments.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f90fb9db-4182-436e-816d-f70158fc4af7', 'Sensis Careers Centre', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'Sensis (Melbourne)' to verified company 'Sensis Careers Centre' for email domain '@hudson.com'
UPDATE Employer SET companyId = 'f90fb9db-4182-436e-816d-f70158fc4af7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sensis (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hudson.com'
GO

-- Move from unverified company 'Sensis (QLD)' to verified company 'Sensis Careers Centre' for email domain '@hudson.com'
UPDATE Employer SET companyId = 'f90fb9db-4182-436e-816d-f70158fc4af7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sensis (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hudson.com'
GO

-- Move from unverified company 'Sensis (Sydney)' to verified company 'Sensis Careers Centre' for email domain '@hudson.com'
UPDATE Employer SET companyId = 'f90fb9db-4182-436e-816d-f70158fc4af7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sensis (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hudson.com'
GO

-- Move from unverified company 'Sensis (VIC)' to verified company 'Sensis Careers Centre' for email domain '@hudson.com'
UPDATE Employer SET companyId = 'f90fb9db-4182-436e-816d-f70158fc4af7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sensis (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hudson.com'
GO

-- Move from unverified company 'Sensis Pty Ltd' to verified company 'Sensis' for email domain '@sensis.com.au'
UPDATE Employer SET companyId = '98298cc5-e9fb-4f41-8ca7-f5754d2b80de' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sensis Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sensis.com.au'
GO

-- Move from unverified to verified company 'Slade Group' for email domain '@sladegroup.com.au'
UPDATE Employer SET companyId = '91e0d2a6-d06d-49b6-a182-df4cacfa0e8f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Slade Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sladegroup.com.au'
GO

-- Move from unverified company 'Slade Group (VIC)' to verified company 'Slade Group' for email domain '@sladegroup.com.au'
UPDATE Employer SET companyId = '91e0d2a6-d06d-49b6-a182-df4cacfa0e8f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Slade Group (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sladegroup.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d5e82d12-48be-4b8c-8631-44bb55900bcb', 'St George', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'St George' for email domain '@stgeorge.com.au'
UPDATE Employer SET companyId = 'd5e82d12-48be-4b8c-8631-44bb55900bcb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'St George' AND c.verifiedById IS NULL AND emailAddress LIKE '%@stgeorge.com.au'
GO

-- Move from unverified company 'St. George Bank' to verified company 'St George' for email domain '@stgeorge.com.au'
UPDATE Employer SET companyId = 'd5e82d12-48be-4b8c-8631-44bb55900bcb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'St. George Bank' AND c.verifiedById IS NULL AND emailAddress LIKE '%@stgeorge.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('df24de26-3adf-40d9-8a4d-a5f720fdfa71', 'Stirling Consolidated', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Stirling Consolidated' for email domain '@bigpond.com.au'
UPDATE Employer SET companyId = 'df24de26-3adf-40d9-8a4d-a5f720fdfa71' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Stirling Consolidated' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bigpond.com.au'
GO

-- Move from unverified to verified company 'TAD' for email domain '@tad.com.au'
UPDATE Employer SET companyId = 'd5a1bff1-7ed1-4a74-838b-7afc687517fd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'TAD' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tad.com.au'
GO

-- Move from unverified to verified company 'Talent2' for email domain '@talent2.com'
UPDATE Employer SET companyId = '1da638c3-43b8-46e5-8c7f-9699842b401f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com'
GO

-- Move from unverified to verified company 'Talent2' for email domain '@Talent2.com.au'
UPDATE Employer SET companyId = '1da638c3-43b8-46e5-8c7f-9699842b401f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2' AND c.verifiedById IS NULL AND emailAddress LIKE '%@Talent2.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('258dbcf7-06d2-474e-aada-942387c2373d', 'Talent2 (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Talent2 (WA)' for email domain '@talent2.com'
UPDATE Employer SET companyId = '258dbcf7-06d2-474e-aada-942387c2373d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2 (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com'
GO

-- Move from unverified company 'Talent2 International' to verified company 'Talent2' for email domain '@talent2.com.au'
UPDATE Employer SET companyId = '1da638c3-43b8-46e5-8c7f-9699842b401f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent2 International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ed1c04bc-31d0-49a6-8351-16f858963911', 'Testing', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Testing' for email domain '@test.linkme.net.au'
UPDATE Employer SET companyId = 'ed1c04bc-31d0-49a6-8351-16f858963911' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Testing' AND c.verifiedById IS NULL AND emailAddress LIKE '%@test.linkme.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('83f44eeb-43f4-458b-adfd-5f7e5ff6e25d', 'TRA Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'TRA Global' to verified company 'TRA Group' for email domain '@tra-group.com'
UPDATE Employer SET companyId = '83f44eeb-43f4-458b-adfd-5f7e5ff6e25d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'TRA Global' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tra-group.com'
GO

-- Move from unverified company 'Trading Post' to verified company 'Talent2' for email domain '@talent2.com.au'
UPDATE Employer SET companyId = '1da638c3-43b8-46e5-8c7f-9699842b401f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Trading Post' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talent2.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6dd8956d-1acd-4db6-bab9-eddb4be8bf41', 'Trident', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Trident' for email domain '@trident.com.au'
UPDATE Employer SET companyId = '6dd8956d-1acd-4db6-bab9-eddb4be8bf41' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Trident' AND c.verifiedById IS NULL AND emailAddress LIKE '%@trident.com.au'
GO

-- Move from unverified company 'Vantage Recruitment (WA)' to verified company 'Vantage Recruitment' for email domain '@vantagerecruitment.com.au'
UPDATE Employer SET companyId = '1bd33dd3-940d-466f-b171-1e5633b71c23' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Vantage Recruitment (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@vantagerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7e2fb34a-7270-45ae-a98f-007b9e2f83df', 'Wesfarmers Industrial and Safety', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Wesfarmers Industrial and Safety' for email domain '@wisau.com.au'
UPDATE Employer SET companyId = '7e2fb34a-7270-45ae-a98f-007b9e2f83df' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Wesfarmers Industrial and Safety' AND c.verifiedById IS NULL AND emailAddress LIKE '%@wisau.com.au'
GO

-- Move from unverified company 'Command Recruitment' to verified company 'Command Recruitment Group' for email domain '@command.com.au'
UPDATE Employer SET companyId = '46fafee5-11e1-45b2-afff-b407a8060e13' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Command Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@command.com.au'
GO

-- Processing worksheet "Unverified company merges" (3rd column set)...

INSERT INTO Company ([id], [name], verifiedById) VALUES ('30282fad-e531-4415-9019-9258fa2ea67f', 'AAMI', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'AAMI' for email domain '@aami.com.au'
UPDATE Employer SET companyId = '30282fad-e531-4415-9019-9258fa2ea67f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'AAMI' AND c.verifiedById IS NULL AND emailAddress LIKE '%@aami.com.au'
GO

-- Move from unverified to verified company 'Abbertons' for email domain '@abbertons.com.au'
UPDATE Employer SET companyId = 'fbe82363-0de4-4575-bbc5-b7bd561db981' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Abbertons' AND c.verifiedById IS NULL AND emailAddress LIKE '%@abbertons.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('614e7d92-7e27-40c5-8319-1c30c788c9a1', 'Abbertons Human Resources', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Abbertons Human Resources' for email domain '@abbertons.com.au'
UPDATE Employer SET companyId = '614e7d92-7e27-40c5-8319-1c30c788c9a1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Abbertons Human Resources' AND c.verifiedById IS NULL AND emailAddress LIKE '%@abbertons.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bc2d76da-45e1-4d68-ad97-998c42fec784', 'Abrahams', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Abrahams' for email domain '@abrahams.com.au'
UPDATE Employer SET companyId = 'bc2d76da-45e1-4d68-ad97-998c42fec784' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Abrahams' AND c.verifiedById IS NULL AND emailAddress LIKE '%@abrahams.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f44500a5-4fed-48dc-bba0-2b889e7be2ae', 'Acceleration Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Acceleration Group' for email domain '@acceleration.com.au'
UPDATE Employer SET companyId = 'f44500a5-4fed-48dc-bba0-2b889e7be2ae' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Acceleration Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@acceleration.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fdaf6e3a-3426-4300-80a7-09b903895981', 'ACQ', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ACQ' for email domain '@acq.edu.au'
UPDATE Employer SET companyId = 'fdaf6e3a-3426-4300-80a7-09b903895981' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ACQ' AND c.verifiedById IS NULL AND emailAddress LIKE '%@acq.edu.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0006e3bd-9b64-47ae-9814-3638aa720848', 'Active Occupation Health Services', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Active Occupation Health Services' for email domain '@activeohs.com.au'
UPDATE Employer SET companyId = '0006e3bd-9b64-47ae-9814-3638aa720848' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Active Occupation Health Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@activeohs.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7010db68-8bba-46d7-a12a-ccf8fb326c54', 'Adams Interim Management', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Adams Interim Management' for email domain '@adamsinterim.com'
UPDATE Employer SET companyId = '7010db68-8bba-46d7-a12a-ccf8fb326c54' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Adams Interim Management' AND c.verifiedById IS NULL AND emailAddress LIKE '%@adamsinterim.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('86e79361-5609-4e92-afbb-dd0fe70b69b8', 'AdLogic', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'AdLogic' for email domain '@martianlogic.com.au'
UPDATE Employer SET companyId = '86e79361-5609-4e92-afbb-dd0fe70b69b8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'AdLogic' AND c.verifiedById IS NULL AND emailAddress LIKE '%@martianlogic.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bcf9a945-2b9d-41f7-84fb-b30cf7d862a9', 'Advance Executive Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Advance Executive Group' for email domain '@advanceexec.com.au'
UPDATE Employer SET companyId = 'bcf9a945-2b9d-41f7-84fb-b30cf7d862a9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Advance Executive Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@advanceexec.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('34710ffa-9c30-4482-8bf1-c85fb5086c07', 'Advantech Software', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Advantech Software' for email domain '@advantechsoftware.com.au'
UPDATE Employer SET companyId = '34710ffa-9c30-4482-8bf1-c85fb5086c07' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Advantech Software' AND c.verifiedById IS NULL AND emailAddress LIKE '%@advantechsoftware.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('16a72601-cb71-4978-a740-2e26e14099d1', 'Alcoa Fastening Systems', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alcoa Fastening Systems' for email domain '@alcoa.com.au'
UPDATE Employer SET companyId = '16a72601-cb71-4978-a740-2e26e14099d1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alcoa Fastening Systems' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alcoa.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('06b27055-2fb2-480d-ba47-a8dbfcf30806', 'All Pacific Travel Concept', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'All Pacific Travel Concept' for email domain '@aptc.com.au'
UPDATE Employer SET companyId = '06b27055-2fb2-480d-ba47-a8dbfcf30806' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'All Pacific Travel Concept' AND c.verifiedById IS NULL AND emailAddress LIKE '%@aptc.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fe4b4cdc-701e-4872-9531-f0e2d3b6fbcc', 'All Type Diesel Services', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'All Type Diesel Services' for email domain '@bigpond.com'
UPDATE Employer SET companyId = 'fe4b4cdc-701e-4872-9531-f0e2d3b6fbcc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'All Type Diesel Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bigpond.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a8b93160-1fc0-4189-b430-3fedfc89d89a', 'Alliance (NS)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance (NS)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = 'a8b93160-1fc0-4189-b430-3fedfc89d89a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance (NS)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('85b662a4-7248-4602-b220-162cfff0d93d', 'Alliance (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance (VIC)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '85b662a4-7248-4602-b220-162cfff0d93d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

-- Move from unverified to verified company 'Alliance Accounting (NSW)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '1663edc1-78d5-4b3f-a997-59d3dc0e669a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Accounting (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3713d0f7-aa88-48cd-b4ad-9d3a42798ca1', 'Alliance Accounting (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Accounting (VIC)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '3713d0f7-aa88-48cd-b4ad-9d3a42798ca1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Accounting (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('152c58be-f98e-43c6-84e4-d2e691fe4a87', 'Alliance Business Support (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Business Support (NSW)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '152c58be-f98e-43c6-84e4-d2e691fe4a87' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Business Support (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('87704a29-c717-43fc-9702-68365d03627d', 'Alliance Business Support (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Business Support (QLD)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '87704a29-c717-43fc-9702-68365d03627d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Business Support (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a8832192-7a7f-470b-931c-272afc5bea84', 'Alliance Business Support (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Business Support (SA)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = 'a8832192-7a7f-470b-931c-272afc5bea84' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Business Support (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fb5f19c9-466f-43f4-ae01-eeea69b6525b', 'Alliance Business Support (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Business Support (VIC)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = 'fb5f19c9-466f-43f4-ae01-eeea69b6525b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Business Support (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d129d328-29fe-4785-b1f4-5268c48d5022', 'Alliance Business Support (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Business Support (WA)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = 'd129d328-29fe-4785-b1f4-5268c48d5022' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Business Support (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('47b9f6fc-0608-4265-9faa-5ff0074c0e61', 'Alliance Financial Services (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Financial Services (NSW)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '47b9f6fc-0608-4265-9faa-5ff0074c0e61' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Financial Services (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cc91c75c-a749-4c8f-85b7-2705540611ee', 'Alliance Financial Services (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Financial Services (QLD)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = 'cc91c75c-a749-4c8f-85b7-2705540611ee' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Financial Services (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('42b1798b-90c4-4629-91a9-07d85034f54c', 'Alliance Financial Services (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Financial Services (VIC)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = '42b1798b-90c4-4629-91a9-07d85034f54c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Financial Services (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

-- Move from unverified to verified company 'Alliance Financial Services (VIC)' for email domain '@freemanadams.com'
UPDATE Employer SET companyId = '42b1798b-90c4-4629-91a9-07d85034f54c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Financial Services (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@freemanadams.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e64366df-10eb-477d-ae02-6b5f54e49c96', 'Alliance Recruitment (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alliance Recruitment (QLD)' for email domain '@alliancerecruitment.com.au'
UPDATE Employer SET companyId = 'e64366df-10eb-477d-ae02-6b5f54e49c96' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alliance Recruitment (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alliancerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f5ba2042-599e-448b-aa07-a9f89e1f0c80', 'Alsco', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Alsco' for email domain '@alsco.com.au'
UPDATE Employer SET companyId = 'f5ba2042-599e-448b-aa07-a9f89e1f0c80' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alsco' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alsco.com.au'
GO

-- Move from unverified company 'Alsco Direct' to verified company 'Alsco' for email domain '@alsco.com.au'
UPDATE Employer SET companyId = 'f5ba2042-599e-448b-aa07-a9f89e1f0c80' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Alsco Direct' AND c.verifiedById IS NULL AND emailAddress LIKE '%@alsco.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('97ee37a6-8638-458d-a228-2073d7817cc9', 'Ambit', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'ambit' to verified company 'Ambit' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = '97ee37a6-8638-458d-a228-2073d7817cc9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ambit' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('330efce7-e093-4738-a778-a2326d7b6b10', 'Ambit (ACT)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ambit (ACT)' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = '330efce7-e093-4738-a778-a2326d7b6b10' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

-- Move from unverified to verified company 'Ambit (NSW)' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = '15445da9-e5e6-48e6-874e-3a7d89a7085f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e9eb518e-2781-4ea5-86d0-17fbd60282dc', 'Ambit (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ambit (QLD)' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = 'e9eb518e-2781-4ea5-86d0-17fbd60282dc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f3bc582a-8e7a-4469-b5d6-026bf6ed6faa', 'Ambit (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ambit (VIC)' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = 'f3bc582a-8e7a-4469-b5d6-026bf6ed6faa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

-- Move from unverified to verified company 'Ambit (VIC)' for email domain '@ambitgroup.com.au'
UPDATE Employer SET companyId = 'f3bc582a-8e7a-4469-b5d6-026bf6ed6faa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambitgroup.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d57a7e5f-b9ce-4230-a77e-74874ad167b3', 'Ambit (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ambit (WA)' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = 'd57a7e5f-b9ce-4230-a77e-74874ad167b3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

-- Move from unverified company 'Ambit Recruitment' to verified company 'Ambit' for email domain '@ambit.com.au'
UPDATE Employer SET companyId = '97ee37a6-8638-458d-a228-2073d7817cc9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambit Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e19a5606-d1e7-4f13-afbc-07249d980ae9', 'Ambition', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ambition' for email domain '@ambition.com.au'
UPDATE Employer SET companyId = 'e19a5606-d1e7-4f13-afbc-07249d980ae9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ambition' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ambition.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('710316d1-b992-440f-8dc4-4dff5a8ce4f2', 'American Express (Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'American Express (Sydney)' for email domain '@aexp.com'
UPDATE Employer SET companyId = '710316d1-b992-440f-8dc4-4dff5a8ce4f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'American Express (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@aexp.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a95afdc7-5abd-4d10-852b-a85ba6098550', 'Amtil', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Amtil' for email domain '@amtil.com.au'
UPDATE Employer SET companyId = 'a95afdc7-5abd-4d10-852b-a85ba6098550' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Amtil' AND c.verifiedById IS NULL AND emailAddress LIKE '%@amtil.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('50b4d5ae-79bc-4093-811e-58b8b82b1ee7', 'ANZ Breast Cancer Trials Group Limited', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ANZ Breast Cancer Trials Group Limited' for email domain '@anzbctg.newcastle.edu.au'
UPDATE Employer SET companyId = '50b4d5ae-79bc-4093-811e-58b8b82b1ee7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ANZ Breast Cancer Trials Group Limited' AND c.verifiedById IS NULL AND emailAddress LIKE '%@anzbctg.newcastle.edu.au'
GO

-- Move from unverified to verified company 'Asphar & Associates' for email domain '@asphar.com.au'
UPDATE Employer SET companyId = '3902bcc3-95d1-4615-9cd8-97bb063851f8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Asphar & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@asphar.com.au'
GO

-- Move from unverified company 'Asphar and Associates' to verified company 'Asphar & Associates' for email domain '@asphar.com.au'
UPDATE Employer SET companyId = '3902bcc3-95d1-4615-9cd8-97bb063851f8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Asphar and Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@asphar.com.au'
GO

-- Move from unverified to verified company 'Assign Recruitment' for email domain '@assign.com.au'
UPDATE Employer SET companyId = '0515c543-911d-4b44-b5b0-3b9f01704413' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Assign Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@assign.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c643e990-9389-456b-8c4e-72bc261c077b', 'Atlassian', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Atlassian' for email domain '@atlassian.com'
UPDATE Employer SET companyId = 'c643e990-9389-456b-8c4e-72bc261c077b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Atlassian' AND c.verifiedById IS NULL AND emailAddress LIKE '%@atlassian.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8d0ddef1-3fc4-4966-ab32-937815d0a980', 'Atmosphere One', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Atmosphere One' for email domain '@atmosphereone.com.au'
UPDATE Employer SET companyId = '8d0ddef1-3fc4-4966-ab32-937815d0a980' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Atmosphere One' AND c.verifiedById IS NULL AND emailAddress LIKE '%@atmosphereone.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3081cdfe-5770-4d0e-a637-f566a071bd22', 'Attain Kendall Williams (DFP VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Attain Kendall Williams (DFP VIC)' for email domain '@attain.com.au'
UPDATE Employer SET companyId = '3081cdfe-5770-4d0e-a637-f566a071bd22' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Attain Kendall Williams (DFP VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@attain.com.au'
GO

-- Move from unverified to verified company 'Australian Aerospace Resources Pty. Ltd' for email domain '@aaresources.com'
UPDATE Employer SET companyId = '86509a3b-5edc-4e77-814e-4e8f5f1d9a55' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Australian Aerospace Resources Pty. Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@aaresources.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d3e9d6c3-032d-4f61-9f27-ab73c63c0061', 'Australian Recruiting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Australian Recruiting' for email domain '@australianrecruiting.com'
UPDATE Employer SET companyId = 'd3e9d6c3-032d-4f61-9f27-ab73c63c0061' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Australian Recruiting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@australianrecruiting.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f8bfc523-24a3-44b7-833c-36fd1772d9fb', 'Australian Volunteers International', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Australian Volunteers International' for email domain '@australianvolunteers.com'
UPDATE Employer SET companyId = 'f8bfc523-24a3-44b7-833c-36fd1772d9fb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Australian Volunteers International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@australianvolunteers.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c90dd046-e1c6-4137-95f9-a71b5e8ab42d', 'Australis Engineering', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Australis Engineering' for email domain '@australiseng.com'
UPDATE Employer SET companyId = 'c90dd046-e1c6-4137-95f9-a71b5e8ab42d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Australis Engineering' AND c.verifiedById IS NULL AND emailAddress LIKE '%@australiseng.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('24585fad-04dd-4168-9498-74cc92b9fc06', 'Auto IT', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Auto IT' for email domain '@autoit.com.au'
UPDATE Employer SET companyId = '24585fad-04dd-4168-9498-74cc92b9fc06' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Auto IT' AND c.verifiedById IS NULL AND emailAddress LIKE '%@autoit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('12ffb461-814c-463f-886c-1264c37f9155', 'Autopeople', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Autopeople' for email domain '@hotmail.com'
UPDATE Employer SET companyId = '12ffb461-814c-463f-886c-1264c37f9155' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Autopeople' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4bdbd0c8-d4ac-416d-b2cd-b8787fe790be', 'Avaland Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Avaland Pty Ltd' for email domain '@pobox.com'
UPDATE Employer SET companyId = '4bdbd0c8-d4ac-416d-b2cd-b8787fe790be' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Avaland Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pobox.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('be7b5a87-fcc9-4e36-a8de-a7cead9452c4', 'Avanade Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Avanade Australia' for email domain '@avanade.com'
UPDATE Employer SET companyId = 'be7b5a87-fcc9-4e36-a8de-a7cead9452c4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Avanade Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@avanade.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cdda8dce-474f-4df1-9845-7df0b253c126', 'ayuda IT', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ayuda IT' for email domain '@ayuda.com.au'
UPDATE Employer SET companyId = 'cdda8dce-474f-4df1-9845-7df0b253c126' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ayuda IT' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ayuda.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7deec1ef-487d-4c1c-9504-dd2aa2def9c6', 'Bayford Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Bayford Group' for email domain '@bayford.com.au'
UPDATE Employer SET companyId = '7deec1ef-487d-4c1c-9504-dd2aa2def9c6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Bayford Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bayford.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d69eaaa0-effc-48e6-abd7-dc8b8e76ad89', 'Bill Lang International Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Bill Lang International Pty Ltd' for email domain '@billlang.org'
UPDATE Employer SET companyId = 'd69eaaa0-effc-48e6-abd7-dc8b8e76ad89' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Bill Lang International Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@billlang.org'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('31935a8d-57b2-4c88-8d8e-2145b45b6748', 'Blue Tongue Recruit', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Blue Tongue Recruit' for email domain '@bluetonguerecruit.com'
UPDATE Employer SET companyId = '31935a8d-57b2-4c88-8d8e-2145b45b6748' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Blue Tongue Recruit' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bluetonguerecruit.com'
GO

-- Move from unverified to verified company 'Blue Tongue Recruit' for email domain '@bluetonguerecruit.com.au'
UPDATE Employer SET companyId = '31935a8d-57b2-4c88-8d8e-2145b45b6748' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Blue Tongue Recruit' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bluetonguerecruit.com.au'
GO

-- Move from unverified to verified company 'Blue Tongue Recruit' for email domain '@edgepp.com'
UPDATE Employer SET companyId = '31935a8d-57b2-4c88-8d8e-2145b45b6748' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Blue Tongue Recruit' AND c.verifiedById IS NULL AND emailAddress LIKE '%@edgepp.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2c5e6d09-054a-46bf-a3d9-236d7154354c', 'Bluescope Steel', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Bluescope Steel' for email domain '@bluescopesteel.com'
UPDATE Employer SET companyId = '2c5e6d09-054a-46bf-a3d9-236d7154354c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Bluescope Steel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bluescopesteel.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('75b6ed5a-df54-44eb-be6e-e929652c0972', 'Booran Holden', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Booran Holden' for email domain '@booran.com.au'
UPDATE Employer SET companyId = '75b6ed5a-df54-44eb-be6e-e929652c0972' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Booran Holden' AND c.verifiedById IS NULL AND emailAddress LIKE '%@booran.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b1fc6b85-88f4-4f23-aa41-e8ebddfcb5fb', 'Bus Advertising', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Bus Advertising' for email domain '@busadvertising.com.au'
UPDATE Employer SET companyId = 'b1fc6b85-88f4-4f23-aa41-e8ebddfcb5fb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Bus Advertising' AND c.verifiedById IS NULL AND emailAddress LIKE '%@busadvertising.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('61bb31b8-db3e-4c39-be8d-af69797c5d2b', 'Buson Auto Parts', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Buson Auto Parts' for email domain '@burson.com.au'
UPDATE Employer SET companyId = '61bb31b8-db3e-4c39-be8d-af69797c5d2b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Buson Auto Parts' AND c.verifiedById IS NULL AND emailAddress LIKE '%@burson.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e67af907-373e-4801-b9c8-6f27be65a8fa', 'C & A Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'C & A Recruitment' for email domain '@carecruitment.com.au'
UPDATE Employer SET companyId = 'e67af907-373e-4801-b9c8-6f27be65a8fa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'C & A Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@carecruitment.com.au'
GO

-- Move from unverified to verified company 'Candle ICT (ACT)' for email domain '@candlerecruit.com.au'
UPDATE Employer SET companyId = '8b806931-f007-4e08-ab95-b519faea2e4d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (ACT)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com.au'
GO

-- Move from unverified to verified company 'Candle ICT (QLD)' for email domain '@candlerecruit.com.au'
UPDATE Employer SET companyId = 'e04cc2f5-c613-46f7-a9e7-a80486dd3371' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com.au'
GO

-- Move from unverified to verified company 'Candle ICT (SA)' for email domain '@candlerecruit.com.au'
UPDATE Employer SET companyId = '90fb56ed-a8e7-43aa-8fa2-a4eb9207d176' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com.au'
GO

-- Move from unverified to verified company 'Candle ICT (VIC)' for email domain '@candlerecruit.com.au'
UPDATE Employer SET companyId = '26f9238f-8a24-4fbf-b57c-3afea301c15e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com.au'
GO

-- Move from unverified to verified company 'Candle ICT (WA)' for email domain '@candleict.com.au'
UPDATE Employer SET companyId = 'f483ec38-bbef-4d16-bfe5-b6a18e5db3f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candleict.com.au'
GO

-- Move from unverified to verified company 'Candle ICT (WA)' for email domain '@candlerecruit.com.au'
UPDATE Employer SET companyId = 'f483ec38-bbef-4d16-bfe5-b6a18e5db3f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Candle ICT (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@candlerecruit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c5874021-91db-4fa7-9026-d3e6c91262df', 'Careers Connections', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Careers Connections' for email domain '@ccjobs.com.au'
UPDATE Employer SET companyId = 'c5874021-91db-4fa7-9026-d3e6c91262df' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Careers Connections' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ccjobs.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2162da5d-336c-49b6-ae59-875e59fd169b', 'Carl Elliott & Co', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Carl Elliott & Co' for email domain '@carlelliott.com.au'
UPDATE Employer SET companyId = '2162da5d-336c-49b6-ae59-875e59fd169b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Carl Elliott & Co' AND c.verifiedById IS NULL AND emailAddress LIKE '%@carlelliott.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('29cc5988-c27e-4b2b-9265-bfbb5676a5ea', 'Celemetrix', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Celemetrix' for email domain '@celemetrix.com.au'
UPDATE Employer SET companyId = '29cc5988-c27e-4b2b-9265-bfbb5676a5ea' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Celemetrix' AND c.verifiedById IS NULL AND emailAddress LIKE '%@celemetrix.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9e6f13cd-f4ec-4283-99cc-c4edcb178db3', 'Challenge Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Challenge Recruitment' for email domain '@challengeltd.com'
UPDATE Employer SET companyId = '9e6f13cd-f4ec-4283-99cc-c4edcb178db3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Challenge Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@challengeltd.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8ae540ee-d53b-4d32-8463-a7c88fdc7888', 'Challenge Recrutiment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Challenge Recrutiment' for email domain '@challengeltd.com'
UPDATE Employer SET companyId = '8ae540ee-d53b-4d32-8463-a7c88fdc7888' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Challenge Recrutiment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@challengeltd.com'
GO

-- Move from unverified to verified company 'Challenge Recrutiment' for email domain '@challengltd.com'
UPDATE Employer SET companyId = '8ae540ee-d53b-4d32-8463-a7c88fdc7888' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Challenge Recrutiment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@challengltd.com'
GO

-- Move from unverified to verified company 'Challenge Recrutiment' for email domain '@choicehr.com'
UPDATE Employer SET companyId = '8ae540ee-d53b-4d32-8463-a7c88fdc7888' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Challenge Recrutiment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@choicehr.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4e499257-f7c8-42a5-a5d5-7744a594d33a', 'Chameleon Technology', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Chameleon Technology' for email domain '@chamtech.com.au'
UPDATE Employer SET companyId = '4e499257-f7c8-42a5-a5d5-7744a594d33a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Chameleon Technology' AND c.verifiedById IS NULL AND emailAddress LIKE '%@chamtech.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('aac5e002-b9ab-4403-9d5d-c266bf3cbfbe', 'Checkpoint', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Checkpoint' for email domain '@checkpt.com'
UPDATE Employer SET companyId = 'aac5e002-b9ab-4403-9d5d-c266bf3cbfbe' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Checkpoint' AND c.verifiedById IS NULL AND emailAddress LIKE '%@checkpt.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cedd11d4-508c-4059-ba31-154eb487c128', 'Chemtura', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Chemtura' for email domain '@chemtura.com'
UPDATE Employer SET companyId = 'cedd11d4-508c-4059-ba31-154eb487c128' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Chemtura' AND c.verifiedById IS NULL AND emailAddress LIKE '%@chemtura.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1e4b2d24-e4cd-4960-844e-4deb91c9ffce', 'City West Water', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'City West Water' for email domain '@citywestwater.com.au'
UPDATE Employer SET companyId = '1e4b2d24-e4cd-4960-844e-4deb91c9ffce' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'City West Water' AND c.verifiedById IS NULL AND emailAddress LIKE '%@citywestwater.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('98c6447c-c1f3-4fe0-9bc9-890555679bbd', 'Classic Executive Recruiting Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Classic Executive Recruiting Pty Ltd' for email domain '@classicexec.com.au'
UPDATE Employer SET companyId = '98c6447c-c1f3-4fe0-9bc9-890555679bbd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Classic Executive Recruiting Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@classicexec.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0ae32e66-307a-47f6-9002-c68c8787d66a', 'CLM Excavations', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'CLM Excavations' for email domain '@clm.com.au'
UPDATE Employer SET companyId = '0ae32e66-307a-47f6-9002-c68c8787d66a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'CLM Excavations' AND c.verifiedById IS NULL AND emailAddress LIKE '%@clm.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6fa6344c-ad36-4b30-bd5b-63ddbaa6cb9e', 'CMA Electrical & Data', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'CMA Electrical & Data' for email domain '@picknowl.com.au'
UPDATE Employer SET companyId = '6fa6344c-ad36-4b30-bd5b-63ddbaa6cb9e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'CMA Electrical & Data' AND c.verifiedById IS NULL AND emailAddress LIKE '%@picknowl.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ae936def-8eef-4b9f-b783-b0b38356db29', 'Combo IT', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Combo IT' for email domain '@combo.com.au'
UPDATE Employer SET companyId = 'ae936def-8eef-4b9f-b783-b0b38356db29' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Combo IT' AND c.verifiedById IS NULL AND emailAddress LIKE '%@combo.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('27dbf184-86a5-452e-8219-6457f5189c95', 'Command', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Command' for email domain '@command.com'
UPDATE Employer SET companyId = '27dbf184-86a5-452e-8219-6457f5189c95' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Command' AND c.verifiedById IS NULL AND emailAddress LIKE '%@command.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d3b032a5-43b9-4b88-8da9-2202ccd8581f', 'Computers Now', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Computers Now' for email domain '@compnow.com.au'
UPDATE Employer SET companyId = 'd3b032a5-43b9-4b88-8da9-2202ccd8581f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Computers Now' AND c.verifiedById IS NULL AND emailAddress LIKE '%@compnow.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4deeb2ac-3aac-48c0-a921-e34990e50dd3', 'Computing Directions Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Computing Directions Australia' for email domain '@cda.com.au'
UPDATE Employer SET companyId = '4deeb2ac-3aac-48c0-a921-e34990e50dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Computing Directions Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@cda.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e1edfa23-c685-4e1e-8c2b-35310ba9ebfa', 'Construction Careers', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Construction Careers' for email domain '@constructioncareers.com.au'
UPDATE Employer SET companyId = 'e1edfa23-c685-4e1e-8c2b-35310ba9ebfa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Construction Careers' AND c.verifiedById IS NULL AND emailAddress LIKE '%@constructioncareers.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e2558137-18bd-4a47-ac03-f1f68fd36e98', 'Contract Personnel', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Contract Personnel' for email domain '@capablestaff.com.au'
UPDATE Employer SET companyId = 'e2558137-18bd-4a47-ac03-f1f68fd36e98' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Contract Personnel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@capablestaff.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('85b80895-9518-42a5-af5f-004e3b4ffe55', 'Cord Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Cord Recruitment' for email domain '@cord.com.au'
UPDATE Employer SET companyId = '85b80895-9518-42a5-af5f-004e3b4ffe55' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Cord Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@cord.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('978b4d9d-1108-4ea4-834f-de762b81c173', 'Corporate Communication Plans', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Corporate Communication Plans' for email domain '@optccp.com.au'
UPDATE Employer SET companyId = '978b4d9d-1108-4ea4-834f-de762b81c173' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Corporate Communication Plans' AND c.verifiedById IS NULL AND emailAddress LIKE '%@optccp.com.au'
GO

-- Move from unverified to verified company 'Corptech' for email domain '@corptech.qld.gov.au'
UPDATE Employer SET companyId = '2586c87b-f372-477c-a86d-27885f71265c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Corptech' AND c.verifiedById IS NULL AND emailAddress LIKE '%@corptech.qld.gov.au'
GO

-- Move from unverified company 'Corptech QLD' to verified company 'Corptech' for email domain '@corptech.qld.gov.au'
UPDATE Employer SET companyId = '2586c87b-f372-477c-a86d-27885f71265c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Corptech QLD' AND c.verifiedById IS NULL AND emailAddress LIKE '%@corptech.qld.gov.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e3ca8b35-9991-4cbf-94f5-f4a594c2b654', 'Cortex', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Cortex' for email domain '@cortexit.com.au'
UPDATE Employer SET companyId = 'e3ca8b35-9991-4cbf-94f5-f4a594c2b654' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Cortex' AND c.verifiedById IS NULL AND emailAddress LIKE '%@cortexit.com.au'
GO

-- Move from unverified company 'Cortex I.T. Labs' to verified company 'Cortex' for email domain '@backupassist.com'
UPDATE Employer SET companyId = 'e3ca8b35-9991-4cbf-94f5-f4a594c2b654' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Cortex I.T. Labs' AND c.verifiedById IS NULL AND emailAddress LIKE '%@backupassist.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a22c0307-a2c5-420a-a53c-4f2d26f0a6e4', 'Coulton Isaac Barber', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Coulton Isaac Barber' for email domain '@cibr.com.au'
UPDATE Employer SET companyId = 'a22c0307-a2c5-420a-a53c-4f2d26f0a6e4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Coulton Isaac Barber' AND c.verifiedById IS NULL AND emailAddress LIKE '%@cibr.com.au'
GO

-- Move from unverified to verified company 'Cox Purtell Staffing Services' for email domain '@coxpurtell.com.au'
UPDATE Employer SET companyId = '094fbf96-5fbc-4700-b79e-59f3d69b03dd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Cox Purtell Staffing Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@coxpurtell.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('82d469b5-e864-4d69-a129-afb6bd2d1cbb', 'Credit Corp', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Credit Corp' for email domain '@creditcorp.com.au'
UPDATE Employer SET companyId = '82d469b5-e864-4d69-a129-afb6bd2d1cbb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Credit Corp' AND c.verifiedById IS NULL AND emailAddress LIKE '%@creditcorp.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b2a9a4c7-21a4-4149-83a3-403756f3960b', 'Crowne Plaza Alice Springs', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Crowne Plaza Alice Springs' for email domain '@crowneplazaalicesprings.com.au'
UPDATE Employer SET companyId = 'b2a9a4c7-21a4-4149-83a3-403756f3960b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Crowne Plaza Alice Springs' AND c.verifiedById IS NULL AND emailAddress LIKE '%@crowneplazaalicesprings.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5a142a8f-739c-4466-b4ee-845d28b32171', 'Customware', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Customware' for email domain '@customware.net'
UPDATE Employer SET companyId = '5a142a8f-739c-4466-b4ee-845d28b32171' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Customware' AND c.verifiedById IS NULL AND emailAddress LIKE '%@customware.net'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f01f9940-b07c-4230-b360-0cf4f1420288', 'D & M Recruitment (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'D & M Recruitment (QLD)' for email domain '@dmrecruitment.com.au'
UPDATE Employer SET companyId = 'f01f9940-b07c-4230-b360-0cf4f1420288' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'D & M Recruitment (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dmrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('11f2db2f-ea86-47e0-9d6a-5000031114cd', 'Day Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Day Solutions' for email domain '@daysolutions.com.au'
UPDATE Employer SET companyId = '11f2db2f-ea86-47e0-9d6a-5000031114cd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Day Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@daysolutions.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3dc48bdd-7a01-4b7f-8e86-3e5cbf829567', 'Dealer Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Dealer Solutions' for email domain '@dealersolutions.com.au'
UPDATE Employer SET companyId = '3dc48bdd-7a01-4b7f-8e86-3e5cbf829567' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Dealer Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dealersolutions.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cddaa296-1550-4de7-8937-bfe85fd8f6bd', 'Debra Manson Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Debra Manson Recruitment' for email domain '@debramanson.com.au'
UPDATE Employer SET companyId = 'cddaa296-1550-4de7-8937-bfe85fd8f6bd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Debra Manson Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@debramanson.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5a3470b1-e1fe-414c-8fef-9c3a829e08e0', 'Devnet Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Devnet Pty Ltd' for email domain '@devnet.com.au'
UPDATE Employer SET companyId = '5a3470b1-e1fe-414c-8fef-9c3a829e08e0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Devnet Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@devnet.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('89fe42eb-1eb5-4228-8851-d440f318874c', 'DFP (Attain Recruitment Services)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DFP (Attain Recruitment Services)' for email domain '@attain.com.au'
UPDATE Employer SET companyId = '89fe42eb-1eb5-4228-8851-d440f318874c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DFP (Attain Recruitment Services)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@attain.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b2eb2dc5-4bdd-477c-bda3-252294410867', 'DFP (Network Contact Centre Specialists)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DFP (Network Contact Centre Specialists)' for email domain '@networkrecruitment.net'
UPDATE Employer SET companyId = 'b2eb2dc5-4bdd-477c-bda3-252294410867' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DFP (Network Contact Centre Specialists)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@networkrecruitment.net'
GO

-- Move from unverified to verified company 'DFP Recruitment' for email domain '@dfp.com.a'
UPDATE Employer SET companyId = 'b0df61f0-4c64-4905-8e7b-2af6234334c5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DFP Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dfp.com.a'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8a0ff00e-e382-4034-8f27-227b6bd8f363', 'Diageo', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Diageo' for email domain '@diageo.com'
UPDATE Employer SET companyId = '8a0ff00e-e382-4034-8f27-227b6bd8f363' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Diageo' AND c.verifiedById IS NULL AND emailAddress LIKE '%@diageo.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5126ed22-b6ec-464f-807c-6413eaf51d72', 'Diamond HR (Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Diamond HR (Sydney)' for email domain '@diamondhr.com.au'
UPDATE Employer SET companyId = '5126ed22-b6ec-464f-807c-6413eaf51d72' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Diamond HR (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@diamondhr.com.au'
GO

-- Move from unverified to verified company 'Diamond HR (Sydney)' for email domain '@diamonhr.com.au'
UPDATE Employer SET companyId = '5126ed22-b6ec-464f-807c-6413eaf51d72' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Diamond HR (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@diamonhr.com.au'
GO

-- Move from unverified to verified company 'Diamond HR (Sydney)' for email domain '@dismondhr.com.au'
UPDATE Employer SET companyId = '5126ed22-b6ec-464f-807c-6413eaf51d72' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Diamond HR (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dismondhr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6a8ae604-699e-4589-b3a2-ed9f950334a8', 'Direct Response Personnel', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Direct Response Personnel' for email domain '@responsepersonnel.com.au'
UPDATE Employer SET companyId = '6a8ae604-699e-4589-b3a2-ed9f950334a8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Direct Response Personnel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@responsepersonnel.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('41c58ab7-9ce9-466c-aa2a-e31488924fcd', 'DistinctConnect Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DistinctConnect Pty Ltd' for email domain '@distinctconnect.com.au'
UPDATE Employer SET companyId = '41c58ab7-9ce9-466c-aa2a-e31488924fcd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DistinctConnect Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@distinctconnect.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b7a451c1-4337-4c1c-a2c5-d8e5726bd2c3', 'Doctors Secretarial Agency', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Doctors Secretarial Agency' for email domain '@dsagency.com.au'
UPDATE Employer SET companyId = 'b7a451c1-4337-4c1c-a2c5-d8e5726bd2c3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Doctors Secretarial Agency' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dsagency.com.au'
GO

-- Move from unverified to verified company 'Downing Teal (WA)' for email domain '@dowingteal.com'
UPDATE Employer SET companyId = '9f704916-ddd8-4f94-8746-5efdf499edb9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Downing Teal (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dowingteal.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6d471e9b-529e-45d5-9a0c-92f4de1c84b2', 'DRP Consulting Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DRP Consulting Pty Ltd' for email domain '@polonsky.com.au'
UPDATE Employer SET companyId = '6d471e9b-529e-45d5-9a0c-92f4de1c84b2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DRP Consulting Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@polonsky.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a4d8df13-9603-4cc4-a33a-d98f056732fb', 'DTS International', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'DTS International' for email domain '@dtssydney.com'
UPDATE Employer SET companyId = 'a4d8df13-9603-4cc4-a33a-d98f056732fb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'DTS International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dtssydney.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e171cd74-d696-4059-9b4a-81626dc0cbd5', 'Dynamic HR', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Dynamic HR' for email domain '@dynamichr.net'
UPDATE Employer SET companyId = 'e171cd74-d696-4059-9b4a-81626dc0cbd5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Dynamic HR' AND c.verifiedById IS NULL AND emailAddress LIKE '%@dynamichr.net'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6de2ceaa-a989-4f80-866c-9ea0c5b13987', 'EBR', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'EBR' for email domain '@ebr.com.au'
UPDATE Employer SET companyId = '6de2ceaa-a989-4f80-866c-9ea0c5b13987' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'EBR' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ebr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a894a01a-f317-4b7c-9df1-4cb810f82089', 'Ecareer', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Ecareer' for email domain '@ecareer.com.au'
UPDATE Employer SET companyId = 'a894a01a-f317-4b7c-9df1-4cb810f82089' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Ecareer' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ecareer.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8fbb0d78-6bab-403e-ba31-883e29c8e4a8', 'Aspire Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'E-career' to verified company 'Aspire Recruitment' for email domain '@aspirerecruit.com.au'
UPDATE Employer SET companyId = '8fbb0d78-6bab-403e-ba31-883e29c8e4a8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'E-career' AND c.verifiedById IS NULL AND emailAddress LIKE '%@aspirerecruit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('74281c31-31b5-45cc-b6aa-1dfbc6bdacb7', 'E-career', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'E-career' for email domain '@ecareer.com.au'
UPDATE Employer SET companyId = '74281c31-31b5-45cc-b6aa-1dfbc6bdacb7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'E-career' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ecareer.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('95c1a20e-ffd6-4361-8606-87bf16223e2b', 'Eden Ritchie Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Eden Ritchie Recruitment' for email domain '@edenritchie.com.au'
UPDATE Employer SET companyId = '95c1a20e-ffd6-4361-8606-87bf16223e2b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Eden Ritchie Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@edenritchie.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a67fdd38-5a77-42ff-b6a6-267ca348ce28', 'Eligo IT Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Eligo IT Recruitment' for email domain '@eligo.com.au'
UPDATE Employer SET companyId = 'a67fdd38-5a77-42ff-b6a6-267ca348ce28' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Eligo IT Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@eligo.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('195b9576-d2eb-432c-be34-5a812151c1f0', 'Elle', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Elle' for email domain '@omen.net.au'
UPDATE Employer SET companyId = '195b9576-d2eb-432c-be34-5a812151c1f0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Elle' AND c.verifiedById IS NULL AND emailAddress LIKE '%@omen.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('22b9b49a-322d-49cc-9af0-25c77451f319', 'Entree Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Entree Recruitment' for email domain '@entree.com.au'
UPDATE Employer SET companyId = '22b9b49a-322d-49cc-9af0-25c77451f319' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Entree Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@entree.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('85b97591-b0d6-41a4-955a-896d507c03bd', 'ES Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ES Recruitment' for email domain '@esrecruitment.com.au'
UPDATE Employer SET companyId = '85b97591-b0d6-41a4-955a-896d507c03bd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ES Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@esrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4d0bdb31-31c7-4e80-a73a-36552df0243d', 'Esperille', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Esperille' for email domain '@esperille.com'
UPDATE Employer SET companyId = '4d0bdb31-31c7-4e80-a73a-36552df0243d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Esperille' AND c.verifiedById IS NULL AND emailAddress LIKE '%@esperille.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ff3a5fff-9d12-4f26-8d16-852cb118fe80', 'Esri Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Esri Australia' for email domain '@esriaustralia.com.au'
UPDATE Employer SET companyId = 'ff3a5fff-9d12-4f26-8d16-852cb118fe80' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Esri Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@esriaustralia.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('aef9eb0c-7e3b-4533-b323-d7cf96193d42', 'ETM', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ETM' for email domain '@etmgroup.com.au'
UPDATE Employer SET companyId = 'aef9eb0c-7e3b-4533-b323-d7cf96193d42' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ETM' AND c.verifiedById IS NULL AND emailAddress LIKE '%@etmgroup.com.au'
GO

-- Move from unverified to verified company 'Everjoy Consulting' for email domain '@everjoy.com.au'
UPDATE Employer SET companyId = '258b4ed4-3296-4b1f-a23e-63940a9381f8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Everjoy Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@everjoy.com.au'
GO

-- Move from unverified to verified company 'Everjoy Consulting' for email domain '@hotmail.com'
UPDATE Employer SET companyId = '258b4ed4-3296-4b1f-a23e-63940a9381f8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Everjoy Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

-- Move from unverified company 'Everjoy Consulting Pty Ltd' to verified company 'Everjoy Consulting' for email domain '@everjoy.com.au'
UPDATE Employer SET companyId = '258b4ed4-3296-4b1f-a23e-63940a9381f8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Everjoy Consulting Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@everjoy.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('94c228b7-46cf-42b0-b6c1-200d01f4fd38', 'Eylandt (Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Eylandt (Sydney)' for email domain '@eylandt.com.au'
UPDATE Employer SET companyId = '94c228b7-46cf-42b0-b6c1-200d01f4fd38' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Eylandt (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@eylandt.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f5207232-0c9b-4f2c-8772-490f72ecb973', 'Final5', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Final5' for email domain '@final5.com.au'
UPDATE Employer SET companyId = 'f5207232-0c9b-4f2c-8772-490f72ecb973' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Final5' AND c.verifiedById IS NULL AND emailAddress LIKE '%@final5.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('18c8af86-d89e-4daa-b21b-a18fe3e8aeb1', 'Financial Lifestyle Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Financial Lifestyle Solutions' for email domain '@flsolutions.com.au'
UPDATE Employer SET companyId = '18c8af86-d89e-4daa-b21b-a18fe3e8aeb1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Financial Lifestyle Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flsolutions.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9f1b91d6-37d1-4b57-9b61-049a55e8c07f', 'Fineos', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Fineos' for email domain '@fineos.com'
UPDATE Employer SET companyId = '9f1b91d6-37d1-4b57-9b61-049a55e8c07f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Fineos' AND c.verifiedById IS NULL AND emailAddress LIKE '%@fineos.com'
GO

-- Move from unverified to verified company 'Fingerprint Consulting' for email domain '@fprecruit.com.au'
UPDATE Employer SET companyId = '96277827-c1a5-459f-bef0-5af04b0fcde3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Fingerprint Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@fprecruit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a7649ec3-ecb6-477c-8d82-985be4be7445', 'Flaschengeist', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Flaschengeist' for email domain '@bigpond.com'
UPDATE Employer SET companyId = 'a7649ec3-ecb6-477c-8d82-985be4be7445' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flaschengeist' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bigpond.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8ad19696-a6c4-4644-bc98-b74c0b9157da', 'Flight Centre (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Flight Centre (NSW)' for email domain '@flightcentre.com'
UPDATE Employer SET companyId = '8ad19696-a6c4-4644-bc98-b74c0b9157da' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flight Centre (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flightcentre.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b6386a52-4518-4e49-9358-592403cc89fa', 'Flight Centre (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Flight Centre (QLD)' for email domain '@flightcentre.com'
UPDATE Employer SET companyId = 'b6386a52-4518-4e49-9358-592403cc89fa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flight Centre (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flightcentre.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f5d8e3fb-cebc-46b4-9d77-44a29c361bd5', 'Flight Centre (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Flight Centre (SA)' for email domain '@flightcentre'
UPDATE Employer SET companyId = 'f5d8e3fb-cebc-46b4-9d77-44a29c361bd5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flight Centre (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flightcentre'
GO

-- Move from unverified to verified company 'Flight Centre (SA)' for email domain '@flightcentre.com'
UPDATE Employer SET companyId = 'f5d8e3fb-cebc-46b4-9d77-44a29c361bd5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flight Centre (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flightcentre.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('21557943-e9eb-4b35-9066-9aadade4aa65', 'Flight Centre (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Flight Centre (VIC)' for email domain '@flightcentre.com'
UPDATE Employer SET companyId = '21557943-e9eb-4b35-9066-9aadade4aa65' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flight Centre (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flightcentre.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fe44a440-89a3-49f6-b73b-4e61cfb4eded', 'Flight Centre (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Flight Centre (WA)' for email domain '@flightcentre.com'
UPDATE Employer SET companyId = 'fe44a440-89a3-49f6-b73b-4e61cfb4eded' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Flight Centre (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@flightcentre.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c6c4a32e-a33f-40fd-939d-2792129d5cf2', 'Forever Healthy', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Forever Healthy' for email domain '@westnet.com.au'
UPDATE Employer SET companyId = 'c6c4a32e-a33f-40fd-939d-2792129d5cf2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Forever Healthy' AND c.verifiedById IS NULL AND emailAddress LIKE '%@westnet.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bd99a145-424a-4cac-b613-1905064cb60d', 'Foster''s Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Foster''s Group' for email domain '@fostersgroup.com'
UPDATE Employer SET companyId = 'bd99a145-424a-4cac-b613-1905064cb60d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Foster''s Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@fostersgroup.com'
GO

-- Move from unverified company 'Fox Symes' to verified company 'Fox Symes Recruitment' for email domain '@foxsymes.com.au'
UPDATE Employer SET companyId = '42d49b43-0678-4c9d-8704-209092c84285' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Fox Symes' AND c.verifiedById IS NULL AND emailAddress LIKE '%@foxsymes.com.au'
GO

-- Move from unverified to verified company 'Fox Symes Recruitment' for email domain '@foxsymes.com.au'
UPDATE Employer SET companyId = '42d49b43-0678-4c9d-8704-209092c84285' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Fox Symes Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@foxsymes.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('508f27b9-cce2-4cea-9a2a-660b56096c82', 'Front Row Video Distribution', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Front Row Video Distribution' for email domain '@hotmail.com'
UPDATE Employer SET companyId = '508f27b9-cce2-4cea-9a2a-660b56096c82' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Front Row Video Distribution' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('815aed66-df49-47e0-a45e-d58b8b95c5e9', 'Frontline Retail', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Frontline Retail' for email domain '@frontlineretail.com.au'
UPDATE Employer SET companyId = '815aed66-df49-47e0-a45e-d58b8b95c5e9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Frontline Retail' AND c.verifiedById IS NULL AND emailAddress LIKE '%@frontlineretail.com.au'
GO

-- Move from unverified to verified company 'Future Prospects' for email domain '@future-prospects.net'
UPDATE Employer SET companyId = '3fc6ae8a-a5ed-465a-8529-9caf5fb3abf8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Future Prospects' AND c.verifiedById IS NULL AND emailAddress LIKE '%@future-prospects.net'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9eb5fa2c-b4b7-4efc-98ae-c743f7b29578', 'Futurum', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Futurum' for email domain '@futurum.com.au'
UPDATE Employer SET companyId = '9eb5fa2c-b4b7-4efc-98ae-c743f7b29578' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Futurum' AND c.verifiedById IS NULL AND emailAddress LIKE '%@futurum.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6ca64063-1fc9-4a44-93c7-1969fc062fba', 'Gagudju Lodge Cooinda', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Gagudju Lodge Cooinda' for email domain '@gagudjulodgecooinda.com.au'
UPDATE Employer SET companyId = '6ca64063-1fc9-4a44-93c7-1969fc062fba' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Gagudju Lodge Cooinda' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gagudjulodgecooinda.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('45c5f028-66d1-47a0-8149-f2ac86a08f3c', 'GBS Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'GBS Recruitment' for email domain '@gbsrecruitment.com.au'
UPDATE Employer SET companyId = '45c5f028-66d1-47a0-8149-f2ac86a08f3c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GBS Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gbsrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c4ba752a-43c9-40af-a05b-898929c901fb', 'GE Money', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'GE Money - Autopeople' to verified company 'GE Money' for email domain '@ge.com'
UPDATE Employer SET companyId = 'c4ba752a-43c9-40af-a05b-898929c901fb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GE Money - Autopeople' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ge.com'
GO

-- Move from unverified to verified company 'Glotel' for email domain '@glotel.com'
UPDATE Employer SET companyId = '363cc5d1-c0aa-485a-a2d7-c9c000d13615' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Glotel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@glotel.com'
GO

-- Move from unverified company 'Glotel (NSW)' to verified company 'Glotel' for email domain '@glotel.com'
UPDATE Employer SET companyId = '363cc5d1-c0aa-485a-a2d7-c9c000d13615' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Glotel (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@glotel.com'
GO

-- Move from unverified company 'Glotel (VIC)' to verified company 'Glotel' for email domain '@glotel.com'
UPDATE Employer SET companyId = '363cc5d1-c0aa-485a-a2d7-c9c000d13615' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Glotel (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@glotel.com'
GO

-- Move from unverified company 'GMT (Melbourne)' to verified company 'GMT Recruitment' for email domain '@gmtpeople.com'
UPDATE Employer SET companyId = '7494d5db-73ba-4747-9f0b-68b0b565500b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GMT (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gmtpeople.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a31674cf-f084-4be3-83ef-f9695e1623c2', 'Goldram Financial Services', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Goldram Financial Services' for email domain '@goldram.com.au'
UPDATE Employer SET companyId = 'a31674cf-f084-4be3-83ef-f9695e1623c2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Goldram Financial Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@goldram.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('97ff3c70-1e3b-4e38-93aa-292246447c7a', 'Goodwin Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Goodwin Recruitment' for email domain '@goodwinrecruitment.com.au'
UPDATE Employer SET companyId = '97ff3c70-1e3b-4e38-93aa-292246447c7a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Goodwin Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@goodwinrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3ef17454-bf52-4154-8ad7-b502eb13820c', 'GRAINassist', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'GRAINassist' for email domain '@grainassist.com.au'
UPDATE Employer SET companyId = '3ef17454-bf52-4154-8ad7-b502eb13820c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GRAINassist' AND c.verifiedById IS NULL AND emailAddress LIKE '%@grainassist.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3adb24de-7b99-403b-af53-23d7c2414e4e', 'GroundProbe Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'GroundProbe Pty Ltd' for email domain '@groundprobe.com'
UPDATE Employer SET companyId = '3adb24de-7b99-403b-af53-23d7c2414e4e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GroundProbe Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@groundprobe.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a7c00427-f5be-49b8-90ea-90402c4c4854', 'GS1 Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'GS1 Australia' for email domain '@gs1au.org'
UPDATE Employer SET companyId = 'a7c00427-f5be-49b8-90ea-90402c4c4854' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'GS1 Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@gs1au.org'
GO

-- Move from unverified to verified company 'Hallis' for email domain '@hallis.com.ay'
UPDATE Employer SET companyId = '5cefbf66-f483-475c-b409-7a66e1e8a4e5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hallis' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hallis.com.ay'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c2994e14-95c8-48dd-8256-a8bfc4ffc01b', 'Hard Hat Creative', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Hard Hat Creative' for email domain '@hardhatcreative.com.au'
UPDATE Employer SET companyId = 'c2994e14-95c8-48dd-8256-a8bfc4ffc01b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hard Hat Creative' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hardhatcreative.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bd7c5c59-6718-460e-895b-b9a123e3d652', 'Health World', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Health World' for email domain '@healthworld.com.au'
UPDATE Employer SET companyId = 'bd7c5c59-6718-460e-895b-b9a123e3d652' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Health World' AND c.verifiedById IS NULL AND emailAddress LIKE '%@healthworld.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4884bec1-44d7-4dd1-af10-b200b7bd1e35', 'Hobsons', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Hobsons' for email domain '@hobsons.com.au'
UPDATE Employer SET companyId = '4884bec1-44d7-4dd1-af10-b200b7bd1e35' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Hobsons' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hobsons.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('73c1ab9b-d7ac-434d-b08d-7fe22fe846ab', 'Horizon Resourcing', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Horizon Resourcing' for email domain '@horizonresourcing.com.au'
UPDATE Employer SET companyId = '73c1ab9b-d7ac-434d-b08d-7fe22fe846ab' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Horizon Resourcing' AND c.verifiedById IS NULL AND emailAddress LIKE '%@horizonresourcing.com.au'
GO

-- Move from unverified to verified company 'HQ Consulting' for email domain '@hqconsulting.com.au'
UPDATE Employer SET companyId = 'e08291a4-0bd9-4274-8352-7cfd02c33f20' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HQ Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hqconsulting.com.au'
GO

-- Move from unverified to verified company 'HRG Australia' for email domain '@hrgworldwide.com'
UPDATE Employer SET companyId = '0bdd1b73-8b8b-433b-9ec6-07717b2407aa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'HRG Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hrgworldwide.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9b44734c-e9f3-4a43-8b7c-184878b78448', 'I2I Marketing Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'I2I Marketing Group' for email domain '@i2imarketing.com.au'
UPDATE Employer SET companyId = '9b44734c-e9f3-4a43-8b7c-184878b78448' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'I2I Marketing Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@i2imarketing.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9e890904-a2b4-46ef-b964-383b6273f2be', 'IG Marketing', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IG Marketing' for email domain '@unwired.com.au'
UPDATE Employer SET companyId = '9e890904-a2b4-46ef-b964-383b6273f2be' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IG Marketing' AND c.verifiedById IS NULL AND emailAddress LIKE '%@unwired.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4bfdf31f-166a-4d2d-b9d5-9c9cf2bd397f', 'Informatel', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Informatel' for email domain '@informatel.net'
UPDATE Employer SET companyId = '4bfdf31f-166a-4d2d-b9d5-9c9cf2bd397f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Informatel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@informatel.net'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ddfbdbfa-2dd8-467b-a4d4-0e97cac59f3f', 'Innovative Recruitment Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Innovative Recruitment Solutions' for email domain '@innovativerecruitmentsolutions.com.au'
UPDATE Employer SET companyId = 'ddfbdbfa-2dd8-467b-a4d4-0e97cac59f3f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Innovative Recruitment Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@innovativerecruitmentsolutions.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('35d3dd78-e92f-4aa1-b28c-b8dcd6008401', 'Insight Publishing', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Insight Publishing' for email domain '@insightpublishing.com.au'
UPDATE Employer SET companyId = '35d3dd78-e92f-4aa1-b28c-b8dcd6008401' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Insight Publishing' AND c.verifiedById IS NULL AND emailAddress LIKE '%@insightpublishing.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('29793df2-57e3-4c49-addc-3a9f2eee0e92', 'Insight Wellness', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Insight Wellness' for email domain '@insightwellness.com.au'
UPDATE Employer SET companyId = '29793df2-57e3-4c49-addc-3a9f2eee0e92' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Insight Wellness' AND c.verifiedById IS NULL AND emailAddress LIKE '%@insightwellness.com.au'
GO

-- Move from unverified to verified company 'Interpro' for email domain '@hotmail.com'
UPDATE Employer SET companyId = 'b18ff458-232b-4240-87f1-6d0a93f89cc1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Interpro' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1a2c0871-5024-4354-9a99-2e179a3a022b', 'Interspire Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Interspire Pty Ltd' for email domain '@interspire.com'
UPDATE Employer SET companyId = '1a2c0871-5024-4354-9a99-2e179a3a022b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Interspire Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@interspire.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('785bcf7d-d1f7-47ff-a0b8-152ae4b0c4da', 'IST Networks', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IST Networks' for email domain '@istnetworks.com'
UPDATE Employer SET companyId = '785bcf7d-d1f7-47ff-a0b8-152ae4b0c4da' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IST Networks' AND c.verifiedById IS NULL AND emailAddress LIKE '%@istnetworks.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f9eddd9d-bee8-419f-a4c2-cc7d1a53306b', 'IT Options Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'IT Options Pty Ltd' for email domain '@itoptions.com.au'
UPDATE Employer SET companyId = 'f9eddd9d-bee8-419f-a4c2-cc7d1a53306b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'IT Options Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@itoptions.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3658078f-b043-4e6e-a0d1-2417fe5574cf', 'ITABA 3', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'ITABA 3 - IT Consultants Group P/L (NS)' to verified company 'ITABA 3' for email domain '@itaba3.com.au'
UPDATE Employer SET companyId = '3658078f-b043-4e6e-a0d1-2417fe5574cf' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ITABA 3 - IT Consultants Group P/L (NS)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@itaba3.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ed98e5a6-e8a7-41ab-8c49-460974d04e7d', 'ITS Brisbane', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ITS Brisbane' for email domain '@itsbrisbane.com.au'
UPDATE Employer SET companyId = 'ed98e5a6-e8a7-41ab-8c49-460974d04e7d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ITS Brisbane' AND c.verifiedById IS NULL AND emailAddress LIKE '%@itsbrisbane.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('71c6e34e-a241-439d-89ca-c7d111dcb3ad', 'Jacobs Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Jacobs Australia' for email domain '@jacobs.com.au'
UPDATE Employer SET companyId = '71c6e34e-a241-439d-89ca-c7d111dcb3ad' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Jacobs Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@jacobs.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5d158610-1beb-4a9f-930b-0d1973bb1a99', 'Jam Prospects', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Jam Prospects' for email domain '@jamprospects.com.au'
UPDATE Employer SET companyId = '5d158610-1beb-4a9f-930b-0d1973bb1a99' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Jam Prospects' AND c.verifiedById IS NULL AND emailAddress LIKE '%@jamprospects.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('254d8621-eed7-46f8-957c-ae3ac36e57ce', 'Javrow Contracting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Javrow Contracting' for email domain '@javrow.com.au'
UPDATE Employer SET companyId = '254d8621-eed7-46f8-957c-ae3ac36e57ce' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Javrow Contracting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@javrow.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fcca11f2-3c3c-4a5e-b1d3-e2b87adfeb2c', 'Jeff Slabe & Associates', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Jeff Slabe & Associates' for email domain '@slabeassociates.com.au'
UPDATE Employer SET companyId = 'fcca11f2-3c3c-4a5e-b1d3-e2b87adfeb2c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Jeff Slabe & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@slabeassociates.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('baf32eff-ab99-4de4-ab02-e7c1439e5ad7', 'Jones Lang LaSalle', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Jones Lang LaSalle' for email domain '@ap.jll.com'
UPDATE Employer SET companyId = 'baf32eff-ab99-4de4-ab02-e7c1439e5ad7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Jones Lang LaSalle' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ap.jll.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cddecd61-e90b-4169-9a18-c6a5cd8e46fe', 'JP Morgan', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'JP Morgan' for email domain '@jpmorgan.com'
UPDATE Employer SET companyId = 'cddecd61-e90b-4169-9a18-c6a5cd8e46fe' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'JP Morgan' AND c.verifiedById IS NULL AND emailAddress LIKE '%@jpmorgan.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('404bb7b8-a9f8-4704-a56b-74e5193dd04b', 'Karndean International', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Karndean International' for email domain '@karndean.com.au'
UPDATE Employer SET companyId = '404bb7b8-a9f8-4704-a56b-74e5193dd04b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Karndean International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@karndean.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('df59b7b8-9a52-4634-8ae7-50ddd231c4f3', 'Kent Douglas and Assoc,', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Kent Douglas and Assoc,' for email domain '@kda.wow.aust.com'
UPDATE Employer SET companyId = 'df59b7b8-9a52-4634-8ae7-50ddd231c4f3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Kent Douglas and Assoc,' AND c.verifiedById IS NULL AND emailAddress LIKE '%@kda.wow.aust.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2bd0ff95-8f00-40c3-8dcb-579e6056e051', 'Key Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Key Recruitment' for email domain '@keyrecruitment.com.au'
UPDATE Employer SET companyId = '2bd0ff95-8f00-40c3-8dcb-579e6056e051' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Key Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@keyrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b1d7cef1-cee3-4a1a-bc78-b061a0e496c8', 'Kings Resources', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Kings Resources' for email domain '@kingsresources.com.au'
UPDATE Employer SET companyId = 'b1d7cef1-cee3-4a1a-bc78-b061a0e496c8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Kings Resources' AND c.verifiedById IS NULL AND emailAddress LIKE '%@kingsresources.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2b55f646-70ec-4e2d-9cb2-8809fc7f4f36', 'Komtel', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Komtel' for email domain '@komtel.com.au'
UPDATE Employer SET companyId = '2b55f646-70ec-4e2d-9cb2-8809fc7f4f36' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Komtel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@komtel.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7ac4551c-d887-43c2-9885-fb10c05cab11', 'Konekt', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Konekt' for email domain '@konekt.com.au'
UPDATE Employer SET companyId = '7ac4551c-d887-43c2-9885-fb10c05cab11' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Konekt' AND c.verifiedById IS NULL AND emailAddress LIKE '%@konekt.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('040b3ba4-14d7-4604-9006-db87b0e5f2eb', 'Korda Mentha', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Korda Mentha' for email domain '@kordamentha.com'
UPDATE Employer SET companyId = '040b3ba4-14d7-4604-9006-db87b0e5f2eb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Korda Mentha' AND c.verifiedById IS NULL AND emailAddress LIKE '%@kordamentha.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a7704286-78ac-40ea-887f-0cc4cadfc121', 'KR Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'KR Consulting' for email domain '@krconsulting.com.au'
UPDATE Employer SET companyId = 'a7704286-78ac-40ea-887f-0cc4cadfc121' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'KR Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@krconsulting.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('24559d59-c807-4987-b4a7-1b8f8bb14229', 'LAN System', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'LAN System' for email domain '@lansystems.com.au'
UPDATE Employer SET companyId = '24559d59-c807-4987-b4a7-1b8f8bb14229' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'LAN System' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lansystems.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f594e967-bdb2-4e3e-bac4-39cde49f58a7', 'Lanier Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Lanier Australia' for email domain '@lanier.com.au'
UPDATE Employer SET companyId = 'f594e967-bdb2-4e3e-bac4-39cde49f58a7' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Lanier Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lanier.com.au'
GO

-- Move from unverified to verified company 'Launch Recruitment' for email domain '@launchrecruitment.com.au'
UPDATE Employer SET companyId = '3a521f4c-77fc-417b-940e-e0cd669caa7c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Launch Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@launchrecruitment.com.au'
GO

-- Move from unverified company 'Launch Recruitment (NSW)' to verified company 'Launch Recruitment' for email domain '@launchrecruitment.com.au'
UPDATE Employer SET companyId = '3a521f4c-77fc-417b-940e-e0cd669caa7c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Launch Recruitment (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@launchrecruitment.com.au'
GO

-- Move from unverified to verified company 'Law Staff' for email domain '@law-staff.com.au'
UPDATE Employer SET companyId = '1cb0766a-0194-4cc2-80f6-0fa35d9437be' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Law Staff' AND c.verifiedById IS NULL AND emailAddress LIKE '%@law-staff.com.au'
GO

-- Move from unverified company 'Law Staff (VIC)' to verified company 'Law Staff' for email domain '@lawstaff.com.au'
UPDATE Employer SET companyId = '1cb0766a-0194-4cc2-80f6-0fa35d9437be' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Law Staff (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lawstaff.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f3326f21-3464-460c-84b6-d480522a8168', 'Lemonade', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Lemonade' for email domain '@lemonade.com.au'
UPDATE Employer SET companyId = 'f3326f21-3464-460c-84b6-d480522a8168' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Lemonade' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lemonade.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('edbbc2ce-eacf-45cd-9d08-ffb6f262aa55', 'Lewence', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Lewence' for email domain '@lewence.com.au'
UPDATE Employer SET companyId = 'edbbc2ce-eacf-45cd-9d08-ffb6f262aa55' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Lewence' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lewence.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cc0ae79b-7e04-4051-b4ac-9f9f7a9e0d38', 'Lightsounds', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Lightsounds' for email domain '@lswonline.com.au'
UPDATE Employer SET companyId = 'cc0ae79b-7e04-4051-b4ac-9f9f7a9e0d38' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Lightsounds' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lswonline.com.au'
GO

-- Move from unverified to verified company 'Link Recruitment' for email domain '@linkrecruitment.com.au'
UPDATE Employer SET companyId = '76f697da-bcbd-4ce9-a9d8-a4a347e4c701' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Link Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@linkrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4ac7ebb5-b9d1-4400-b700-75bc598755c0', 'Link Recruitment (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Link Recruitment (NSW)' for email domain '@linkrecruitment.com.au'
UPDATE Employer SET companyId = '4ac7ebb5-b9d1-4400-b700-75bc598755c0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Link Recruitment (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@linkrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2d810abd-4401-44ed-a55e-bebea8640aec', 'Link Recruitment (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Link Recruitment (QLD)' for email domain '@linkrecruitment.com.au'
UPDATE Employer SET companyId = '2d810abd-4401-44ed-a55e-bebea8640aec' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Link Recruitment (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@linkrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4c0bcd4d-09ac-4454-af7b-3e742c754809', 'Link Recruitment (SA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Link Recruitment (SA)' for email domain '@linkrecruitment.com.au'
UPDATE Employer SET companyId = '4c0bcd4d-09ac-4454-af7b-3e742c754809' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Link Recruitment (SA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@linkrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('554d29bd-bf39-4ed6-ae73-617a36a07fb8', 'Link Recruitment (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Link Recruitment (VIC)' for email domain '@linkrecruitment.com.au'
UPDATE Employer SET companyId = '554d29bd-bf39-4ed6-ae73-617a36a07fb8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Link Recruitment (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@linkrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('efd9c022-c97e-46a0-abc4-361ee5e0cfd1', 'Locker Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Locker Group' for email domain '@locker.com.au'
UPDATE Employer SET companyId = 'efd9c022-c97e-46a0-abc4-361ee5e0cfd1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Locker Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@locker.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('24522582-ffc7-4550-91e4-b62f6783c6d4', 'Locker Group (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Locker Group (NSW)' for email domain '@locker.com.au'
UPDATE Employer SET companyId = '24522582-ffc7-4550-91e4-b62f6783c6d4' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Locker Group (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@locker.com.au'
GO

-- Move from unverified company 'Locker Group Pty Ltd' to verified company 'Locker Group' for email domain '@locker.com.au'
UPDATE Employer SET companyId = 'efd9c022-c97e-46a0-abc4-361ee5e0cfd1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Locker Group Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@locker.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6695a8dd-93b4-4944-bc3e-df1a758208bc', 'Lois Recruitment Solution', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Lois Recruitment Solution' for email domain '@hotmail.com'
UPDATE Employer SET companyId = '6695a8dd-93b4-4944-bc3e-df1a758208bc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Lois Recruitment Solution' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6dda9aba-c8dd-4004-b504-d614869e579b', 'Look Print', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Look Print' for email domain '@look.com.au'
UPDATE Employer SET companyId = '6dda9aba-c8dd-4004-b504-d614869e579b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Look Print' AND c.verifiedById IS NULL AND emailAddress LIKE '%@look.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b679605d-60c7-49b9-961b-4554073046f1', 'LTX', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'LTX' for email domain '@ltx.com.au'
UPDATE Employer SET companyId = 'b679605d-60c7-49b9-961b-4554073046f1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'LTX' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ltx.com.au'
GO

-- Move from unverified to verified company 'Macquarie People' for email domain '@macquariepeople.com.au'
UPDATE Employer SET companyId = '91bc156d-f9d3-4e98-82ca-3913a1c0589c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Macquarie People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@macquariepeople.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a3c92d42-c7f9-4ad5-8030-e8732ae97cfc', 'Management Consultancy International', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Management Consultancy International' for email domain '@mcionline.com.au'
UPDATE Employer SET companyId = 'a3c92d42-c7f9-4ad5-8030-e8732ae97cfc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Management Consultancy International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mcionline.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8ba528b1-b1b3-4ff1-a24d-8a09e6025b9f', 'Management Effect', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Management Effect' for email domain '@managementeffect.com'
UPDATE Employer SET companyId = '8ba528b1-b1b3-4ff1-a24d-8a09e6025b9f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Management Effect' AND c.verifiedById IS NULL AND emailAddress LIKE '%@managementeffect.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('82fc6a49-c0fc-4a89-9140-46570a047f65', 'MAS Administration', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'MAS Administration' for email domain '@tfyt.com.au'
UPDATE Employer SET companyId = '82fc6a49-c0fc-4a89-9140-46570a047f65' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'MAS Administration' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tfyt.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2f57fe16-ab8f-4322-bc13-9a2524d2aefa', 'McBeath Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'McBeath Consulting' for email domain '@mcbeathconsulting.com'
UPDATE Employer SET companyId = '2f57fe16-ab8f-4322-bc13-9a2524d2aefa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'McBeath Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mcbeathconsulting.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('00c25f57-f63a-4409-bce2-e27673fcdf72', 'McKenzie Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'McKenzie Consulting' for email domain '@mckpeople.com.au'
UPDATE Employer SET companyId = '00c25f57-f63a-4409-bce2-e27673fcdf72' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'McKenzie Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mckpeople.com.au'
GO

-- Move from unverified company 'McKenzie Consulting Australia' to verified company 'McKenzie Consulting' for email domain '@mckpeople.com.au'
UPDATE Employer SET companyId = '00c25f57-f63a-4409-bce2-e27673fcdf72' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'McKenzie Consulting Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mckpeople.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('61c4b01d-b16e-4148-b367-33e6809cda65', 'MCS Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'MCS Consulting' for email domain '@mcs-consulting.com.au'
UPDATE Employer SET companyId = '61c4b01d-b16e-4148-b367-33e6809cda65' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'MCS Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mcs-consulting.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c8cbe1d9-2e9d-4e5b-9258-4466d7512449', 'MDDT Bookkeeping Services', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'MDDT Bookkeeping Services' for email domain '@hotmail.com'
UPDATE Employer SET companyId = 'c8cbe1d9-2e9d-4e5b-9258-4466d7512449' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'MDDT Bookkeeping Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ca483963-a42f-4545-8904-1d3363296205', 'MECWA', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'MECWA' for email domain '@mecwa.org.au'
UPDATE Employer SET companyId = 'ca483963-a42f-4545-8904-1d3363296205' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'MECWA' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mecwa.org.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2afaec60-b79d-4e20-b552-20291b313916', 'Message Media', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Message Media' for email domain '@message-media.com.au'
UPDATE Employer SET companyId = '2afaec60-b79d-4e20-b552-20291b313916' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Message Media' AND c.verifiedById IS NULL AND emailAddress LIKE '%@message-media.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0dbf781f-ac3b-41c1-a8b8-8e6bcff89228', 'Meta Management Solutions Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Meta Management Solutions Pty Ltd' for email domain '@metamanagement.net.au'
UPDATE Employer SET companyId = '0dbf781f-ac3b-41c1-a8b8-8e6bcff89228' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Meta Management Solutions Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@metamanagement.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('32de96dd-e843-417b-9a00-b70220c5cb99', 'Michelle L Watson', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Michelle L Watson' for email domain '@hotmail.com'
UPDATE Employer SET companyId = '32de96dd-e843-417b-9a00-b70220c5cb99' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Michelle L Watson' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('982ac7cb-e1ed-4bf8-a925-f17e4c20e40d', 'Millsom Hoists', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Millsom Hoists' for email domain '@millsomhoists.com.au'
UPDATE Employer SET companyId = '982ac7cb-e1ed-4bf8-a925-f17e4c20e40d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Millsom Hoists' AND c.verifiedById IS NULL AND emailAddress LIKE '%@millsomhoists.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6b2df148-bc1e-4155-9f4d-900400830609', 'Mitchells Quality Foods', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Mitchells Quality Foods' for email domain '@mitchellsqf.com.au'
UPDATE Employer SET companyId = '6b2df148-bc1e-4155-9f4d-900400830609' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mitchells Quality Foods' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mitchellsqf.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3c5e0cf1-d6ff-457c-8e4b-b9b08c298f2d', 'Mitronics', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Mitronics' for email domain '@mitronics.com.au'
UPDATE Employer SET companyId = '3c5e0cf1-d6ff-457c-8e4b-b9b08c298f2d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mitronics' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mitronics.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('acbf4528-aa91-4b3c-bca2-1b8063a3cd1b', 'Montare Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Montare Recruitment' for email domain '@montare.com.au'
UPDATE Employer SET companyId = 'acbf4528-aa91-4b3c-bca2-1b8063a3cd1b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Montare Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@montare.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('435f14ba-a417-4f48-9e71-46c8b324f140', 'Mosaic Recruitment (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Mosaic Recruitment (NSW)' for email domain '@mosaicrecruitment.com.au'
UPDATE Employer SET companyId = '435f14ba-a417-4f48-9e71-46c8b324f140' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mosaic Recruitment (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mosaicrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('fbe81b76-1472-461e-84db-f68bd4600923', 'Mosaic Recruitment (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Mosaic Recruitment (VIC)' for email domain '@mosaicrecruitment.com.au'
UPDATE Employer SET companyId = 'fbe81b76-1472-461e-84db-f68bd4600923' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Mosaic Recruitment (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@mosaicrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7958b627-45b0-45ae-9fc7-dcf1858b4b1a', 'MultiDirect', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'MultiDirect' for email domain '@multidirect.com.au'
UPDATE Employer SET companyId = '7958b627-45b0-45ae-9fc7-dcf1858b4b1a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'MultiDirect' AND c.verifiedById IS NULL AND emailAddress LIKE '%@multidirect.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d130031a-aa3c-4758-8768-cc08e2a588b3', 'National Safety Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'National Safety Group' for email domain '@nationalsafetygroup.com.au'
UPDATE Employer SET companyId = 'd130031a-aa3c-4758-8768-cc08e2a588b3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'National Safety Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@nationalsafetygroup.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c93aca65-5ae0-4a16-9f64-0753bed337ee', 'National Transport Insurance', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'National Transport Insurance' for email domain '@nti.com.au'
UPDATE Employer SET companyId = 'c93aca65-5ae0-4a16-9f64-0753bed337ee' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'National Transport Insurance' AND c.verifiedById IS NULL AND emailAddress LIKE '%@nti.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c01b50d3-3de0-49ed-ad35-d815db63b245', 'Neal Andrews & Associates', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Neal Andrews & Associates' for email domain '@hotmail.com'
UPDATE Employer SET companyId = 'c01b50d3-3de0-49ed-ad35-d815db63b245' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Neal Andrews & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@hotmail.com'
GO

-- Move from unverified to verified company 'Neal Andrews & Associates' for email domain '@nealandrews.com'
UPDATE Employer SET companyId = 'c01b50d3-3de0-49ed-ad35-d815db63b245' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Neal Andrews & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@nealandrews.com'
GO

-- Move from unverified to verified company 'Neal Andrews & Associates' for email domain '@recruit.co.nz'
UPDATE Employer SET companyId = 'c01b50d3-3de0-49ed-ad35-d815db63b245' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Neal Andrews & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@recruit.co.nz'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0c8583be-4a08-465f-910f-a80c80a98269', 'Neumann Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Neumann Group' for email domain '@neumann.com.au'
UPDATE Employer SET companyId = '0c8583be-4a08-465f-910f-a80c80a98269' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Neumann Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@neumann.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2c1fae87-8ca6-4bfc-a5cf-551512ac9fbd', 'Norman Disney & Young', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Norman Disney & Young' for email domain '@ndy.com'
UPDATE Employer SET companyId = '2c1fae87-8ca6-4bfc-a5cf-551512ac9fbd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Norman Disney & Young' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ndy.com'
GO

-- Move from unverified company 'Norman Disney & Young (NSW)' to verified company 'Norman Disney & Young' for email domain '@ndy.com'
UPDATE Employer SET companyId = '2c1fae87-8ca6-4bfc-a5cf-551512ac9fbd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Norman Disney & Young (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ndy.com'
GO

-- Move from unverified company 'Norman Disney & Young (QLD)' to verified company 'Norman Disney & Young' for email domain '@ndy.com'
UPDATE Employer SET companyId = '2c1fae87-8ca6-4bfc-a5cf-551512ac9fbd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Norman Disney & Young (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ndy.com'
GO

-- Move from unverified company 'Norman Disney & Young (VIC)' to verified company 'Norman Disney & Young' for email domain '@ndy.com'
UPDATE Employer SET companyId = '2c1fae87-8ca6-4bfc-a5cf-551512ac9fbd' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Norman Disney & Young (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@ndy.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('dbc98a2e-185e-4986-9839-11535e512c02', 'Nu-Stream', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Nu-Stream' for email domain '@nu-stream.com.au'
UPDATE Employer SET companyId = 'dbc98a2e-185e-4986-9839-11535e512c02' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Nu-Stream' AND c.verifiedById IS NULL AND emailAddress LIKE '%@nu-stream.com.au'
GO

-- Move from unverified to verified company 'Oakton' for email domain '@oakton.com'
UPDATE Employer SET companyId = 'f10262fa-53c3-4bae-b77c-32b6084028fe' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Oakton' AND c.verifiedById IS NULL AND emailAddress LIKE '%@oakton.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cee9641f-1315-4ccb-8d5e-be4d0a677d7e', 'Odyssey Resources Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Odyssey Resources Australia' for email domain '@odyssey-resources.com'
UPDATE Employer SET companyId = 'cee9641f-1315-4ccb-8d5e-be4d0a677d7e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Odyssey Resources Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@odyssey-resources.com'
GO

-- Move from unverified to verified company 'Online Recruitment' for email domain '@onlinerecruitment.com.au'
UPDATE Employer SET companyId = 'a6390343-a293-4939-ac63-f6b1b17f24f2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Online Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@onlinerecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d0f9c6f6-7c26-4e53-b434-059e51292d57', 'ORIX Australia Corporation Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ORIX Australia Corporation Ltd' for email domain '@orix.com.au'
UPDATE Employer SET companyId = 'd0f9c6f6-7c26-4e53-b434-059e51292d57' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ORIX Australia Corporation Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@orix.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cc720125-49c4-4022-b110-1976adb1e091', 'Paragon Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Paragon Recruitment' for email domain '@paragonrec.com.au'
UPDATE Employer SET companyId = 'cc720125-49c4-4022-b110-1976adb1e091' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Paragon Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@paragonrec.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2e167ee9-3159-4014-9ed7-383bd12cc16b', 'Paragorn Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'Paragon Recruitment Services' to verified company 'Paragorn Recruitment' for email domain '@paragonrec.com.au'
UPDATE Employer SET companyId = '2e167ee9-3159-4014-9ed7-383bd12cc16b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Paragon Recruitment Services' AND c.verifiedById IS NULL AND emailAddress LIKE '%@paragonrec.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f0819e1e-9d71-40b8-ab4d-5d119e34b49b', 'Paul Ingle & Associates', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Paul Ingle & Associates' for email domain '@paul-ingle.com.au'
UPDATE Employer SET companyId = 'f0819e1e-9d71-40b8-ab4d-5d119e34b49b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Paul Ingle & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@paul-ingle.com.au'
GO

-- Move from unverified company 'Paul Ingle & Associates Pty Ltd' to verified company 'Paul Ingle & Associates' for email domain '@paul-ingle.com.au'
UPDATE Employer SET companyId = 'f0819e1e-9d71-40b8-ab4d-5d119e34b49b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Paul Ingle & Associates Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@paul-ingle.com.au'
GO

-- Move from unverified company 'Paul Ingle and Associates' to verified company 'Paul Ingle & Associates' for email domain '@paul-ingle.com.au'
UPDATE Employer SET companyId = 'f0819e1e-9d71-40b8-ab4d-5d119e34b49b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Paul Ingle and Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@paul-ingle.com.au'
GO

-- Move from unverified to verified company 'Peak Placements' for email domain '@peakplacements.com.au'
UPDATE Employer SET companyId = 'e52ee9c4-7cf3-43e6-9f0c-18a1e12a67d9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Peak Placements' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peakplacements.com.au'
GO

-- Move from unverified to verified company 'Peak Placements' for email domain '@yourehired.net.au'
UPDATE Employer SET companyId = 'e52ee9c4-7cf3-43e6-9f0c-18a1e12a67d9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Peak Placements' AND c.verifiedById IS NULL AND emailAddress LIKE '%@yourehired.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('29067ee4-d39e-4d82-aad2-270c0e743eae', 'Pedders Suspension', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Pedders Suspension' for email domain '@pedders.com.au'
UPDATE Employer SET companyId = '29067ee4-d39e-4d82-aad2-270c0e743eae' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Pedders Suspension' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pedders.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('9fed06be-b9c7-4301-b47a-4507f83050ff', 'PeopleConnect', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'People Connect' to verified company 'PeopleConnect' for email domain '@peopleconnect.com.au'
UPDATE Employer SET companyId = '9fed06be-b9c7-4301-b47a-4507f83050ff' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People Connect' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peopleconnect.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2dc8b965-fc6d-4f6f-a281-d8df41f3fb4a', 'People R Us', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'People R Us' for email domain '@peoplerus.com.au'
UPDATE Employer SET companyId = '2dc8b965-fc6d-4f6f-a281-d8df41f3fb4a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'People R Us' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplerus.com.au'
GO

-- Move from unverified to verified company 'PeopleConnect' for email domain '@peopleconnect.com.au'
UPDATE Employer SET companyId = '9fed06be-b9c7-4301-b47a-4507f83050ff' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'PeopleConnect' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peopleconnect.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4f815e90-feb9-4297-ac35-35129164da31', 'PeopleHunt Recruitment Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'Peoplehunt' to verified company 'PeopleHunt Recruitment Group' for email domain '@peoplehunt.com.au'
UPDATE Employer SET companyId = '4f815e90-feb9-4297-ac35-35129164da31' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Peoplehunt' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplehunt.com.au'
GO

-- Move from unverified to verified company 'PeopleHunt Recruitment Group' for email domain '@peoplehunt.com.au'
UPDATE Employer SET companyId = '4f815e90-feb9-4297-ac35-35129164da31' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'PeopleHunt Recruitment Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@peoplehunt.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('33087a15-5f10-4e65-9095-8d7f4c8db7ef', 'Perilya (WA)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Perilya (WA)' for email domain '@bh.perilya.com.au'
UPDATE Employer SET companyId = '33087a15-5f10-4e65-9095-8d7f4c8db7ef' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Perilya (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@bh.perilya.com.au'
GO

-- Move from unverified to verified company 'Pernickety Recruitment' for email domain '@pernickety.com.au'
UPDATE Employer SET companyId = '7463db08-b2b7-49f7-8e68-de1df36ad19b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Pernickety Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pernickety.com.au'
GO

-- Move from unverified to verified company 'Personnel Concept' for email domain '@engineerjobs.com.au'
UPDATE Employer SET companyId = '8e77fbce-75a4-4530-8747-f500f01c460b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Personnel Concept' AND c.verifiedById IS NULL AND emailAddress LIKE '%@engineerjobs.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6e52820a-08b8-4f60-9a4f-a37e329164c9', 'Pivotal HR', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Pivotal HR' for email domain '@pivotalhr.com.au'
UPDATE Employer SET companyId = '6e52820a-08b8-4f60-9a4f-a37e329164c9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Pivotal HR' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pivotalhr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('aec23231-e981-44c2-9df1-969b8ab70e2e', 'PLAN Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'PLAN Australia' for email domain '@planaustralia.com.au'
UPDATE Employer SET companyId = 'aec23231-e981-44c2-9df1-969b8ab70e2e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'PLAN Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@planaustralia.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('5166cfd9-0696-4a4d-88d7-426ea8a3e495', 'Playfair Enterprises', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Playfair Enterprises' for email domain '@playfair.net.au'
UPDATE Employer SET companyId = '5166cfd9-0696-4a4d-88d7-426ea8a3e495' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Playfair Enterprises' AND c.verifiedById IS NULL AND emailAddress LIKE '%@playfair.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('526e438b-3633-43a4-bd69-57cbfcba8f5a', 'Polyweld', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Polyweld' for email domain '@polyweld.com.au'
UPDATE Employer SET companyId = '526e438b-3633-43a4-bd69-57cbfcba8f5a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Polyweld' AND c.verifiedById IS NULL AND emailAddress LIKE '%@polyweld.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('29096653-5504-4745-ac84-4d2cbb739fea', 'Preston Motors Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Preston Motors Group' for email domain '@prestonmotors.com.au'
UPDATE Employer SET companyId = '29096653-5504-4745-ac84-4d2cbb739fea' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Preston Motors Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@prestonmotors.com.au'
GO

-- Move from unverified to verified company 'Prime Appointments' for email domain '@jrsglobal.co'
UPDATE Employer SET companyId = 'dd0a78b0-6010-4cb9-9fe3-8a952aef1adb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Prime Appointments' AND c.verifiedById IS NULL AND emailAddress LIKE '%@jrsglobal.co'
GO

-- Move from unverified to verified company 'Professional Recruitment Australia' for email domain '@pra.com.au'
UPDATE Employer SET companyId = 'e879d97c-4ac9-48b2-913b-eeb102595305' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Recruitment Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pra.com.au'
GO

-- Move from unverified to verified company 'Professional Recruitment Australia' for email domain '@prorecruit.com.au'
UPDATE Employer SET companyId = 'e879d97c-4ac9-48b2-913b-eeb102595305' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Recruitment Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@prorecruit.com.au'
GO

-- Move from unverified company 'Professional Recruitment Australia (NSW)' to verified company 'Professional Recruitment Australia' for email domain '@pra.com.au'
UPDATE Employer SET companyId = 'e879d97c-4ac9-48b2-913b-eeb102595305' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Recruitment Australia (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pra.com.au'
GO

-- Move from unverified company 'Professional Recruitment Australia (Sydney)' to verified company 'Professional Recruitment Australia' for email domain '@pra.com.au'
UPDATE Employer SET companyId = 'e879d97c-4ac9-48b2-913b-eeb102595305' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Recruitment Australia (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pra.com.au'
GO

-- Move from unverified company 'Professional Recruitment Australia (VIC)' to verified company 'Professional Recruitment Australia' for email domain '@pra.com.au'
UPDATE Employer SET companyId = 'e879d97c-4ac9-48b2-913b-eeb102595305' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Recruitment Australia (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pra.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('df66cace-fb74-44ac-81a6-be6f0f237b99', 'Professional Recruitment Australia (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Professional Recruitment Australia (VIC)' for email domain '@prorecruit.com.au'
UPDATE Employer SET companyId = 'df66cace-fb74-44ac-81a6-be6f0f237b99' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Professional Recruitment Australia (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@prorecruit.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7396fbf6-0f9d-42af-ad9b-e8af041f770a', 'Profusion Group Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Profusion Group Pty Ltd' for email domain '@profusiongroup.com'
UPDATE Employer SET companyId = '7396fbf6-0f9d-42af-ad9b-e8af041f770a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Profusion Group Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@profusiongroup.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1d64a6a0-e896-4d37-bb89-0765a3808045', 'Project Coordination', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Project Coordination' for email domain '@projectcoordinationqld.com.au'
UPDATE Employer SET companyId = '1d64a6a0-e896-4d37-bb89-0765a3808045' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Project Coordination' AND c.verifiedById IS NULL AND emailAddress LIKE '%@projectcoordinationqld.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a6406004-0bb7-45d6-b7f0-962295f21489', 'Project People', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Project People' for email domain '@projectpeople.com'
UPDATE Employer SET companyId = 'a6406004-0bb7-45d6-b7f0-962295f21489' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Project People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@projectpeople.com'
GO

-- Move from unverified company 'Project People (QLD)' to verified company 'Project People' for email domain '@projectpeople.com'
UPDATE Employer SET companyId = 'a6406004-0bb7-45d6-b7f0-962295f21489' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Project People (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@projectpeople.com'
GO

-- Move from unverified company 'Project People (VIC)' to verified company 'Project People' for email domain '@projectpeople.com'
UPDATE Employer SET companyId = 'a6406004-0bb7-45d6-b7f0-962295f21489' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Project People (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@projectpeople.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('80b3bd9f-404f-415c-90ea-f0772d20050e', 'Pronet Technology', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Pronet Technology' for email domain '@pronet.com.au'
UPDATE Employer SET companyId = '80b3bd9f-404f-415c-90ea-f0772d20050e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Pronet Technology' AND c.verifiedById IS NULL AND emailAddress LIKE '%@pronet.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a1d752cf-9562-47b6-8267-11b2485eac05', 'Propell National Valuers', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Propell National Valuers' for email domain '@propellvaluers.com'
UPDATE Employer SET companyId = 'a1d752cf-9562-47b6-8267-11b2485eac05' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Propell National Valuers' AND c.verifiedById IS NULL AND emailAddress LIKE '%@propellvaluers.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('067472e5-b627-40e0-94fb-4c82df3a7a23', 'Prospect Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Prospect Consulting' for email domain '@prospectconsulting.com.au'
UPDATE Employer SET companyId = '067472e5-b627-40e0-94fb-4c82df3a7a23' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Prospect Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@prospectconsulting.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('08b981cc-eaab-453a-a3f6-26c06c0a6420', 'Qamvis', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Qamvis' for email domain '@qamvis.com'
UPDATE Employer SET companyId = '08b981cc-eaab-453a-a3f6-26c06c0a6420' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Qamvis' AND c.verifiedById IS NULL AND emailAddress LIKE '%@qamvis.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2b4884c8-ce42-4e27-b01c-1a435a1f1aba', 'QBE Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'QBE Recruitment' for email domain '@qbe.com'
UPDATE Employer SET companyId = '2b4884c8-ce42-4e27-b01c-1a435a1f1aba' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'QBE Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@qbe.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('540620a2-b51b-4c7b-84c0-39ff2b5fc147', 'Quadrant People Resources', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Quadrant People Resources' for email domain '@quadrantpeople.com.au'
UPDATE Employer SET companyId = '540620a2-b51b-4c7b-84c0-39ff2b5fc147' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quadrant People Resources' AND c.verifiedById IS NULL AND emailAddress LIKE '%@quadrantpeople.com.au'
GO

-- Move from unverified company 'Quadrant People Resources Pty Ltd' to verified company 'Quadrant People Resources' for email domain '@quadrantengineering.com.au'
UPDATE Employer SET companyId = '540620a2-b51b-4c7b-84c0-39ff2b5fc147' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quadrant People Resources Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@quadrantengineering.com.au'
GO

-- Move from unverified company 'Quadrant People Resources Pty Ltd' to verified company 'Quadrant People Resources' for email domain '@quadrantlogistics.com.au'
UPDATE Employer SET companyId = '540620a2-b51b-4c7b-84c0-39ff2b5fc147' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quadrant People Resources Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@quadrantlogistics.com.au'
GO

-- Move from unverified company 'Quadrant People Resources Pty Ltd' to verified company 'Quadrant People Resources' for email domain '@quadrantpeople.com.au'
UPDATE Employer SET companyId = '540620a2-b51b-4c7b-84c0-39ff2b5fc147' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quadrant People Resources Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@quadrantpeople.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ba499428-01be-42d6-982b-08209f0a989f', 'Quiksilver', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Quiksilver' for email domain '@qsilver.com.au'
UPDATE Employer SET companyId = 'ba499428-01be-42d6-982b-08209f0a989f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Quiksilver' AND c.verifiedById IS NULL AND emailAddress LIKE '%@qsilver.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('43d2e9c5-56b8-4ef3-9c3c-3f0736d29bc8', 'Radiant Body Bar', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Radiant Body Bar' for email domain '@radiantbodybar.com.au'
UPDATE Employer SET companyId = '43d2e9c5-56b8-4ef3-9c3c-3f0736d29bc8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Radiant Body Bar' AND c.verifiedById IS NULL AND emailAddress LIKE '%@radiantbodybar.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8d8c0f71-a553-4df3-a3de-81c02ee3796b', 'Railways Credit Union', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Railways Credit Union' for email domain '@railwayscreditunion.com.au'
UPDATE Employer SET companyId = '8d8c0f71-a553-4df3-a3de-81c02ee3796b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Railways Credit Union' AND c.verifiedById IS NULL AND emailAddress LIKE '%@railwayscreditunion.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('105dde1a-dae6-4906-80f7-3fb8ff591eab', 'ReaGroup', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ReaGroup' for email domain '@realestate.com.au'
UPDATE Employer SET companyId = '105dde1a-dae6-4906-80f7-3fb8ff591eab' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ReaGroup' AND c.verifiedById IS NULL AND emailAddress LIKE '%@realestate.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2cbacc61-faaa-49ab-9ff0-c5ecf3a59bcb', 'Real Enterprising People', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Real Enterprising People' for email domain '@repr.com.au'
UPDATE Employer SET companyId = '2cbacc61-faaa-49ab-9ff0-c5ecf3a59bcb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Real Enterprising People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@repr.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ea4917a1-d693-498b-bab4-f50d45cf57e6', 'Red Giraffe', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Red Giraffe' for email domain '@redgiraffesearch.com.au'
UPDATE Employer SET companyId = 'ea4917a1-d693-498b-bab4-f50d45cf57e6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Red Giraffe' AND c.verifiedById IS NULL AND emailAddress LIKE '%@redgiraffesearch.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e76a1449-696c-454c-b6b1-6e3cb84c50a3', 'Reed Construction Data (NSW)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Reed Construction Data (NSW)' for email domain '@rcd.com.au'
UPDATE Employer SET companyId = 'e76a1449-696c-454c-b6b1-6e3cb84c50a3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Reed Construction Data (NSW)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rcd.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a711a95d-0940-4f47-a0fa-97c313a71f7e', 'Reed Construction Data (QLD)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Reed Construction Data (QLD)' for email domain '@rcd.com.au'
UPDATE Employer SET companyId = 'a711a95d-0940-4f47-a0fa-97c313a71f7e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Reed Construction Data (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rcd.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('643374e5-d685-43ec-94e5-4c66b483636c', 'Reed Construction Data (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Reed Construction Data (VIC)' for email domain '@rcd.com.au'
UPDATE Employer SET companyId = '643374e5-d685-43ec-94e5-4c66b483636c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Reed Construction Data (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rcd.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1970ca65-312a-4541-b1ae-d93645fd9604', 'Reliance Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Reliance Recruitment' for email domain '@reliancerecruitment.com'
UPDATE Employer SET companyId = '1970ca65-312a-4541-b1ae-d93645fd9604' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Reliance Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@reliancerecruitment.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('db215a0d-df62-410c-90e4-54ffe77cf1df', 'Remax', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Remax' for email domain '@remax.com.au'
UPDATE Employer SET companyId = 'db215a0d-df62-410c-90e4-54ffe77cf1df' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Remax' AND c.verifiedById IS NULL AND emailAddress LIKE '%@remax.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('425c8fa0-9466-4ca5-a0c2-48612190a076', 'Resource Network International Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Resource Network International Pty Ltd' for email domain '@rni.com.au'
UPDATE Employer SET companyId = '425c8fa0-9466-4ca5-a0c2-48612190a076' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Resource Network International Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@rni.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e3916e74-ea68-4cf0-b585-d93dc885adaa', 'Riverina Stock Feeds', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Riverina Stock Feeds' for email domain '@riverina.com.au'
UPDATE Employer SET companyId = 'e3916e74-ea68-4cf0-b585-d93dc885adaa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Riverina Stock Feeds' AND c.verifiedById IS NULL AND emailAddress LIKE '%@riverina.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c425b813-f99b-440f-a644-94197aad7e0a', 'Sahai Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Sahai Pty Ltd' for email domain '@sahai.com.au'
UPDATE Employer SET companyId = 'c425b813-f99b-440f-a644-94197aad7e0a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sahai Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sahai.com.au'
GO

-- Move from unverified to verified company 'Salmat' for email domain '@salesforce.com.au'
UPDATE Employer SET companyId = '62c36205-f6e8-4eb8-b7fa-d143d8017c5d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Salmat' AND c.verifiedById IS NULL AND emailAddress LIKE '%@salesforce.com.au'
GO

-- Move from unverified to verified company 'Salmat' for email domain '@salmat.com.au'
UPDATE Employer SET companyId = '62c36205-f6e8-4eb8-b7fa-d143d8017c5d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Salmat' AND c.verifiedById IS NULL AND emailAddress LIKE '%@salmat.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('1f0daf3b-b2f8-4897-b242-e5fc0de41fda', 'Sarina Russo', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Sarina Russo' for email domain '@russorecruitment.com.au'
UPDATE Employer SET companyId = '1f0daf3b-b2f8-4897-b242-e5fc0de41fda' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sarina Russo' AND c.verifiedById IS NULL AND emailAddress LIKE '%@russorecruitment.com.au'
GO

-- Move from unverified to verified company 'Sarina Russo' for email domain '@tsrg.com.au'
UPDATE Employer SET companyId = '1f0daf3b-b2f8-4897-b242-e5fc0de41fda' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sarina Russo' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tsrg.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('cc6084b8-90ea-4819-9a3f-eb800c177502', 'Save Water', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Save Water' for email domain '@savewater.com.au'
UPDATE Employer SET companyId = 'cc6084b8-90ea-4819-9a3f-eb800c177502' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Save Water' AND c.verifiedById IS NULL AND emailAddress LIKE '%@savewater.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c0c165e1-e751-4caa-a6ef-40c3d340d800', 'seanemployer', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'seanemployer' for email domain '@email.com'
UPDATE Employer SET companyId = 'c0c165e1-e751-4caa-a6ef-40c3d340d800' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'seanemployer' AND c.verifiedById IS NULL AND emailAddress LIKE '%@email.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0ac4e9e6-ecb4-4273-aff7-adf527a86ab3', 'seanemployer4', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'seanemployer4' for email domain '@email.com'
UPDATE Employer SET companyId = '0ac4e9e6-ecb4-4273-aff7-adf527a86ab3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'seanemployer4' AND c.verifiedById IS NULL AND emailAddress LIKE '%@email.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ce9d9821-b5b5-4664-8c79-79bbcdace4f1', 'SEARCH IQ', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'SEARCH IQ' for email domain '@searchiq.com.au'
UPDATE Employer SET companyId = 'ce9d9821-b5b5-4664-8c79-79bbcdace4f1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'SEARCH IQ' AND c.verifiedById IS NULL AND emailAddress LIKE '%@searchiq.com.au'
GO

-- Move from unverified to verified company 'Searchforce Media' for email domain '@searchforcemedia.com.au'
UPDATE Employer SET companyId = '342aacb2-7e83-400c-a758-3b73af106a6f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Searchforce Media' AND c.verifiedById IS NULL AND emailAddress LIKE '%@searchforcemedia.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e5cd7ff8-7c3c-45f2-a400-d8a2e4a0d0b5', 'Secure Parking', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Secure Parking' for email domain '@secureparking.com.au'
UPDATE Employer SET companyId = 'e5cd7ff8-7c3c-45f2-a400-d8a2e4a0d0b5' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Secure Parking' AND c.verifiedById IS NULL AND emailAddress LIKE '%@secureparking.com.au'
GO

-- Move from unverified to verified company 'Select Accountancy' for email domain '@sanfordtravel.com.au'
UPDATE Employer SET companyId = '539747e8-e3cd-4930-b75d-c91af860d528' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Accountancy' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sanfordtravel.com.au'
GO

-- Move from unverified to verified company 'Select Appointments (VIC)' for email domain '@select.com.au'
UPDATE Employer SET companyId = '4e424608-64bd-45f3-b85a-0963bcb18cf2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('20c18b59-0f6a-4bce-a7a3-e80288071faa', 'Select Appointments Notting Hill', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Appointments Notting Hill' for email domain '@vedior.com.au'
UPDATE Employer SET companyId = '20c18b59-0f6a-4bce-a7a3-e80288071faa' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Appointments Notting Hill' AND c.verifiedById IS NULL AND emailAddress LIKE '%@vedior.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('3a7d9d3e-bf67-4deb-8e8d-b1b7a2f2a615', 'Select Evalu8 (Melbourne)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Evalu8 (Melbourne)' for email domain '@select.com.au'
UPDATE Employer SET companyId = '3a7d9d3e-bf67-4deb-8e8d-b1b7a2f2a615' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Evalu8 (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select.com.au'
GO

-- Move from unverified to verified company 'Select Evalu8 (Melbourne)' for email domain '@select-teleresources.com.au'
UPDATE Employer SET companyId = '3a7d9d3e-bf67-4deb-8e8d-b1b7a2f2a615' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Evalu8 (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select-teleresources.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('e21b3bfc-1bf2-459d-9b63-870e5cb63a02', 'Select Teleresources (Brisbane)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Teleresources (Brisbane)' for email domain '@select-teleresources.com.au'
UPDATE Employer SET companyId = 'e21b3bfc-1bf2-459d-9b63-870e5cb63a02' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Teleresources (Brisbane)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select-teleresources.com.au'
GO

-- Move from unverified to verified company 'Select Teleresources (Melbourne)' for email domain '@select-teleresources.com.au'
UPDATE Employer SET companyId = 'dcba4c11-5f83-4b52-a9ea-cb1ee69c7e89' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Teleresources (Melbourne)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select-teleresources.com.au'
GO

-- Move from unverified to verified company 'Select Teleresources (Parramatta)' for email domain '@selectevalu8.com.au'
UPDATE Employer SET companyId = '640b7239-93c1-42dd-b881-e694e107aa16' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Teleresources (Parramatta)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@selectevalu8.com.au'
GO

-- Move from unverified to verified company 'Select Teleresources (Parramatta)' for email domain '@select-teleresources.com.au'
UPDATE Employer SET companyId = '640b7239-93c1-42dd-b881-e694e107aa16' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Teleresources (Parramatta)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select-teleresources.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('18d1c878-3d15-4937-9f39-56f591b1fa1a', 'Select Teleresources (Sydney)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Teleresources (Sydney)' for email domain '@select-teleresources.com.au'
UPDATE Employer SET companyId = '18d1c878-3d15-4937-9f39-56f591b1fa1a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Teleresources (Sydney)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select-teleresources.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('79488340-a3a5-4c4f-8389-675e45db4c4b', 'Select Teleresources (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Select Teleresources (VIC)' for email domain '@select-teleresources.com.au'
UPDATE Employer SET companyId = '79488340-a3a5-4c4f-8389-675e45db4c4b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Select Teleresources (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@select-teleresources.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f05979cf-3779-4590-a867-751ec045b3bb', 'Serviceworks Management', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Serviceworks Management' for email domain '@serviceworks.com.au'
UPDATE Employer SET companyId = 'f05979cf-3779-4590-a867-751ec045b3bb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Serviceworks Management' AND c.verifiedById IS NULL AND emailAddress LIKE '%@serviceworks.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('955ccedd-6196-46ce-ad02-2492f65db5b9', 'Shortlist Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Shortlist Consulting' for email domain '@shortlistconsulting.com.au'
UPDATE Employer SET companyId = '955ccedd-6196-46ce-ad02-2492f65db5b9' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Shortlist Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@shortlistconsulting.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8d52a039-d52f-4a8a-a7ad-09b94857f0c2', 'Signature Security', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Signature Security' for email domain '@signaturesecurity.com.au'
UPDATE Employer SET companyId = '8d52a039-d52f-4a8a-a7ad-09b94857f0c2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Signature Security' AND c.verifiedById IS NULL AND emailAddress LIKE '%@signaturesecurity.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7780426d-7633-4a73-bd10-054a849147ca', 'Signature Security Group', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Signature Security Group' for email domain '@signaturesecurity.com.au'
UPDATE Employer SET companyId = '7780426d-7633-4a73-bd10-054a849147ca' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Signature Security Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@signaturesecurity.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4561adcb-8658-4ff8-8712-3753f3e363ba', 'Silent Partner Consulting', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Silent Partner Consulting' for email domain '@spconsulting.com.au'
UPDATE Employer SET companyId = '4561adcb-8658-4ff8-8712-3753f3e363ba' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Silent Partner Consulting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@spconsulting.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('31bfd9ff-a2f7-431a-9f2a-7cd3c439719c', 'Smalls Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'Smalls Recruiting' to verified company 'Smalls Recruitment' for email domain '@smalls.com.au'
UPDATE Employer SET companyId = '31bfd9ff-a2f7-431a-9f2a-7cd3c439719c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Smalls Recruiting' AND c.verifiedById IS NULL AND emailAddress LIKE '%@smalls.com.au'
GO

-- Move from unverified to verified company 'Smalls Recruitment' for email domain '@smalls.com.au'
UPDATE Employer SET companyId = '31bfd9ff-a2f7-431a-9f2a-7cd3c439719c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Smalls Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@smalls.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a81141be-278a-4b7f-aa15-a599f8649dd3', 'Thales Australia', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified company 'Staff & Exec / HR Partners' to verified company 'Thales Australia' for email domain '@staff-exec.com.au'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Staff & Exec / HR Partners' AND c.verifiedById IS NULL AND emailAddress LIKE '%@staff-exec.com.au'
GO

-- Move from unverified company 'Staff & Exec / HR Partners' to verified company 'Thales Australia' for email domain '@thalesgroup.com.au'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Staff & Exec / HR Partners' AND c.verifiedById IS NULL AND emailAddress LIKE '%@thalesgroup.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('ab903fc0-fa41-4d01-b3d9-11bc2083959c', 'Starshots', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Starshots' for email domain '@tpg.com.au'
UPDATE Employer SET companyId = 'ab903fc0-fa41-4d01-b3d9-11bc2083959c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Starshots' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tpg.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('32410864-ae80-4851-bec7-88aa7b819b53', 'State Trustees', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'State Trustees' for email domain '@statetrustees.com.au'
UPDATE Employer SET companyId = '32410864-ae80-4851-bec7-88aa7b819b53' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'State Trustees' AND c.verifiedById IS NULL AND emailAddress LIKE '%@statetrustees.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('477074e1-9624-4267-9f80-8bf7d96c029b', 'Sylvania BMW', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Sylvania BMW' for email domain '@sylvania.com.au'
UPDATE Employer SET companyId = '477074e1-9624-4267-9f80-8bf7d96c029b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Sylvania BMW' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sylvania.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('bfa223c3-3f67-43c4-b563-53f62fc9db29', 'Synergy Globe', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Synergy Globe' for email domain '@sparta.com.au'
UPDATE Employer SET companyId = 'bfa223c3-3f67-43c4-b563-53f62fc9db29' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Synergy Globe' AND c.verifiedById IS NULL AND emailAddress LIKE '%@sparta.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('b1ea0f03-f72c-4fdc-9835-02bdcf2dc81a', 'Talent Technologies', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Talent Technologies' for email domain '@talenttech.com.au'
UPDATE Employer SET companyId = 'b1ea0f03-f72c-4fdc-9835-02bdcf2dc81a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Talent Technologies' AND c.verifiedById IS NULL AND emailAddress LIKE '%@talenttech.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('32bb239b-2b29-4490-a0c7-f09589c983a2', 'Taycour & Associates', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Taycour & Associates' for email domain '@chariot.net.au'
UPDATE Employer SET companyId = '32bb239b-2b29-4490-a0c7-f09589c983a2' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Taycour & Associates' AND c.verifiedById IS NULL AND emailAddress LIKE '%@chariot.net.au'
GO

-- Move from unverified to verified company 'Taylor Coulter' for email domain '@taylorcoulter.com.au'
UPDATE Employer SET companyId = '49f62b82-ff35-475b-9753-9f9dd4f3294f' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Taylor Coulter' AND c.verifiedById IS NULL AND emailAddress LIKE '%@taylorcoulter.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('44aaf9ee-b449-4b92-b558-fcd004b1d52b', 'Temporarily Yours', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Temporarily Yours' for email domain '@tempyours.com.au'
UPDATE Employer SET companyId = '44aaf9ee-b449-4b92-b558-fcd004b1d52b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Temporarily Yours' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tempyours.com.au'
GO

-- Move from unverified to verified company 'Temporarily Yours' for email domain '@tempyoursnsw.com.au'
UPDATE Employer SET companyId = '44aaf9ee-b449-4b92-b558-fcd004b1d52b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Temporarily Yours' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tempyoursnsw.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('92a00729-32e0-4154-af37-bb8cb72a189b', 'Temporarily/Permanently Your''s (VIC)', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Temporarily/Permanently Your''s (VIC)' for email domain '@tempyours.com.au'
UPDATE Employer SET companyId = '92a00729-32e0-4154-af37-bb8cb72a189b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Temporarily/Permanently Your''s (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tempyours.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f6572108-ef59-4249-8c84-0f30d341cc63', 'Tenix Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Tenix Solutions' for email domain '@tenix.com'
UPDATE Employer SET companyId = 'f6572108-ef59-4249-8c84-0f30d341cc63' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Tenix Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tenix.com'
GO

-- Move from unverified company 'Thales' to verified company 'Thales Australia' for email domain '@au.thalesgroup.com'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Thales' AND c.verifiedById IS NULL AND emailAddress LIKE '%@au.thalesgroup.com'
GO

-- Move from unverified company 'Thales' to verified company 'Thales Australia' for email domain '@chandlermacleod.com'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Thales' AND c.verifiedById IS NULL AND emailAddress LIKE '%@chandlermacleod.com'
GO

-- Move from unverified company 'Thales' to verified company 'Thales Australia' for email domain '@thalesgroup.com.au'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Thales' AND c.verifiedById IS NULL AND emailAddress LIKE '%@thalesgroup.com.au'
GO

-- Move from unverified to verified company 'Thales Australia' for email domain '@staff-exec.com.au'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Thales Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@staff-exec.com.au'
GO

-- Move from unverified to verified company 'Thales Australia' for email domain '@thalesgroup.com.au'
UPDATE Employer SET companyId = 'a81141be-278a-4b7f-aa15-a599f8649dd3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Thales Australia' AND c.verifiedById IS NULL AND emailAddress LIKE '%@thalesgroup.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('595bff8e-617c-4449-b005-f75de503592c', 'The ADWEB Agency / Intranet DASHBOARD', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'The ADWEB Agency / Intranet DASHBOARD' for email domain '@adweb.com.au'
UPDATE Employer SET companyId = '595bff8e-617c-4449-b005-f75de503592c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The ADWEB Agency / Intranet DASHBOARD' AND c.verifiedById IS NULL AND emailAddress LIKE '%@adweb.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('043e65ea-2d42-4dc5-9e15-a3c8629d4302', 'The Aged Care Standards & Accreditation Agency', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'The Aged Care Standards & Accreditation Agency' for email domain '@accreditation.org.au'
UPDATE Employer SET companyId = '043e65ea-2d42-4dc5-9e15-a3c8629d4302' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Aged Care Standards & Accreditation Agency' AND c.verifiedById IS NULL AND emailAddress LIKE '%@accreditation.org.au'
GO

-- Move from unverified to verified company 'The Bailey Group' for email domain '@photongroup.com'
UPDATE Employer SET companyId = '78472ccd-8021-400c-bc57-b1455ef78625' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Bailey Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@photongroup.com'
GO

-- Move from unverified to verified company 'The Bailey Group' for email domain '@thebaileygroup.com.au'
UPDATE Employer SET companyId = '78472ccd-8021-400c-bc57-b1455ef78625' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Bailey Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@thebaileygroup.com.au'
GO

-- Move from unverified to verified company 'The Heat Group' for email domain '@heatgroup.com.au'
UPDATE Employer SET companyId = '607535ce-dcf7-4250-b609-1c6e1c6f40dc' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Heat Group' AND c.verifiedById IS NULL AND emailAddress LIKE '%@heatgroup.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6ea8e585-c0cc-426c-b54e-c6165804b1e3', 'The Lead Generation', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'The Lead Generation' for email domain '@lgco.com.au'
UPDATE Employer SET companyId = '6ea8e585-c0cc-426c-b54e-c6165804b1e3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Lead Generation' AND c.verifiedById IS NULL AND emailAddress LIKE '%@lgco.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c5826e17-aa1e-43f4-9a97-555444ecc968', 'The Marketing Department', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'The Marketing Department' for email domain '@tmd.com.au'
UPDATE Employer SET companyId = 'c5826e17-aa1e-43f4-9a97-555444ecc968' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Marketing Department' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tmd.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('c305e944-6bc9-4d00-8815-8135a52bcba6', 'The Red Pill', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'The Red Pill' for email domain '@theredpill.com.au'
UPDATE Employer SET companyId = 'c305e944-6bc9-4d00-8815-8135a52bcba6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The Red Pill' AND c.verifiedById IS NULL AND emailAddress LIKE '%@theredpill.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('323daf35-afaa-43e1-bd9e-cc423a19fb16', 'The System Works', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'The System Works' for email domain '@thesystemworks.com.au'
UPDATE Employer SET companyId = '323daf35-afaa-43e1-bd9e-cc423a19fb16' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'The System Works' AND c.verifiedById IS NULL AND emailAddress LIKE '%@thesystemworks.com.au'
GO

-- Move from unverified to verified company 'TMS Asia Pacific' for email domain '@tmsap.com'
UPDATE Employer SET companyId = '1240ac55-e1c2-45e6-aa8f-bfb2a7ac95b3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'TMS Asia Pacific' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tmsap.com'
GO

-- Move from unverified company 'TMS Asia Pacific (VIC)' to verified company 'TMS Asia Pacific' for email domain '@tmsap.com'
UPDATE Employer SET companyId = '1240ac55-e1c2-45e6-aa8f-bfb2a7ac95b3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'TMS Asia Pacific (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tmsap.com'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('33fdbb6e-1522-499c-bcce-3ff1e7998063', 'Toll Personnel', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Toll Personnel' for email domain '@toll.com.au'
UPDATE Employer SET companyId = '33fdbb6e-1522-499c-bcce-3ff1e7998063' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Toll Personnel' AND c.verifiedById IS NULL AND emailAddress LIKE '%@toll.com.au'
GO

-- Move from unverified company 'Toll Personnel (QLD)' to verified company 'Toll Personnel' for email domain '@toll.com.au'
UPDATE Employer SET companyId = '33fdbb6e-1522-499c-bcce-3ff1e7998063' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Toll Personnel (QLD)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@toll.com.au'
GO

-- Move from unverified company 'Toll Personnel (VIC)' to verified company 'Toll Personnel' for email domain '@toll.com.au'
UPDATE Employer SET companyId = '33fdbb6e-1522-499c-bcce-3ff1e7998063' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Toll Personnel (VIC)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@toll.com.au'
GO

-- Move from unverified company 'Toll Personnel (WA)' to verified company 'Toll Personnel' for email domain '@toll.com.au'
UPDATE Employer SET companyId = '33fdbb6e-1522-499c-bcce-3ff1e7998063' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Toll Personnel (WA)' AND c.verifiedById IS NULL AND emailAddress LIKE '%@toll.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4ea65c63-8652-4fdf-9da1-3cd12dc0ec36', 'Tonnex', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Tonnex' for email domain '@tonnex.com.au'
UPDATE Employer SET companyId = '4ea65c63-8652-4fdf-9da1-3cd12dc0ec36' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Tonnex' AND c.verifiedById IS NULL AND emailAddress LIKE '%@tonnex.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('650127c0-32ca-4286-beef-1d44e2c30e0d', 'Trio People', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Trio People' for email domain '@triopeople.com.au'
UPDATE Employer SET companyId = '650127c0-32ca-4286-beef-1d44e2c30e0d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Trio People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@triopeople.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('f5f27886-8e5f-40ce-8aa1-3426978999a6', 'Turbo Engineering', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Turbo Engineering' for email domain '@turboengineering.com.au'
UPDATE Employer SET companyId = 'f5f27886-8e5f-40ce-8aa1-3426978999a6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Turbo Engineering' AND c.verifiedById IS NULL AND emailAddress LIKE '%@turboengineering.com.au'
GO

-- Move from unverified company 'Turbo Engineering Corporation Pty Ltd' to verified company 'Turbo Engineering' for email domain '@turboengineering.com.au'
UPDATE Employer SET companyId = 'f5f27886-8e5f-40ce-8aa1-3426978999a6' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Turbo Engineering Corporation Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@turboengineering.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('7c57bc80-9247-4bc8-b052-8f69b976e47e', 'Universal Recruitment', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Universal Recruitment' for email domain '@universalrecruitment.com.au'
UPDATE Employer SET companyId = '7c57bc80-9247-4bc8-b052-8f69b976e47e' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Universal Recruitment' AND c.verifiedById IS NULL AND emailAddress LIKE '%@universalrecruitment.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('149bbbff-fa66-46c9-a739-48bb5c79643a', 'Vectis', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Vectis' for email domain '@valuerecruitment.com.au'
UPDATE Employer SET companyId = '149bbbff-fa66-46c9-a739-48bb5c79643a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Vectis' AND c.verifiedById IS NULL AND emailAddress LIKE '%@valuerecruitment.com.au'
GO

-- Move from unverified to verified company 'Vectis' for email domain '@vectis.net.au'
UPDATE Employer SET companyId = '149bbbff-fa66-46c9-a739-48bb5c79643a' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Vectis' AND c.verifiedById IS NULL AND emailAddress LIKE '%@vectis.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d584b832-c82b-4910-8a58-ef94434016eb', 'Vectis People', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Vectis People' for email domain '@vectis.net.au'
UPDATE Employer SET companyId = 'd584b832-c82b-4910-8a58-ef94434016eb' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Vectis People' AND c.verifiedById IS NULL AND emailAddress LIKE '%@vectis.net.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4f2beba3-6c3b-4ebf-ae9c-1384c4493ac1', 'Venator', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Venator' for email domain '@venator.com.au'
UPDATE Employer SET companyId = '4f2beba3-6c3b-4ebf-ae9c-1384c4493ac1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Venator' AND c.verifiedById IS NULL AND emailAddress LIKE '%@venator.com.au'
GO

-- Move from unverified to verified company 'Venator' for email domain '@venitor.com.au'
UPDATE Employer SET companyId = '4f2beba3-6c3b-4ebf-ae9c-1384c4493ac1' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Venator' AND c.verifiedById IS NULL AND emailAddress LIKE '%@venitor.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('a8383b42-6fbe-4b31-9771-43fae9b7c01d', 'Vires', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Vires' for email domain '@vires.com.au'
UPDATE Employer SET companyId = 'a8383b42-6fbe-4b31-9771-43fae9b7c01d' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Vires' AND c.verifiedById IS NULL AND emailAddress LIKE '%@vires.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('248add71-56cf-489c-9b6f-83378d43eb57', 'Virgin Blue', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Virgin Blue' for email domain '@virginblue.com.au'
UPDATE Employer SET companyId = '248add71-56cf-489c-9b6f-83378d43eb57' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Virgin Blue' AND c.verifiedById IS NULL AND emailAddress LIKE '%@virginblue.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0a148ad8-74dc-4cb7-a09e-a3f51ebeba2b', 'Viva9 Pty Ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Viva9 Pty Ltd' for email domain '@commissionmonster.com.au'
UPDATE Employer SET companyId = '0a148ad8-74dc-4cb7-a09e-a3f51ebeba2b' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Viva9 Pty Ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@commissionmonster.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d039a314-d9c8-4560-babc-e5946e20df98', 'WebAlive Pty ltd', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'WebAlive Pty ltd' for email domain '@webalive.com.au'
UPDATE Employer SET companyId = 'd039a314-d9c8-4560-babc-e5946e20df98' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'WebAlive Pty ltd' AND c.verifiedById IS NULL AND emailAddress LIKE '%@webalive.com.au'
GO

-- Move from unverified to verified company 'WHR Solutions' for email domain '@whrsolutions.com.au'
UPDATE Employer SET companyId = 'ffe44654-4736-4709-8ef9-a92e3fef7eb0' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'WHR Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@whrsolutions.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('4fb11d0f-f29e-40ac-bf7f-0413287ed6a8', 'Wintringham', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Wintringham' for email domain '@wintringham.org.au'
UPDATE Employer SET companyId = '4fb11d0f-f29e-40ac-bf7f-0413287ed6a8' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Wintringham' AND c.verifiedById IS NULL AND emailAddress LIKE '%@wintringham.org.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('d7b9ba3d-bba0-4bc1-b8bb-4adab6ed4767', 'Workforce international', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Workforce international' for email domain '@workforce.com.au'
UPDATE Employer SET companyId = 'd7b9ba3d-bba0-4bc1-b8bb-4adab6ed4767' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Workforce international' AND c.verifiedById IS NULL AND emailAddress LIKE '%@workforce.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('0324bf0b-188f-4a80-b3c4-ef08975c32e3', 'You First Financial Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'You First Financial Solutions' for email domain '@youfirst.com.au'
UPDATE Employer SET companyId = '0324bf0b-188f-4a80-b3c4-ef08975c32e3' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'You First Financial Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@youfirst.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('8d977c17-327a-4dd4-91d0-44ac788a6290', 'Your People Solutions', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Your People Solutions' for email domain '@yourpeople.com.au'
UPDATE Employer SET companyId = '8d977c17-327a-4dd4-91d0-44ac788a6290' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Your People Solutions' AND c.verifiedById IS NULL AND emailAddress LIKE '%@yourpeople.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('2fe4ae79-8f4b-4863-946d-39f7b2b88f03', 'ZAMRO International', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'ZAMRO International' for email domain '@zamro.com.au'
UPDATE Employer SET companyId = '2fe4ae79-8f4b-4863-946d-39f7b2b88f03' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'ZAMRO International' AND c.verifiedById IS NULL AND emailAddress LIKE '%@zamro.com.au'
GO

INSERT INTO Company ([id], [name], verifiedById) VALUES ('6bfb5eaa-ebf2-432c-9207-559cccd3950c', 'Zest Marketing Concepts', '1BFF6396-C939-4669-9429-85A1DD2225BC')
GO
-- Move from unverified to verified company 'Zest Marketing Concepts' for email domain '@zestmarketing.com.au'
UPDATE Employer SET companyId = '6bfb5eaa-ebf2-432c-9207-559cccd3950c' FROM Employer e INNER JOIN Company c ON e.companyId = c.[id] INNER JOIN RegisteredUser ru ON e.[id] = ru.[id] WHERE c.[name] = 'Zest Marketing Concepts' AND c.verifiedById IS NULL AND emailAddress LIKE '%@zestmarketing.com.au'
GO

-- All done!
