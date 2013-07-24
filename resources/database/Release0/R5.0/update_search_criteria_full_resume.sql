IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'search_criteria'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	update linkme_owner.search_criteria set value= "Full Resumé" Where value like 'Full Resum%' and (criteriaType='ddlResumeCriteria1' or criteriaType='ddlResumeCriteria2' or criteriaType='ddlResumeCriteria3')
	
END
GO