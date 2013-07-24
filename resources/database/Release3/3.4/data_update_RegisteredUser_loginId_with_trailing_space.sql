update RegisteredUser
set loginId = left(loginId, len(loginId) - 1)
where loginId like '% '
