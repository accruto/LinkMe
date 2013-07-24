isql -U linkme_owner -P linkme -d linkme -i drop_tables.sql 
isql -U linkme_owner -P linkme -d linkme -i create_tables.sql 
isql -U linkme_owner -P linkme -d linkme -i update_database_version_table.sql
pause
