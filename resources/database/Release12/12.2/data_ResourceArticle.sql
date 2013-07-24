update resourcearticle
set text = REPLACE(cast(text as varchar(8000)),'<p><p>','<p>')
go
update resourcearticle
set text = REPLACE(cast(text as varchar(8000)),'</p></p>','</p>')
go
