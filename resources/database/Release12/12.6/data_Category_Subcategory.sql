insert into faqcategory (displayName, id, displayOrder) values ('Employer','556AA450-EB4A-4664-A3DC-400C2232739D',10)
insert into faqcategory (displayName, id, displayOrder) values ('Member','F0D71552-14BC-40B5-A237-A1A8BE2F2C98',20)
insert into faqcategory (displayName, id, displayOrder) values ('Security','9D961194-16DF-463E-8B74-2C744F91DA6C',30)


insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','About LinkMe','E40AC11D-BF4D-4F7B-AC8C-E65FE67F0EC1')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','Setting up your profile','09D11385-0213-4157-A5A9-1B2A74E6887E')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','Security, spam and access','D35C5AF3-7CAB-4D5E-855E-2EAD93ABBA8B')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','Email alerts and newsletters','197ADF9E-50BB-4D6C-A9A8-C00ED2CFDAFE')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','My LinkMe profile','637DBC94-B73E-47B5-A3FE-7D6C85CB092A')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','Accessing and editing your profile','C519F82C-B870-41E2-97C3-2051F52F4FE8')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','Applying for jobs','4DB01A58-CE5F-4E97-8E05-7CDA6F824E14')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('F0D71552-14BC-40B5-A237-A1A8BE2F2C98','Miscellaneous','40360199-D434-4D73-AE0A-55E2BFD1C0FA')

insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','About LinkMe','71F97D2D-3AB4-4BA7-BAF0-AB548AA35403')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','Searching for candidates','31BC7D9E-F853-4661-ABCA-8DA58776D696')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','Saved searches and email alerts','614DCFD7-60E9-4944-BF4B-880D8FB25AB5')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','Folders and notes','6BA949D8-5CB4-4B03-A736-D07D05FC57EB')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','Job Board','363BBC2F-2E58-4D57-8059-2A5BE74F044C')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','Contacting candidates','11A9E08D-5909-4083-9B08-28ACDC911077')
insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('556AA450-EB4A-4664-A3DC-400C2232739D','Candidate Connect','0A2FBDFD-9231-4C19-A602-DC73C0E68E9B')

insert into faqSubcategory (faqCategoryId,DisplayName,Id) values ('9D961194-16DF-463E-8B74-2C744F91DA6C','Job and employment scams','F1630A91-BACA-4835-A516-90D2001AE986')

UPDATE FAQCATEGORY SET DISPLAYNAME = 'Employers' where ID = '556AA450-EB4A-4664-A3DC-400C2232739D'
UPDATE FAQCATEGORY SET DISPLAYNAME = 'Members' where ID = 'F0D71552-14BC-40B5-A237-A1A8BE2F2C98'
UPDATE FAQCATEGORY SET DISPLAYNAME = 'Online Security' where ID = '9D961194-16DF-463E-8B74-2C744F91DA6C'