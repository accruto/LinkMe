DECLARE @id UNIQUEIDENTIFIER
DECLARE @offeringId UNIQUEIDENTIFIER

-- AHRI Short Courses - Human Resource Essentials

SET @offeringId = 'BDAEB859-4B0F-41A2-95EA-0C3C997CEEC6'

DELETE
	dbo.OfferingCriteria
FROM
	dbo.OfferingCriteria AS c
INNER JOIN
	dbo.OfferingCriteriaSet AS s ON S.id = c.id
WHERE
	s.offeringId = @offeringId

DELETE
	dbo.OfferingCriteriaSet
WHERE
	offeringId = @offeringId

SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Advisor', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR administrator', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Manager', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Director', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Officer', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Generalist', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Executive', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Assistant', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Coordinator', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL

-- AHRI Short Courses - People Management Essentials

SET @offeringId = 'B851FDA1-09E3-4502-9BF4-98E60C34481E'

DELETE
	dbo.OfferingCriteria
FROM
	dbo.OfferingCriteria AS c
INNER JOIN
	dbo.OfferingCriteriaSet AS s ON S.id = c.id
WHERE
	s.offeringId = @offeringId

DELETE
	dbo.OfferingCriteriaSet
WHERE
	offeringId = @offeringId

SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Advisor', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR administrator', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Manager', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Director', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Officer', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Generalist', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Executive', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Assistant', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'HR Coordinator', 'Keywords', '"human resources" OR recruitment OR learning OR development OR "talent management" OR compliance OR "organisational development" OR performance OR policy OR planning OR engagement OR resourcing OR payroll', NULL, NULL, NULL, NULL

-- HR industry association for individual HR practitioners

SET @offeringId = '6FFE6723-5907-4BB7-BE38-1236AE1FE55A'

DELETE
	dbo.OfferingCriteria
FROM
	dbo.OfferingCriteria AS c
INNER JOIN
	dbo.OfferingCriteriaSet AS s ON S.id = c.id
WHERE
	s.offeringId = @offeringId

DELETE
	dbo.OfferingCriteriaSet
WHERE
	offeringId = @offeringId

SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'Keywords', 'HR OR "Human Resources" OR "People management"', NULL, NULL, NULL, NULL, NULL, NULL

-- HR Industry association for organisations

SET @offeringId = '907E01AA-EFEE-40FA-B43F-8B826B6933AE'

DELETE
	dbo.OfferingCriteria
FROM
	dbo.OfferingCriteria AS c
INNER JOIN
	dbo.OfferingCriteriaSet AS s ON S.id = c.id
WHERE
	s.offeringId = @offeringId

DELETE
	dbo.OfferingCriteriaSet
WHERE
	offeringId = @offeringId

SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'Director', 'Keywords', '"Human resources" OR people OR recruitment OR "learning & development"', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'GM', 'Keywords', '"Human resources" OR people OR recruitment OR "learning & development"', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'CEO', 'Keywords', '"Human resources" OR people OR recruitment OR "learning & development"', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'Executive', 'Keywords', '"Human resources" OR people OR recruitment OR "learning & development"', NULL, NULL, NULL, NULL
SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'JobTitle', 'Manager', 'Keywords', '"Human resources" OR people OR recruitment OR "learning & development"', NULL, NULL, NULL, NULL

-- HR Industry Association for students studying HR

SET @offeringId = '8E0F3648-AA4A-4362-B94C-671FAE271F44'

DELETE
	dbo.OfferingCriteria
FROM
	dbo.OfferingCriteria AS c
INNER JOIN
	dbo.OfferingCriteriaSet AS s ON S.id = c.id
WHERE
	s.offeringId = @offeringId

DELETE
	dbo.OfferingCriteriaSet
WHERE
	offeringId = @offeringId

SET @id = NEWID()
EXEC dbo.CreateOfferingCriteria @id, @offeringId, 'Country', 1, 'Keywords', 'Courses OR graduate OR degree OR education OR university OR student', NULL, NULL, NULL, NULL, NULL, NULL

