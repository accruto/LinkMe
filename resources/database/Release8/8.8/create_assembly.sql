IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StrJoin]') AND type = N'AF')
DROP AGGREGATE [dbo].[StrJoin]

IF  EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'LinkMe.SqlServer')
DROP ASSEMBLY [LinkMe.SqlServer]

CREATE ASSEMBLY [LinkMe.SqlServer] FROM '$(LINKME_SCRIPT_PATH)\..\..\assembly\LinkMe.SqlServer.dll'
