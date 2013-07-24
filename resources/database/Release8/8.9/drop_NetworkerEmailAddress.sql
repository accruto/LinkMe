if exists (select * from dbo.sysobjects where id = object_id(N'[NetworkerEmailAddress]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[NetworkerEmailAddress]
GO
