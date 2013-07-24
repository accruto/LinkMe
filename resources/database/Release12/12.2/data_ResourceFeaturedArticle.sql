update resourcefeaturedarticle
set resourcearticleid = '75AE0313-EC81-48B7-B8E2-313068695511'
where cssclass = 'article1'
go

update resourcefeaturedarticle
set resourcearticleid = 'BA7E9A7C-FCD6-40D8-AC7F-88D397D5B601'
where cssclass = 'article2'
go

delete from resourcefeaturedarticle
where id = 'DC2F0C82-9F8F-4AD5-9033-A851B0343502'
or id = 'D4A041CC-805C-4186-AC14-7FC7B37556E9'
go

insert into resourcefeaturedarticle values
('E1A0DB49-084D-488C-B547-804EF7A0CAF7',
'532F3A84-F9AC-4F70-87A7-2DF49F608F29',
'jobinterviewing',
0)
go

insert into resourcefeaturedarticle values
('711368A9-BD65-41A6-A581-9507BA9FEF51',
'2AA22168-D0D3-4E41-BC39-37C9E007211D',
'careermanagement',
0)
go
