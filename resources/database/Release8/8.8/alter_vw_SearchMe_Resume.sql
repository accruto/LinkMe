ALTER VIEW [dbo].[vw_SearchMe_Resume]
AS
    SELECT id, 
		   lensXml as doc,
           sha1sum,
           lensXmlIsNull as docIsNull
    FROM dbo.Resume