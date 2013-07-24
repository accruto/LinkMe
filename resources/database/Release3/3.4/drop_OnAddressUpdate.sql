if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OnAddressUpdate]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[OnAddressUpdate]
GO

