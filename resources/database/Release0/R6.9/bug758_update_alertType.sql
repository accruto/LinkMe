UPDATE linkme_owner.user_alert_data
SET alertType = REPLACE(alertType, ', ', ',')
WHERE alertType LIKE '%, %'