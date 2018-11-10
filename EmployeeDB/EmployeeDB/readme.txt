Создание таблиц базы данных:

Подразделения:

CREATE TABLE [dbo].[Departments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL
)

Люди:

CREATE TABLE [dbo].[Peoples]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NCHAR(50) NOT NULL, 
    [LastName] NCHAR(50) NOT NULL, 
    [Phone] NCHAR(10) NULL, 
    [Adress] NCHAR(50) NULL
)

Организации:

CREATE TABLE [dbo].[Organizations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NULL, 
    [Adress] NCHAR(50) NULL, 
    [Phone] NCHAR(10) NULL, 
    [Email] NCHAR(50) NULL
)

Сотрудники:

CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [People_Id] INT NOT NULL, 
    [Department_Id] INT NOT NULL, 
    [Salary] MONEY NOT NULL DEFAULT 0
)

CREATE VIEW [dbo].[ViewEmployees]
	AS SELECT 
	Employees.Id as PersID,
	Department_Id as Department,
	Departments.Name,
	People_Id as People_id,
	Peoples.FirstName as FirstName,
	Peoples.LastName as LastName,
	Employees.Organization_Id as OrgId,
	Organizations.Name as Organisation
	FROM [Employees]
	inner join Departments on Employees.Department_Id = Departments.Id
	inner join Peoples on Employees.People_Id = Peoples.Id
	inner join Organizations on Employees.Organization_Id = Organizations.Id


Заполнение данных:

INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 1', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 2', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 3', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 4', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 5', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 6', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 7', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 8', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 9', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 10', 1)
INSERT INTO [dbo].[Departments] ( [Name], [Organization_Id]) VALUES (N'Подразделение 11', 1)


INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 1', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 1', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 2', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 2', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 3', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 3', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 4', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 4', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 5', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 5', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 6', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 6', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 7', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 7', N'333-33-33 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Иван', N'Иванов 8', N'555-55-55 ', N'Москва')
INSERT INTO [dbo].[Peoples] ([FirstName], [LastName], [Phone], [Adress]) VALUES (N'Петр', N'Петров 8', N'333-33-33 ', N'Москва')


INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 2,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES (4,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 5,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 6,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 7,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES (8,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 9,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 3,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 1,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 3,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 2,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 4,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 5,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 6,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 7,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 8,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 9,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 1,3)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 2,2)
INSERT INTO [dbo].[ViewEmployees] ([Department], [People_id]) VALUES ( 3,3)
