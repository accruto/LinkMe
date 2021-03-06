IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StrJoin]') AND type = N'AF')
DROP AGGREGATE [dbo].[StrJoin]

CREATE AGGREGATE [dbo].[StrJoin](@input nvarchar(4000)) RETURNS nvarchar(max)
EXTERNAL NAME [LinkMe.Framework.SqlServer].[LinkMe.Framework.SqlServer.StrJoin]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitGuids]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SplitGuids]
GO

CREATE FUNCTION [dbo].[SplitGuids] (@delimiter NVARCHAR(MAX), @input NVARCHAR(MAX))
RETURNS TABLE (value UNIQUEIDENTIFIER)
AS EXTERNAL NAME [LinkMe.Framework.SqlServer].[LinkMe.Framework.SqlServer.UserDefinedFunctions].SplitGuids
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitInts]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SplitInts]
GO

CREATE FUNCTION [dbo].[SplitInts] (@delimiter NVARCHAR(MAX), @input NVARCHAR(MAX))
RETURNS TABLE (value INT)
AS EXTERNAL NAME [LinkMe.Framework.SqlServer].[LinkMe.Framework.SqlServer.UserDefinedFunctions].SplitInts
GO