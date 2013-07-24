UPDATE networker_profile
SET matchEmployerSearches = 0	
WHERE id IN (
SELECT id FROM user_profile
	WHERE userId = 'linkme3@objectconsulting.com.au');
			
  
  



