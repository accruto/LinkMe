

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networkNodes'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
begin

drop table linkme_owner.networkNodes


end
go