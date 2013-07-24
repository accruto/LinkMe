DECLARE @providerId UNIQUEIDENTIFIER
SET @providerId = '{8168214E-DCDA-4660-9D8B-BD9C9E2F7F0A}'
DECLARE @parentCategoryId UNIQUEIDENTIFIER

-- Provider

EXEC dbo.CreateOfferProvider @providerId, 'Australian Business Academy'

-- Accounting

SET @parentCategoryId = 'FAF62B20-0302-4E86-BDB5-8EE27443FF71'
EXEC dbo.CreateOffering '{CEEB7275-7CEF-4fec-9E05-61BE89FDAC6C}', 'Diploma of Accounting', @providerId, 'BA3BA4C5-1765-40DC-97FB-754EDD28B65A', @parentcategoryId
EXEC dbo.CreateOffering '{196A6D9C-8FB6-45b0-B19B-2DA8626B444B}', 'Advanced Diploma of Accounting', @providerId, 'D00F960A-67CF-4C7F-8969-9CCEC9FFBF42', @parentcategoryId

-- Art, design & Digital Media

SET @parentCategoryId = 'AB12D51C-92CC-4717-A3B2-A50BFEE6A097'
EXEC dbo.CreateOffering '{2E51EF39-F59E-42d8-86CA-4032472306D5}', 'Diploma of Graphic Design', @providerId, '{724460B8-BE86-4dbc-BA30-130D11C6489F}', @parentcategoryId
EXEC dbo.CreateOffering '{2F67E9A8-5E9F-4497-87E2-90A631A6A8E0}', 'Advanced Diploma of Grpahic Design', @providerId, '{81681F46-2284-45dc-A385-F175E051C020}', @parentcategoryId

-- Business Courses

SET @parentCategoryId = 'D328CBF4-3429-4D49-A792-442F85FC7763'
EXEC dbo.CreateOffering '{0EFF5DB5-8AB9-4725-99FA-246CD0761A4B}', 'Diploma of Management', @providerId, 'AA125008-067E-400B-8C90-EF61FAF534D8', @parentcategoryId
EXEC dbo.CreateOffering '{16D0E1D0-0E5F-49ec-B2DE-5257C7E04F89}', 'Diploma of Business Administration', @providerId, '{115568F4-6F23-4a5e-B594-43C99C081056}', @parentcategoryId

-- Information Technology & Computing Courses

SET @parentCategoryId = 'AE0DB911-537D-46DE-B971-4B46DC266372'
EXEC dbo.CreateOffering '{47CED3F3-E890-4516-8113-36EC1BEF373D}', 'Diploma of Information Technology', @providerId, '{18553C60-8509-44d3-AEFD-802D4AD1C484}', @parentcategoryId

-- Marketing Courses

SET @parentCategoryId = 'E322D095-8939-476A-B816-8798060E7F2F'
EXEC dbo.CreateOffering '{11E7FD9B-2C86-4baf-B487-F86D46E2607E}', 'Advanced Diploma of Marketing', @providerId, '{02D5AA2F-3AE7-4ac8-95CE-665F12F699A9}', @parentcategoryId

-- Tourism & Hospitality Courses

SET @parentCategoryId = '5CFF8309-DB97-485F-A5F5-3641DF04795D'
EXEC dbo.CreateOffering '{47326ACF-05D1-4bce-A37F-8ED8D51E3FAA}', 'Diploma of Travel and Tourism', @providerId, '{505D1824-4F2D-4f45-BC82-31B90B87FEBC}', @parentcategoryId
EXEC dbo.CreateOffering '{F513DF81-F205-4780-8D1E-3760C167047A}', 'Advanced Diploma of Travel and Tourism', @providerId, '{88271B36-5EBD-4f22-876F-C1587B6A8E9C}', @parentcategoryId

-- Alternative Careers

SET @parentCategoryId = '385188B4-D205-46B5-AA18-6170B9623407'
EXEC dbo.CreateOffering '{02AA47D6-87D6-4154-B90E-55AA364FD67C}', 'Help me start my career in Tourism & Travel', @providerId, '{0B59F1CA-9CDD-46eb-B3FC-03DDAC5E66AA}', @parentcategoryId
EXEC dbo.CreateOffering '{9A27A185-AC3C-45b3-9CCD-122E3BEE71F5}', 'Help me start my career in the Sports Industry', @providerId, '{3005E6C5-1468-461b-B793-47EF58A5DD62}', @parentcategoryId
EXEC dbo.CreateOffering '{4664D351-1226-4c46-B55E-ADF5AD1EEE53}', 'Help me start my career in Graphic Design', @providerId, '{180BE534-E31D-47b4-88DD-4E69AEFCB512}', @parentcategoryId
