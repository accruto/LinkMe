SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'dbo.vw_SearchMe_Resume'))
    DROP VIEW dbo.vw_SearchMe_Resume
GO

CREATE VIEW dbo.vw_SearchMe_Resume
AS
    SELECT id, lensXml as doc,
           sha1sum,
           lastEditedTime,
           lensXmlIsNull as docIsNull
      FROM dbo.Resume
