
INSERT INTO user_profile
  (id, userId, password, firstName, lastName, active)
  values ('af95f3d2191b425883400b2a07827a40', 'linkme1@objectconsulting.com.au', 'X03MO1qnZdYdgyfeuILPmQ==', 'linkme1', 
          'linkme1', 1);

INSERT INTO networker_profile
  (id, resumeId, resumeUpdatedRemindedDate, contactsCount, networkerMatches, employerMatches, employerMisses, matchEmployerSearches, matchNetworkerSearches, postcode, matchOtherStatesSearches)
  values ('af95f3d2191b425883400b2a07827a40', NULL, NULL, 0, 0, 0, 0, 0, 0, 'NULL', 0); 			

INSERT INTO user_profile
  (id, userId, password, firstName, lastName, active)
  values ('6b365bebf7024d2dab65491c0638e71a', 'linkme2@objectconsulting.com.au', 'X03MO1qnZdYdgyfeuILPmQ==', 'linkme2', 
          'linkme2', 1);

INSERT INTO networker_profile
  (id, resumeId, resumeUpdatedRemindedDate, contactsCount, networkerMatches, employerMatches, employerMisses, matchEmployerSearches, matchNetworkerSearches, postcode, matchOtherStatesSearches)
  values ('6b365bebf7024d2dab65491c0638e71a', NULL, NULL, 0, 0, 0, 0, 0, 0, 'NULL', 0); 	

INSERT INTO user_profile
  (id, userId, password, firstName, lastName, active) 
  values ('ac2ce2e1a5b04738bda69abc02ff5aea', 'linkme10@objectconsulting.com.au', 'X03MO1qnZdYdgyfeuILPmQ==', 'pontius', 
          'pilate', 0);

INSERT INTO employer_profile
  (id, organisationName, contactPhoneNumber, credits)
  values ('ac2ce2e1a5b04738bda69abc02ff5aea', 'sqpr', '12345678', 0);