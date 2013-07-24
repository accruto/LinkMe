Order of execution:

1. alter_File_add_size_hash_dataId.sql
2. TaskRunner /testrun au.com.venturelogic.linkme.taskRunner.temp.SetFileHashesTask
3. alter_File_make_size_hash_notnull.sql

4. alter_JobAd_drop_FK_Image.sql
5. TaskRunner /testrun au.com.venturelogic.linkme.taskRunner.temp.MigrateImageDataTask
6. alter_JobAd_add_FK_BrandingLogoFile.sql
7. drop_ImageData.sql

8. alter_neworker_profile_add_profilePhotoId.sql
9. TaskRunner /testrun au.com.venturelogic.linkme.taskRunner.temp.MigratePhotoTask
10. drop_Photo.sql

11. data_employer_profile_enable_SuggestedCandidates.sql
12. data_ProductDefinition_add_ITWireJobAdPosting.sql
13. create_EventLogExtraData.sql