insert into resourceAnsweredQuestion
values ('69655F8A-9DC5-4298-A955-1BB43E20FED1',
'10E4EE17-7C46-4301-A7FB-9630899706A1',
'What are the most important aspects to writing an Australian Cover Letter?',
'Preparing a highly targeted and personalised cover letter and you are already on your way to a brand new job. Obviously, you will need a professionally written resume also! By impressing the reader (hiring professional) and they will enthusiastically move onto your resume. Disappoint the reader and your resume will be deleted. 

Will a perfectly written cover letter ensure that you get the job? Of course not. However, a poorly written cover letter will guarantee that your application will not get the attention that is needed to be one of the top candidates. In the current job market there are three areas of your cover letter that you need to pay special attention to:
 
Target the employer’s needs:

Too many times, we write our cover letter and resume from our point of view. From the perspective of the hiring manager they want to know that you have the skills to do the job you are applying for. If the employer is looking for a candidate who is going to need to travel and spend time outside of the office then you need to emphasise that travelling is something you are willing to do (and enjoy). If you do not feel that the job is right for you, then the easy solution is not to apply for the job. However, if you do decide to apply for a certain role then target the needs of the employer and the skills that they require from the perfect candidate.
 
Don’t be afraid to emphasise your previous achievements: 

When applying for a job you need to prove that you are the best candidate. The only way to do this, is by emphasising your achievements and all those skills that make you both unique and special. Try to establish yourself as an expert. Remember that in order to stand out, you need to be in the top 5-10% of all the candidates applying for the role.  While no one likes arrogance, employers DO want to see examples of your achievements that would make you the right person for the job.
 
Provide examples how you will add value to the organisation:

If you don’t believe that you have the skills to add value to the particular organisation then why is the hiring manager going to hire you?  It is not enough anymore just to present your skills and achievements but you need to prove to the reader that you are capable of adding value to the role and to the whole organisation. Providing examples of the added value expertise that you can offer should be highlighted in your cover letter to help differentiate your application as compared to others.',
getdate(),
null)
go

update resourceAnsweredQuestion
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = '69655F8A-9DC5-4298-A955-1BB43E20FED1'
go

update resourceAnsweredQuestion
set shortUrl = 'http://bit.ly/OSmfyv'
where id = '69655F8A-9DC5-4298-A955-1BB43E20FED1'
go
