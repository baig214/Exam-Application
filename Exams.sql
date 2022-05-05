create database exams
use exams

create table roles(roleid int constraint ridpk primary key ,
Role varchar(50) constraint uqrole unique constraint nnrole not null)   
select * from roles
insert into roles values(1,'Admin')
insert into roles values(2,'Staff')
insert into roles values(3,'Student')
--2--
create table users(userid int constraint uidPk primary key,
name varchar(100) constraint uqName unique,
email varchar(100) constraint nnemail not null constraint uqemail unique,
mobile bigint constraint nnmobile not null constraint uqmobile unique,
password varchar(100) constraint nnpass not null ,
Role int constraint rolefk foreign key references roles(roleid))
select * from users
insert into users values(1,'Mohammed','mohammed@gmail.com',9876543210,'mohammed1',1)
insert into users values(2,'Nateeqa','nateeqa@gmail.com',9876543211,'nateeqa1',1)
insert into users values(3,'Shamail','shamail@gmail.com',9876543212,'shamail1',2)
insert into users values(4,'Ali','ali@gmail.com',9876543213,'syedali1',2)





select userid,name,password from users where email='mohammed@gmail.com'
--3------------------------------------------------------------------------------
create table subjects(subid int constraint subidpk primary key,
subjectname varchar(100) constraint nnsname not null constraint uqsname unique )
select * from subjects


insert into subjects(subid,subjectname) values (101,'Maths'),(102,'English'),(103,'Science'),(104,'Computers');

delete from subjects where subid=106
select name from users where role=

--------------------------------------------------
create table StaffSubjectAllocation (staffId int constraint fkstaffid11 foreign key references Users (userid),
                                    subjectId int constraint fksubid11 foreign key references Subjects (subid),
AllottedBy int constraint fkuserid11 foreign key references Users (UserId),
AllotedOn date constraint defdate11 Default getdate()
primary key (Staffid, SubjectId))

select * from StaffSubjectAllocation
-----------------------------------------------------------------------------------------------

create table QuestionBank (QuestionId int identity(301,1) constraint pkqid primary key,
                         subjectId int constraint fksubid1 foreign key references Subjects (subid),
        staffId int constraint fkstaffid1 foreign key references Users (userid),
        Question varchar(max) not null,
OptionA varchar(max) not null,
OptionB varchar(max) not null,
OptionC varchar(max) not null,
OptionD varchar(max) not null,
Answer char constraint ckans Check (Answer in ('A','B','C','D')),
CreatedOn date constraint defdate1 default getdate())

select * from QuestionBank
delete from QuestionBank
-------------------------------------------------------------------------------------------------------

Create table Exams (ExamID int constraint pkexamid primary key,
                   StudentId int constraint fkstudentid foreign key references Users (userid),
  subjectId int constraint fksubid2 foreign key references Subjects (subid),
  [Date of Examination] date constraint defdate2 default getdate())

select * from Exams

	insert into Exams (ExamID,StudentId,subjectId) values ( 1,8,104);

	delete from Exams
----------------------------------------------------------------------------------------------------

create table Answers (ExamId int constraint fkexamid foreign key references Exams (ExamID),
                      QuestionID int  constraint fkqid  foreign key references QuestionBank (QuestionId),
 Answer char(2) constraint ckans1 Check (Answer in ('A','B','C','D','NA')))

 insert into Answers Values(1,301,'B')

 select * from Answers
 delete from Answers
 --------------------------------------------------------------------------------------------------

 select password from users where userid=1

 select password from users where userid=1 and password='mohammed1'

 select max(ExamId)+1 from Exams

 select isNull(max(ExamId),1) from Exams

 select e.ExamID as [Examid],e.QuestionID,e.Answer from Answers e inner join QuestionBank q on q.Answer=e.Answer where e.ExamId=67 and q.QuestionId=e.QuestionID