

IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'networkNodes'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
begin

create table linkme_owner.networkNodes
(
	networkerId varchar(32) primary key,
	nodeXml text,
	updatedDate datetime
)

end
go