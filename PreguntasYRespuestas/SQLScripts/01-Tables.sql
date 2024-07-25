CREATE TABLE dbo.Question(
	QuestionId int IDENTITY(1,1) NOT NULL,
	Title nvarchar(100) NOT NULL,
	Content nvarchar(max) NOT NULL,
	UserId nvarchar(150) NOT NULL,
	UserName nvarchar(150) NOT NULL,
	Created datetime2(7) NOT NULL,
 CONSTRAINT PK_Question PRIMARY KEY CLUSTERED 
(
	QuestionId ASC
)
) 
GO

CREATE TABLE dbo.Answer(
	AnswerId int IDENTITY(1,1) NOT NULL,
	QuestionId int NOT NULL,
	Content nvarchar(max) NOT NULL,
	UserId nvarchar(150) NOT NULL,
	UserName nvarchar(150) NOT NULL,
	Created datetime2(7) NOT NULL,
 CONSTRAINT PK_Answer PRIMARY KEY CLUSTERED 
(
	AnswerId ASC
)
) 
GO
ALTER TABLE dbo.Answer  WITH CHECK ADD  CONSTRAINT FK_Answer_Question FOREIGN KEY(QuestionId)
REFERENCES dbo.Question (QuestionId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE dbo.Answer CHECK CONSTRAINT FK_Answer_Question
GO

SET IDENTITY_INSERT dbo.Question ON 
GO
INSERT INTO dbo.Question(QuestionId, Title, Content, UserId, UserName, Created)
VALUES(1, 'Question 1?', 
		'ed finibus, enim vel dapibus rutrum, leo dolor lobortis turpis, ac pulvinar neque nulla eget augue.',
		'1',
		'test@test.com',
		'2024-07-25 17:32')

INSERT INTO dbo.Question(QuestionId, Title, Content, UserId, UserName, Created)
VALUES(2, 'Question 2?', 
		'Phasellus placerat consectetur augue, ac tempor ipsum scelerisque in. Suspendisse ac rhoncus magna.',
		'2',
		'test2@test.com',
		'2024-07-25 17:48')
GO
SET IDENTITY_INSERT dbo.Question OFF
GO

SET IDENTITY_INSERT dbo.Answer ON 
GO
INSERT INTO dbo.Answer(AnswerId, QuestionId, Content, UserId, UserName, Created)
VALUES(1, 1, 'Sed porttitor tincidunt sem, at consectetur mi venenatis fringilla.', '2', 'test3@test.com', '2019-05-18 14:40')

INSERT INTO dbo.Answer(AnswerId, QuestionId, Content, UserId, UserName, Created)
VALUES(2, 1, 'Nam magna risus, elementum vel elit at', '3', 'test4@test.com', '2019-05-18 16:18')
GO
SET IDENTITY_INSERT dbo.Answer OFF 
GO