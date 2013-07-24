UPDATE linkme_owner.Industry
SET keywordExpression = 'Accounting'
WHERE displayName = 'Accounting'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Administration'
WHERE displayName = 'Administration'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Advertising OR Media OR Entertainment'
WHERE displayName = 'Advert./Media/Entertain.'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Automotive'
WHERE displayName = 'Automotive'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Banking OR "Financial Services"'
WHERE displayName = 'Banking & Fin. Services'

UPDATE linkme_owner.Industry
SET keywordExpression = '"Call Centre" OR "Customer Service"'
WHERE displayName = 'Call Centre/Cust. Service'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Community OR Sport'
WHERE displayName = 'Community & Sport'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Construction'
WHERE displayName = 'Construction'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Consulting OR "Corporate Strategy"'
WHERE displayName = 'Consulting & Corp. Strategy'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Education OR Training'
WHERE displayName = 'Education & Training'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Engineering'
WHERE displayName = 'Engineering'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Government OR Defence'
WHERE displayName = 'Government/Defence'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Healthcare OR Medical'
WHERE displayName = 'Healthcare & Medical'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Hospitality OR Tourism'
WHERE displayName = 'Hospitality & Tourism'

UPDATE linkme_owner.Industry
SET keywordExpression = 'HR OR Recruitment'
WHERE displayName = 'HR & Recruitment'

UPDATE linkme_owner.Industry
SET keywordExpression = 'IT OR Telecommunications'
WHERE displayName = 'I.T. & T'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Insurance OR Superannuation'
WHERE displayName = 'Insurance & Superannuation'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Legal'
WHERE displayName = 'Legal'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Manufacturing OR Operations'
WHERE displayName = 'Manufacturing/Operations'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Mining OR Oil OR Gas'
WHERE displayName = 'Mining, Oil & Gas'

UPDATE linkme_owner.Industry
SET keywordExpression = NULL
WHERE displayName = 'Other'

UPDATE linkme_owner.Industry
SET keywordExpression = '"Primary Industry"'
WHERE displayName = 'Primary Industry'

UPDATE linkme_owner.Industry
SET keywordExpression = '"Real Estate" OR Property'
WHERE displayName = 'Real Estate & Property'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Retail OR "Consumer Products"'
WHERE displayName = 'Retail & Consumer Prods.'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Sales OR Marketing'
WHERE displayName = 'Sales & Marketing'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Science OR Technology'
WHERE displayName = 'Science & Technology'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Self-Employment'
WHERE displayName = 'Self-Employment'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Trades OR Services'
WHERE displayName = 'Trades & Services'

UPDATE linkme_owner.Industry
SET keywordExpression = 'Transport OR Logistics'
WHERE displayName = 'Transport & Logistics'

GO

SELECT *
FROM linkme_owner.Industry
WHERE keywordExpression IS NULL
