-- Categories
-------------

DECLARE @grandParentId UNIQUEIDENTIFIER
DECLARE @parentId UNIQUEIDENTIFIER
DECLARE @categoryId UNIQUEIDENTIFIER
DECLARE @categoryName NVARCHAR(500)

SET @grandParentId = 'e69254e0-f5e6-47c2-899c-8041d77eef8b'

-- Accounting courses

SET @parentId = 'FAF62B20-0302-4e86-BDB5-8EE27443FF71'
SET @categoryName = 'Accounting Courses'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

SET @categoryId = 'A6E73736-324C-4b7a-80A7-8E507C714D8D'
SET @categoryName = 'CA Study Support Program (for Chartered Accountant Program students)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = '23FC3EBC-1C87-4d69-838D-FD47EB618C2B'
SET @categoryName = 'Tax Fundamentals Workshop (for graduates and business professionals seeking to learn the essentials of tax)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'E87D2350-67CB-4ec4-8F1D-B9697AA9D2B8'
SET @categoryName = 'Tax Update (CPD) Training (online and face-to-face training for professional tax accountants)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'FA876523-5EC5-40f8-A145-65373B6B4343'
SET @categoryName = 'Master of Applied Finance'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

-- Real estate courses

SET @parentId = '67E3AA08-1EE8-4f10-9F1D-3CB72DD55B84'
SET @categoryName = 'Real Estate Courses'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

SET @categoryId = '0B55B6B6-6389-46d1-A84A-A852AAA4DA40'
SET @categoryName = 'Registration Program (entry level no experience required)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = '64C96CA6-C5BB-4ed3-87C2-4EF59570972F'
SET @categoryName = 'Licence Program (no experience required)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = '1750C9C8-0D0B-485b-884F-3BE7E0D5F410'
SET @categoryName = 'Intensive Licence Program (experienced salespeople)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'E28D442A-05B4-4a12-8E97-888B523BE878'
SET @categoryName = 'CPD (Continuing Professional Development)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

-- Financial services courses

SET @parentId = '56099C39-4C57-4464-A2D6-A9FE5F1561CE'
SET @categoryName = 'Financial Services Courses'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

SET @categoryId = 'E219FE13-F3A5-49af-A709-C48EADA545F5'
SET @categoryName = 'Master of Applied Finance'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = '5B855E4D-5C7B-400b-8B75-2EE4B1886C6C'
SET @categoryName = 'Graduate Diploma of Applied Finance'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'DAA51518-3FAB-4fe0-A085-B0DF1CD6A259'
SET @categoryName = 'Graduate Certificate of Applied Finance'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'CA59E895-2810-45c0-99B0-E886C87E7CA2'
SET @categoryName = 'Advanced Diploma of Financial Services (Financial Planning)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'F36CC369-E5B4-4d89-B60A-1E1E69CE0FCE'
SET @categoryName = 'Diploma of Financial Services (Financial Planning)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = '06BDF5D9-A8B4-46d7-A4A3-2AC64D20AC85'
SET @categoryName = 'RG 146 Compliance'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'A1332DA1-5F60-4c0b-87E3-4916B8AB7EF6'
SET @categoryName = 'Mortgage/Finance Broking'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

-- Tax courses

SET @parentId = 'EBE4756B-153C-4759-AD3D-242098D36F43'
SET @categoryName = 'Tax Courses'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @parentId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@parentId, @grandParentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @grandParentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @parentId

SET @categoryId = '2300A743-1639-41d6-B2F9-75969AE7D0CF'
SET @categoryName = 'Tax Fundamentals Workshop (for graduates and business professionals seeking to learn the essentials of tax)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

SET @categoryId = 'C2177F1C-014E-46d2-BD64-193694C0695F'
SET @categoryName = 'Tax Update (CPD) Training (online and face-to-face training for professional tax accountants)'

IF NOT EXISTS (SELECT * FROM OfferCategory WHERE id = @categoryId)
	INSERT
		OfferCategory (id, parentId, name, enabled, deleted)
	VALUES
		(@categoryId, @parentId, @categoryName, 1, 0)
ELSE
	UPDATE
		OfferCategory
	SET
		parentId = @parentId,
		name = @categoryName,
		enabled = 1,
		deleted = 0
	WHERE
		id = @categoryId

-- Providers
------------

-- Kaplan Tax & Accounting

DECLARE @providerId UNIQUEIDENTIFIER
DECLARE @providerName NVARCHAR(100)
DECLARE @offeringId UNIQUEIDENTIFIER
DECLARE @offeringName NVARCHAR(100)

SET @providerName = 'Kaplan Tax & Accounting'
SET @providerId = '519594B9-517E-491f-9C49-998DA6D93F73'

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, @providerName, 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = @providerName,
		enabled = 1
	WHERE
		id = @providerId

