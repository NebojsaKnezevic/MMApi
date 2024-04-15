CREATE PROCEDURE [dbo].[LoadTestData]
AS
	
  Insert into QuestionGroup (Name, IsActive, CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  values ('Medical',1,'2024-04-08 10:00:00.000',1,0,100,1,1)

  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Cardiovascular',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Respiratory',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Endocrine / Renal',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Other Medical History',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Allergies',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Medications',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Functional capacity',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  Insert into [QuestionGroup] (Name,ParentId,IsActive,CreatedOn,InLevel, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale)
  select 'Anaesthetic',Id,1,'2024-04-08 10:00:00.000',2,0,100,1,1
  from [QuestionGroup] where Name = 'Medical'
  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (1,'Angina (chest pain when exercising or at night) or severe pain across the front of your chest lasting 30 minutes or more?',
  0,2,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (2,'A heart attack?',
  1,2,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (3,'A pacemaker, an irregular heartbeat or abnormal pulse, or heart-block?',
  0,2,0,100,1,1,1,'2024-04-08 10:00:00.000')

  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (4,'Problematic wheezing, bronchitis, asthma, COPD, emphysema, shortness of breath?',
  0,3,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (5,'Any history of obstructive sleep apnoea? ',
  0,3,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (6,'Any significant infections of the lung e.g. tuberculosis?',
  0,3,0,100,1,1,1,'2024-04-08 10:00:00.000')

  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (7,'Do you have diabetes?',
  0,4,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (8,'Any kidney disease or significant urinary or bladder problems?',
  0,4,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (9,'Any thyroid disease or thyroid problem? ',
  0,4,0,100,1,1,1,'2024-04-08 10:00:00.000')

  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (10,'An organ transplantation?',
  0,5,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (11,'Epilepsy or other significant neurological conditions (such as Parkinson’s disease)?',
  0,5,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (12,'A formal diagnosis of dementia or mental illness?',
  0,5,0,100,1,1,1,'2024-04-08 10:00:00.000')

  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (13,'Have you ever had an anaphylactic reaction?',
  0,6,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (14,'Do you have any allergies? Please list any allergies you have, including medicines, metal, food, latex and plasters',
  1,6,0,100,1,1,1,'2024-04-08 10:00:00.000')


  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (15,'More than 3 prescribed medicines?',
  0,7,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (16,'Oral diabetic medicines or insulin?',
  0,7,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (17,'Blood thinning medicine e.g. warfarin, clopidogrel, ticagrelor, apixaban, rivaroxaban, heparin, aspirin?',
  0,7,0,100,1,1,1,'2024-04-08 10:00:00.000')


  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (18,'Using the scale below, please tick by the number that most closely matches your exercise capability:',
  0,8,0,100,1,1,1,'2024-04-08 10:00:00.000')

  
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (19,'Have you had any surgery / General anaesthetic in the past year?',
  0,9,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (20,'Have you or any of your blood relatives had any significant problems with an anaesthetic?',
  0,9,0,100,1,1,1,'2024-04-08 10:00:00.000')
  insert into Question (OrderNo, Text, AdditionalComment, QuestionGroupId, ApplicableAgeLow, ApplicableAgeHigh, ApplicableMale, ApplicableFemale, IsActive, CreatedOn)
  values (21,'Do you have any restricted neck movement, restricted jaw movement, limited mouth opening, or have you had a previous tracheotomy?',
  0,9,0,100,1,1,1,'2024-04-08 10:00:00.000')

  

  --select * from [Question]

  select 'Yes' as ans into #tempYesNo
  insert into #tempYesNo (ans) values ('No')

  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  select Id, case when ans = 'Yes' then 2 else 1 end, ans, 1, '2024-04-08 10:00:00.000' from Question, #tempYesNo
  where Id <> 18
  order by Id, ans

  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Walking around the house',1,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Domestic activities',2,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Walk 200 yards without stopping',3,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Gardening or golf (carrying clubs)',4,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Cycling',5,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Swimming',6,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Easily climb 2 flights of stairs',7,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Brisk swimming',8,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Jogging',9,'2024-04-08 10:00:00.000')
  insert into [Answer] (QuestionId, OrderNo, Text, IsActive, CreatedOn)
  values (18,1,'Football / squash (competitive sports)',10,'2024-04-08 10:00:00.000')
RETURN 0
