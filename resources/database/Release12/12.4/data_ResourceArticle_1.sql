insert into resourcearticle
values ('D02251DD-CC6F-4118-9515-77275A52F1C3',
'5E719F17-4728-4708-A6FC-F78137DA1D04',
'How psychometric testing helps employers make hiring decisions',
'Every one of us is different. We have different strengths, different weaknesses; some of us are more team orientated, some of us prefer to take more of a leadership role; some of us are risk takers, others more cautious, and so on. 
Understanding what makes a person “tick” by applying psychometric testing, can go a long way towards determining how well a potential candidate might fit within an organisation, and how suitable they may be for a specific position within that organisation.
There are many types of psychometric tests that can be done. The majority involve both cognitive and personality assessments, and can be tailored specifically to the particular position. It is also important to note that personality tests do not consist of questions which have correct answers assigned to them. What they show are personality traits that are designed to provide a deeper understanding of the candidate, as opposed to a “gut feel” that may be formed in an interview process, or a biased assessment from a previous employer.  Implementing both cognitive and personality testing complements and increases the validity of the assessment process.
At the end of the day, there are only three questions the employer really has to answer during the selection process:
•	First, do you have the right skills and experience?
•	Second, do you have the required enthusiasm and motivation?
•	Finally, are you going to fit in, in terms of your personality, attitude and general work style?
If the answer to any one of these questions is “no”, the chances are that person is going to struggle down the line to fulfil their role within an organisation. Psychometric testing is a way of applying a level of objectivity to the process.
While psychometric testing cannot predict, and never has predicted, “performance”, it is a very useful tool that more and more companies are using. In America, for example, psychometric testing is now used by over 80% of the Fortune 500 companies in the USA and by over 75% of the Times Top 100 companies in the UK.
Most employers will probably not make a selection decision based solely on psychometric testing alone. However, what psychometric testing can reveal are core competencies of potential candidates that reduce the margin of error in the selection process.',
0,
getdate(),
null)
go

update resourcearticle
set text = '<p>'+replace(cast(text as varchar(8000)), char(13) + char(10),'</p>'+char(13) + char(10)+'<p>') +'</p>'
where id = 'D02251DD-CC6F-4118-9515-77275A52F1C3'
go

update resourcearticle
set shortUrl = 'http://bit.ly/LEcir6'
where id = 'D02251DD-CC6F-4118-9515-77275A52F1C3'
go