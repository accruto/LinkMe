if exists (select * from dbo.sysobjects where id = object_id(N'[linkme_owner].[GetABNTransactionSummaryByDateRange]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [linkme_owner].[GetABNTransactionSummaryByDateRange]
GO

