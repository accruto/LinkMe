update linkme_owner.locality
set postcode =  '0' + postcode 
where ( state = 'nt' and ( postcode like '8__' or postcode like '9__' ) ) or
      ( state = 'act' and postcode = '200' )
go
