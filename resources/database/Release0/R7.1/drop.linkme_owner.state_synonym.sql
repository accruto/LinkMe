if exists (select * from dbo.sysobjects where id = object_id(N'[state_synonym]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [state_synonym]
GO


