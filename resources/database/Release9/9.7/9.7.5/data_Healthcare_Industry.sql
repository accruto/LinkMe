UPDATE
	dbo.Industry
SET
	displayName = 'Healthcare, Medical & Pharmaceutical',
	keywordExpression = 'Healthcare OR Medical OR Pharmaceutical',
	shortDisplayName = 'Healthcare, Medical & Pharmaceutical',
	urlName = 'healthcare-medical-pharmaceutical'
WHERE
	id = '180A913D-D05B-49F1-B3C9-45D57368A3EF'
GO

INSERT
	dbo.IndustryAlias (id, industryId, displayName)
VALUES
	('19992EFB-A55F-4a03-B974-1F4F613B9EDB', '180A913D-D05B-49F1-B3C9-45D57368A3EF', 'Healthcare & Medical')
GO


