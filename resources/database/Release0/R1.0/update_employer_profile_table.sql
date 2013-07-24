update linkme_owner.employer_profile 
set subRole = 'Employer';

update linkme_owner.employer_profile 
set emailAddress = (select userId from user_profile where linkme_owner.user_profile.id = linkme_owner.employer_profile.id);