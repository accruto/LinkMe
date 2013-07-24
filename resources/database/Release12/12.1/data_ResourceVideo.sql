update resourcevideo set shortUrl = 'http://bit.ly/yU3H53' where id = '049fbd01-dff6-44d6-bad2-b26bef2edf58'
update resourcevideo set shortUrl = 'http://bit.ly/A20a2P' where id = 'f9c3d03b-6c2d-401b-97e0-3b56cbcf1115'
update resourcevideo set shortUrl = 'http://bit.ly/A8Xpyb' where id = '3d377694-010a-42aa-9e60-07a1a2ccb8a9'
update resourcevideo set shortUrl = 'http://bit.ly/AqaiJO' where id = '7a4c19a8-0890-430c-bf90-5855eceaba60'
update resourcevideo set shortUrl = 'http://bit.ly/wpgg4x' where id = 'f0eac57c-f360-404e-84d3-6aed6ae4704b'
update resourcevideo set shortUrl = 'http://bit.ly/zKjKEP' where id = 'e3a90071-9935-4658-91c6-59ce63cbe9fa'
update resourcevideo set shortUrl = 'http://bit.ly/xAgKos' where id = '0ab554f3-59ed-4956-b890-1e1ada36ef33'
update resourcevideo set shortUrl = 'http://bit.ly/wASAig' where id = '7e5e8a6c-3296-4146-8d31-d36ed2625ecf'
update resourcevideo set shortUrl = 'http://bit.ly/xgCVjR' where id = '21f0ce20-ddf9-416c-956c-9191b1e373c0'
update resourcevideo set shortUrl = 'http://bit.ly/yRXC00' where id = '91dcad3f-d618-48b0-809c-a768e2ee928c'
update resourcevideo set shortUrl = 'http://bit.ly/x63rfy' where id = '829961ff-dfd3-41cb-90b9-5b11ea68292a'
update resourcevideo set shortUrl = 'http://bit.ly/zzGQtN' where id = 'cfe83e81-e412-417e-8b85-9e6d2441049d'
update resourcevideo set shortUrl = 'http://bit.ly/AbXpLF' where id = '2c22a93b-9ce4-4683-8b10-718c2d0a8f43'
update resourcevideo set shortUrl = 'http://bit.ly/w1KeMD' where id = '269029f0-03aa-4cca-bc0d-b5406a148938'
update resourcevideo set shortUrl = 'http://bit.ly/z971iy' where id = '312a4cd8-2d77-4984-a786-3c16f6d4993e'
update resourcevideo set shortUrl = 'http://bit.ly/AaGIBT' where id = '251b701d-d504-4989-b153-d1c2a4214580'
go
update resourcevideo
set transcript = '<p>'+replace(cast(transcript as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
go

