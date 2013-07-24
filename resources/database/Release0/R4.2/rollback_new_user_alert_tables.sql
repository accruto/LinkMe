IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_alert_parameters'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	DROP TABLE user_alert_parameters

END
GO

IF EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_alert_data'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN
	
	DROP TABLE user_alert_data

END
GO