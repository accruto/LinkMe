isql -U linkme_owner -P linkme -d linkme -i add_columns_employer_profile_table.sql
isql -U linkme_owner -P linkme -d linkme -i update_employer_profile_table.sql 
isql -U linkme_owner -P linkme -d linkme -i add_columns_user_profile_table.sql
isql -U linkme_owner -P linkme -d linkme -i update_database_version_table.sql
pause
