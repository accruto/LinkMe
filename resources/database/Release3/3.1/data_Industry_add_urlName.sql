UPDATE
	dbo.Industry
SET
	urlName = map.urlName
FROM
	dbo.Industry AS I
INNER JOIN (
	SELECT urlName = 'accounting', displayName = 'Accounting'
	UNION SELECT urlName = 'administration',  displayName = 'Administration'
	UNION SELECT urlName = 'advertising-media-entertainment', displayName = 'Advertising, Media & Entertainment'
	UNION SELECT urlName = 'automotive', displayName = 'Automotive'
	UNION SELECT urlName = 'banking-financial-services', displayName = 'Banking & Financial Services'
	UNION SELECT urlName = 'call-centre-customer-service', displayName = 'Call Centre & Customer Service'
	UNION SELECT urlName = 'community-sport', displayName = 'Community & Sport'
	UNION SELECT urlName = 'construction', displayName = 'Construction'
	UNION SELECT urlName = 'consulting-corporate-strategy', displayName = 'Consulting & Corporate Strategy'
	UNION SELECT urlName = 'education-training', displayName = 'Education & Training'
	UNION SELECT urlName = 'engineering', displayName = 'engineering'
	UNION SELECT urlName = 'government-defence', displayName = 'Government & Defence'
	UNION SELECT urlName = 'healthcare-medical', displayName = 'Healthcare & Medical'
	UNION SELECT urlName = 'hospitality-tourism', displayName = 'Hospitality & Tourism'
	UNION SELECT urlName = 'hr-recruitment', displayName = 'HR & Recruitment'
	UNION SELECT urlName = 'insurance-superannuation', displayName = 'Insurance & Superannuation'
	UNION SELECT urlName = 'it-telecommunications', displayName = 'IT & Telecommunications'
	UNION SELECT urlName = 'legal', displayName = 'Legal'
	UNION SELECT urlName = 'manufacturing-operations', displayName = 'Manufacturing & Operations'
	UNION SELECT urlName = 'mining-oil-gas', displayName = 'Mining, Oil & Gas'
	UNION SELECT urlName = 'other', displayName = 'Other'
	UNION SELECT urlName = 'primary-industry', displayName = 'Primary Industry'
	UNION SELECT urlName = 'real-estate-property', displayName = 'Real Estate & Property'
	UNION SELECT urlName = 'retail-consumer-products', displayName = 'Retail & Consumer Products'
	UNION SELECT urlName = 'sales-marketing', displayName = 'Sales & Marketing'
	UNION SELECT urlName = 'science-technology', displayName = 'Science & Technology'
	UNION SELECT urlName = 'self-employment', displayName = 'Self-Employment'
	UNION SELECT urlName = 'trades-services', displayName = 'Trades & Services'
	UNION SELECT urlName = 'transport-logistics', displayName = 'Transport & Logistics'
	) AS map ON map.displayName = I.displayName

GO