SET @offeringName = 'CA Study Support Program (for Chartered Accountant Program students)'
SET @offeringId = '4444912F-8E38-41fb-A461-EA8524F4D462'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'A6E73736-324C-4b7a-80A7-8E507C714D8D'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Tax Fundamentals Workshop (for graduates and business professionals seeking to learn the essentials of tax)'
SET @offeringId = 'ECEAF673-B45C-4467-874D-C5FC30DC5786'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = '23FC3EBC-1C87-4d69-838D-FD47EB618C2B'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @categoryId = '2300A743-1639-41d6-B2F9-75969AE7D0CF'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Tax Update (CPD) Training (online and face-to-face training for professional tax accountants)'
SET @offeringId = '9F858256-E438-46dd-963E-C6B01F1CC695'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'E87D2350-67CB-4ec4-8F1D-B9697AA9D2B8'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @categoryId = 'C2177F1C-014E-46d2-BD64-193694C0695F'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

-- Kaplan Advanced Finance

SET @providerName = 'Kaplan Advanced Finance'
SET @providerId = '82EDCAE2-E143-44f4-8629-EC5AA8481DE8'

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, @providerName, 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = @providerName,
		enabled = 1
	WHERE
		id = @providerId

SET @offeringName = 'Master of Applied Finance'
SET @offeringId = '03C889FB-CB1E-48c7-9269-62B33AAE6037'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'FA876523-5EC5-40f8-A145-65373B6B4343'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @categoryId = 'E219FE13-F3A5-49af-A709-C48EADA545F5'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Graduate Diploma of Applied Finance'
SET @offeringId = '7D7BF861-C2B3-4d50-AC76-D849D87717DD'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = '5B855E4D-5C7B-400b-8B75-2EE4B1886C6C'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Graduate Certificate of Applied Finance'
SET @offeringId = 'C6F36E34-4C42-4b2c-BF56-B982B7F70075'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'DAA51518-3FAB-4fe0-A085-B0DF1CD6A259'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

-- Kaplan Finance

SET @providerName = 'Kaplan Finance'
SET @providerId = 'FFEAB827-F9DF-446f-82ED-C36CB9A0AF3E'

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, @providerName, 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = @providerName,
		enabled = 1
	WHERE
		id = @providerId

SET @offeringName = 'Advanced Diploma of Financial Services (Financial Planning)'
SET @offeringId = 'DEAD0DD2-7F1D-4fd8-B518-2C436C5B24FC'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'CA59E895-2810-45c0-99B0-E886C87E7CA2'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Diploma of Financial Services (Financial Planning)'
SET @offeringId = 'AC839BC2-2BBD-4710-9A52-933021020971'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'F36CC369-E5B4-4d89-B60A-1E1E69CE0FCE'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'RG 146 Compliance'
SET @offeringId = '99FF1CB4-59C3-4cb1-B8D2-A188C2B41693'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = '06BDF5D9-A8B4-46d7-A4A3-2AC64D20AC85'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Mortgage/Finance Broking'
SET @offeringId = 'FD007234-A6E5-4c27-BF2C-55252EC35DC2'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'A1332DA1-5F60-4c0b-87E3-4916B8AB7EF6'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)
-- Kaplan Real Estate

SET @providerName = 'Kaplan Real Estate'
SET @providerId = '92C60FBC-2291-4e66-B4BC-92038AA53516'

IF NOT EXISTS (SELECT * FROM OfferProvider WHERE id = @providerId)
	INSERT
		OfferProvider (id, name, enabled)
	VALUES
		(@providerId, @providerName, 1)
ELSE
	UPDATE
		OfferProvider
	SET
		name = @providerName,
		enabled = 1
	WHERE
		id = @providerId

SET @offeringName = 'Registration Program (entry level no experience required)'
SET @offeringId = '3EEF0711-7C60-4c5c-AB42-2320261AA605'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = '0B55B6B6-6389-46d1-A84A-A852AAA4DA40'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Licence Program (no experience required)'
SET @offeringId = '6D495B8F-618B-4fee-A0A0-9A31FFBAB698'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = '64C96CA6-C5BB-4ed3-87C2-4EF59570972F'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'Intensive Licence Program (experienced salespeople)'
SET @offeringId = '68D1C383-8B66-412d-BAD8-73E673822B51'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = '1750C9C8-0D0B-485b-884F-3BE7E0D5F410'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)

SET @offeringName = 'CPD (Continuing Professional Development)'
SET @offeringId = '359D4825-64FF-4754-9FAE-FED81E1C0426'

IF NOT EXISTS (SELECT * FROM Offering WHERE id = @offeringId)
	INSERT
		Offering (id, providerId, name, enabled)
	VALUES
		(@offeringId, @providerId, @offeringName, 1)
ELSE
	UPDATE
		Offering
	SET
		providerId = @providerId,
		name = @offeringName,
		enabled = 1
	WHERE
		id = @offeringId

SET @categoryId = 'E28D442A-05B4-4a12-8E97-888B523BE878'

IF NOT EXISTS (SELECT * FROM OfferCategoryOffering WHERE categoryId = @categoryId AND offeringId = @offeringId)
	INSERT
		OfferCategoryOffering (categoryId, offeringId)
	VALUES
		(@categoryId, @offeringId)
