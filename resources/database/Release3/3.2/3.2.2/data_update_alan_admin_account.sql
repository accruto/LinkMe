-- Update Alan Admin account to have last name Cocks instead of last name Sparkes (Khan is an idiot)
UPDATE
	RegisteredUser 
SET
	lastName = 'Cocks'
WHERE
	loginId = 'Alan Admin'