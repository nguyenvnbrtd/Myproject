Create database ExamManager
go

use ExamManager


create table Users(
	Id int identity(1,1) primary key,
	UserName nvarchar(max),
	PassWord nvarchar(max),
	Permission int,
	Name nvarchar(max),
	Gender nvarchar(10),
	BirthDay date,
	PhoneNumber	nvarchar(20),
	Job nvarchar(max),
	MoreInfo nvarchar(max)
)

create table Subjects(
	Id int identity(1,1) primary key,
	SubjectName nvarchar(max),
)

create table QuizList(
	Id int identity(1,1) primary key,
	SubjectsId int,
	NameList nvarchar(max)

	foreign key (SubjectsId) references Subjects(Id)
)


create table Quiz(
	Id int identity(1,1) primary key,
	QuizListId int not null,
	Content nvarchar(max)
	
	foreign key(QuizListId) references QuizList(Id)
)

create table Answer(
	Id int identity(1,1) primary key,
	QuizId int not null,
	Content nvarchar(max),
	IsCorrect int,

	foreign key(QuizId) references Quiz(Id),
)

create table ExamInfo(
	Id int identity(1,1) primary key,
	QuizListId int not null,
	DayOpen date,
	DayEnd date,
	Duration int,
	Ispublic int,

	foreign key(QuizListId) references QuizList(Id),
)

create table UserExam(
	UserId int not null,
	ExamInfoId int not null,
	TimeEntry int,
	ExamPoint int,


	foreign key(UserId) references Users(Id),
	foreign key(ExamInfoId) references ExamInfo(Id),
	primary key(UserId,ExamInfoId),
)

insert into Users values('admin','admin',1,N'Lean',N'','1/1/1999',N'',N'',N'')
insert into Users values('sinhvien1','sinhvien1',1,N'DDD',N'','1/1/1999',N'',N'',N'')

alter table Subjects add Descriptions nvarchar(max)
alter table UserExam drop Column TimeEntry
alter table UserExam add TimeEntry Datetime