/*
   Friday, 24 March 2006 3:05:22 PM
   User: 
   Server: (LOCAL)
   Database: LinkMe
   Application: MS SQLEM - Data Tools
*/

ALTER TABLE linkme_owner.mail_merge_task ADD
	fromaddress nvarchar(320) NULL CONSTRAINT DF_mail_merge_task_fromaddress DEFAULT N'support@linkme.com.au'
GO
