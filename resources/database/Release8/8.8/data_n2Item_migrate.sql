UPDATE dbo.n2Item
SET Community = Title
GO

UPDATE dbo.n2Item
SET Title = NULL
GO

UPDATE dbo.n2Item
SET Page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome' 
WHERE parentID = '2'

UPDATE dbo.n2Item
SET Page = 'au.com.venturelogic.linkme.web.default' 
WHERE parentID = '1'

GO

UPDATE dbo.n2Item
SET  parentID = NULL
WHERE Page = 'au.com.venturelogic.linkme.web.ui.registered.networkers.networkerhome'

UPDATE dbo.n2Item
SET parentID =  NULL
WHERE Page = 'au.com.venturelogic.linkme.web.default'

GO