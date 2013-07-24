if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetPostcodesWithinRadius]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetPostcodesWithinRadius]
GO
