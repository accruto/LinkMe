-- Resume a47daec177704d159c8ec0d2a30f865f causes the LENS DocServer to crash!
-- Luckily it belongs to an unregistered networker - just delete it.

DELETE linkme_owner.networker_resume_data
WHERE id = 'a47daec177704d159c8ec0d2a30f865f'
