IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_alert_parameters'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

	CREATE TABLE user_alert_parameters
	(
		id varchar(50),
		parameterName varchar(50) NULL,
		parameterValue TEXT NULL
	)

	CREATE NONCLUSTERED INDEX ix_user_alert_parameters_id on user_alert_parameters(Id)
	CREATE NONCLUSTERED INDEX ix_user_alert_parameters_parameterName on user_alert_parameters(parameterName)
END
GO

IF NOT EXISTS (
	SELECT 1
	FROM information_schema.tables
	WHERE table_name = 'user_alert_data'
	AND table_schema = 'linkme_owner'
	AND table_type = 'BASE TABLE'
)
BEGIN

	CREATE TABLE user_alert_data
	(
		id varchar(50),
		userProfileId varchar(50) NULL,
		alertType varchar(400) NULL	
	)

	
	CREATE NONCLUSTERED INDEX ix_user_alert_data_id on user_alert_data(Id)
	CREATE NONCLUSTERED INDEX ix_user_alert_data_user_profile_id on user_alert_data(userProfileId)

END
GO