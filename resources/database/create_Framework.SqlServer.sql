IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StrJoin]') AND type = N'AF')
DROP AGGREGATE [dbo].[StrJoin]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitGuids]') AND type = N'AF')
DROP AGGREGATE [dbo].[SplitGuids]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitGuids]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SplitGuids]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitInts]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SplitInts]

IF  EXISTS (SELECT * FROM sys.assemblies WHERE name = N'LinkMe.SqlServer')
DROP ASSEMBLY [LinkMe.SqlServer]

IF  EXISTS (SELECT * FROM sys.assemblies WHERE name = N'LinkMe.Framework.SqlServer')
DROP ASSEMBLY [LinkMe.Framework.SqlServer]

--CREATE ASSEMBLY [LinkMe.Framework.SqlServer] FROM 'C:\LinkMe\Bin\LinkMe.Framework.SqlServer.dll'
CREATE ASSEMBLY [LinkMe.Framework.SqlServer] FROM '$(LINKME_SCRIPT_PATH)\..\..\..\..\Framework\SqlServer\Bin\LinkMe.Framework.SqlServer.dll'
GO

CREATE AGGREGATE [dbo].[StrJoin](@input nvarchar(4000)) RETURNS nvarchar(max)
EXTERNAL NAME [LinkMe.Framework.SqlServer].[LinkMe.Framework.SqlServer.StrJoin]
GO

CREATE FUNCTION [dbo].[SplitGuids] (@delimiter NVARCHAR(MAX), @input NVARCHAR(MAX))
RETURNS TABLE (value UNIQUEIDENTIFIER)
AS EXTERNAL NAME [LinkMe.Framework.SqlServer].[LinkMe.Framework.SqlServer.UserDefinedFunctions].SplitGuids
GO

CREATE FUNCTION [dbo].[SplitInts] (@delimiter NVARCHAR(MAX), @input NVARCHAR(MAX))
RETURNS TABLE (value INT)
AS EXTERNAL NAME [LinkMe.Framework.SqlServer].[LinkMe.Framework.SqlServer.UserDefinedFunctions].SplitInts
GO
