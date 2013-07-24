if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[networker_assessment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [linkme_owner].[networker_assessment]
GO
