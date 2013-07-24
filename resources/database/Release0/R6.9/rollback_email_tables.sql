if exists (select * from dbo.sysobjects where id = object_id(N'[mail_merge_task]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [mail_merge_task]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[mail_merge_content_item]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [mail_merge_content_item]
GO


