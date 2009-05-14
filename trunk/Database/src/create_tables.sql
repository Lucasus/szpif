Use szpifDatabase
PRINT 'CREATING TABLES...'

IF Object_ID('Roles','U') IS NOT NULL 
BEGIN
	delete from [Roles];
	DROP TABLE [Roles];
END

IF Object_ID('WeeklyAccountTasks','U') IS NOT NULL 
BEGIN
	delete from [WeeklyAccountTasks];
	DROP TABLE [WeeklyAccountTasks];
END

IF Object_ID('WeeklyAccounts','U') IS NOT NULL 
BEGIN
	delete from [WeeklyAccounts];
	DROP TABLE [WeeklyAccounts];
END


IF Object_ID('Tasks','U') IS NOT NULL 
BEGIN
	delete from [Tasks];
	DROP TABLE [Tasks];
END

IF Object_ID('TaskStatus','U') IS NOT NULL 
BEGIN
	delete from [TaskStatus];
	DROP TABLE [TaskStatus];
END

IF Object_ID('Projects','U') IS NOT NULL 
BEGIN
	delete from [Projects];
	DROP TABLE [Projects];
END

IF Object_ID('ProjectStatus','U') IS NOT NULL 
BEGIN
	delete from [ProjectStatus];
	DROP TABLE [ProjectStatus];
END

IF Object_ID('Orders','U') IS NOT NULL 
BEGIN
	delete from [Orders];
	DROP TABLE [Orders];
END

IF Object_ID('OrderStatus','U') IS NOT NULL 
BEGIN
	delete from [OrderStatus];
	DROP TABLE [OrderStatus];
END


IF Object_ID('Clients','U') IS NOT NULL 
BEGIN
	delete from [Clients];
	DROP TABLE [Clients];
END


IF Object_ID('Employees','U') IS NOT NULL 
BEGIN
	delete from [Employees];
	DROP TABLE [Employees];
END

IF Object_ID('Credentials','U') IS NOT NULL 
BEGIN
		delete from [Credentials];
		DROP TABLE [Credentials];
END

IF Object_ID('Help','U') IS NOT NULL 
BEGIN
		delete from [Help];
		DROP TABLE [Help];
END

IF Object_ID('UserTypes','U') IS NOT NULL 
BEGIN
		delete from [UserTypes];
		DROP TABLE [UserTypes];
END

IF Object_ID('ColumnsToTypes','U') IS NOT NULL 
BEGIN
		delete from [ColumnsToTypes];
		DROP TABLE [ColumnsToTypes];
END

GO

-- DANE OSOBOWE ---------------------------------------------------------------------
CREATE TABLE [dbo].[Credentials](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FirstName] [nvarchar](40)  NOT NULL ,
	[SecondName] [nvarchar](40)  NULL,
	[LastName] [nvarchar](40) NOT NULL,
	[EMail] [nvarchar](40) NOT NULL,
	[Phone] [char](20) NULL,
	[Street] [nvarchar](40) NOT NULL,
	[HouseNr] [char](7) NOT NULL,
	[FlatNr] [char](7) NULL,
	[City] [nvarchar](40) NOT NULL,
	[PostalCode] [nvarchar](6) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Pesel] [char](11) NULL,
	[Nip] [char](20) NOT NULL,

	CONSTRAINT [CK_Cred_City] CHECK  (dbo.RegExMatch(City, '\b[A-Z][a-z]+\b')=1),
	CONSTRAINT [CK_Cred_Country] CHECK  (dbo.RegExMatch(Country, '\b[A-Z][a-z]+( [A-Z][a-z-]+)*\b')=1),
	CONSTRAINT [CK_Cred_Email] CHECK  (dbo.RegExMatch(Email, '\b[a-zA-Z_.]+@[a-z.]+[a-z]\b')=1),
	CONSTRAINT [CK_Cred_FirstName] CHECK  (dbo.RegExMatch(FirstName, '\b[A-Z][a-z]+\b')=1),
	CONSTRAINT [CK_Cred_FlatNr] CHECK  (dbo.RegExMatch (FlatNr, '\b[0-9]+[A-Z]*\b')=1),
	CONSTRAINT [CK_Cred_HouseNr] CHECK  (dbo.RegExMatch(HouseNr, '\b[1-9][0-9]*[A-Z]*\b')=1),
	CONSTRAINT [CK_Cred_LastName] CHECK  (dbo.RegExMatch(LastName, '\b[A-Z][a-z]+\b')=1),
	CONSTRAINT [CK_Cred_Nip] CHECK  (dbo.RegExMatch(Nip, '\b[0-9]+\b')=1),
	CONSTRAINT [CK_Cred_Pesel] CHECK  (dbo.RegExMatch(Pesel, '\b[0-9]{11}\b')=1),
	CONSTRAINT [CK_Cred_Phone] CHECK  (dbo.RegExMatch(Phone, '\b[0-9]+\b')=1),
	CONSTRAINT [CK_Cred_PostalCode] CHECK  (dbo.RegExMatch(PostalCode, '\b[0-9][0-9]-[0-9][0-9][0-9]\b')=1),
	CONSTRAINT [CK_Cred_SecondName] CHECK  (dbo.RegExMatch(SecondName,  '\b[A-Z][a-z]+\b')=1),
	CONSTRAINT [CK_Cred_Street] CHECK  (dbo.RegExMatch(Street, '\b[A-Z][a-zA-Z -]*[a-z]\b')=1),
);


-- PRACOWNICY ---------------------------------------------------------------------
CREATE TABLE [Employees]
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[CredentialsId] [int] NOT NULL REFERENCES [Credentials] ([Id]) ON DELETE NO ACTION ON UPDATE CASCADE,
	[SuperiorId] [int] NULL REFERENCES [Employees] ([Id]),
	[Login] [nvarchar] (40) NOT NULL UNIQUE,
	[Password] [nvarchar] (40) NOT NULL,
	[HoursNr] [int] NOT NULL,
	[RatePerHour] [money] NOT NULL, 

	CONSTRAINT [CK_Empl_Login] CHECK  (dbo.RegExMatch([Login], '\b[A-Za-z0-9]{6}[A-Za-z0-9]*\b')=1),
	CONSTRAINT [CK_Empl_Password] CHECK  (dbo.RegExMatch(Password, '\b[A-Za-z0-9]{6}[A-Za-z0-9]*\b')=1),
	CONSTRAINT [CK_Empl_HourNr] CHECK  (HoursNr > 0 AND HoursNr < 43),
	CONSTRAINT [CK_Empl_RatePerHour] CHECK  (RatePerHour > 0)
);


-- KLIENCI ---------------------------------------------------------------------
CREATE TABLE [Clients]
(
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CredentialsId] [int] NOT NULL REFERENCES [Credentials] ([Id]) ON UPDATE CASCADE,
	[CompanyName] [nvarchar] (100),
	CONSTRAINT [CK_Clients_CompanyName] CHECK  (dbo.RegExMatch([CompanyName], '\b[A-Z][a-z]+( [A-Z][a-z-]+)*\b')=1)
);


-- ZLECENIA ---------------------------------------------------------------------
CREATE TABLE [OrderStatus] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[EndDate] [datetime] NULL,
	[UsedHours] [int] NOT NULL,
	[UsedBudget] [int] NOT NULL,
	[Status] [nvarchar] (100) NOT NULL, 
	CONSTRAINT [CK_OrderSt_UsedHours] CHECK  (UsedHours > 0),
	CONSTRAINT [CK_OrderSt_UsedBudget] CHECK  (UsedBudget > 0)
);

CREATE TABLE [Orders] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ClientId] [int] NOT NULL REFERENCES [Clients] ([Id]) ON UPDATE CASCADE,
	[OrderStatusId] [int] NOT NULL REFERENCES [OrderStatus] ([Id]) ON UPDATE CASCADE,
	[OrderName] [nvarchar] (40) NOT NULL,
	[Decription] [nvarchar] (300) NULL,
	[MaxHours] [int] NOT NULL,
	[MaxBudget] [money] NOT NULL,
	[Price] [money] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[ExpectedEndDate] [datetime] NOT NULL, 
	CONSTRAINT [CK_Order_Name] CHECK  (dbo.RegExMatch([OrderName], '\b[A-Za-z0-9]{3}[A-Za-z0-9]*\b')=1),
	CONSTRAINT [CK_Task_MaxHours] CHECK  (MaxHours > 0),
	CONSTRAINT [CK_Task_Price] CHECK  (Price > 0),
	CONSTRAINT [CK_Order_Dates] CHECK  (ExpectedEndDate >= StartDate)
);

-- PROJEKTY ---------------------------------------------------------------------
CREATE TABLE [ProjectStatus] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[EndDate] [datetime] NULL,
	[UsedHours] [int] NOT NULL,
	[UsedBudget] [int] NOT NULL,
	[Status] [nvarchar] (100) NOT NULL, 
	CONSTRAINT [CK_ProjectSt_UsedHours] CHECK  (UsedHours > 0),
	CONSTRAINT [CK_ProjectSt_UsedBudget] CHECK  (UsedBudget > 0)
	
);

CREATE TABLE [Projects] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ManagerId] [int] NULL REFERENCES [Employees] ([Id]), 
	[OrderId] [int] NOT NULL REFERENCES [Orders] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
	[ProjectStatusId] [int] NOT NULL REFERENCES [ProjectStatus] ([Id]) ON UPDATE CASCADE,
	[ProjectName] [nvarchar] (40) NOT NULL,
	[MaxHours] [int] NOT NULL,
	[MaxBudget] [money] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[ExpectedEndDate] [datetime] NOT NULL, 
	CONSTRAINT [CK_Project_Name] CHECK  (dbo.RegExMatch([ProjectName], '\b[A-Za-z0-9]+\b')=1),
	CONSTRAINT [CK_Project_MaxHours] CHECK  (MaxHours > 0),
	CONSTRAINT [CK_Project_MaxBudget] CHECK  (MaxBudget > 0),
	CONSTRAINT [CK_Project_Dates] CHECK  (ExpectedEndDate >= StartDate)
);


-- ZADANIA ---------------------------------------------------------------------
CREATE TABLE [TaskStatus] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[EndDate] [datetime] NULL,
	[UsedHours] [int] NOT NULL,
	[BonusGiven] [bit] NOT NULL,
	[Status] [nvarchar] (100) NOT NULL, 
	CONSTRAINT [CK_TaskSt_UsedHours] CHECK  (UsedHours > 0)
);


CREATE TABLE [Tasks] 
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[EmployeeId] [int] NULL REFERENCES [Employees] ([Id]), --TODO
	[ProjectId] [int] NOT NULL REFERENCES [Projects] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
	[TaskStatusId] [int] NOT NULL REFERENCES [TaskStatus] ([Id]) ON UPDATE CASCADE,
	[TaskName] [nvarchar] (40) NOT NULL,
	[MaxHours] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[ExpectedEndDate] [datetime] NOT NULL, 
	[Bonus] [money] NOT NULL DEFAULT 0, 
	CONSTRAINT [CK_Tasks_Name] CHECK  (dbo.RegExMatch([TaskName], '\b[A-Za-z0-9]+\b')=1),
	CONSTRAINT [CK_Tasks_MaxHours] CHECK  (MaxHours > 0),
	CONSTRAINT [CK_Tasks_Bonus] CHECK  (Bonus > 0),
	CONSTRAINT [CK_Tasks_Dates] CHECK  (ExpectedEndDate >= StartDate)
);

-- ROZLICZENIA TYGODNIOWE ---------------------------------------------------------------------
CREATE TABLE [WeeklyAccounts]
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
	[StartDate] [datetime] NOT NULL,
	[ReportedHours] [int] NOT NULL,
	[Status] [int] NOT NULL,
	CONSTRAINT [CK_WeeklyAccounts_ReportedHours] CHECK  (ReportedHours > 0)

);

CREATE TABLE [WeeklyAccountTasks]
(
	[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[WeeklyAccountId] [int] NOT NULL REFERENCES [WeeklyAccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
	[TaskId] [int] NOT NULL REFERENCES [Tasks] ([Id]),
	[WorkedHours] [int] NOT NULL,
	[Comment] [varchar] (1000) NULL, 
	CONSTRAINT [CK_WeeklyAccountsTasks_ReportedHours] CHECK  (WorkedHours > 0)

);

-- UPRAWNIENIA ---------------------------------------------------------------------
CREATE TABLE [Roles]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]), 
		[Role] [nvarchar] (40) NOT NULL
		
);

CREATE TABLE [RoleNames]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[RoleName] [nvarchar] (40) NOT NULL
);
--pomocnicze tabele techniczne zawierajace metadane-------

CREATE TABLE [Help]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[Message] [nvarchar] (40) NOT NULL
);

CREATE TABLE [UserTypes]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[Name] [nvarchar] (40) NOT NULL,
		[TypeSchema] [nvarchar] (1000) NOT NULL
);

CREATE TABLE [ColumnsToTypes]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[ViewName] [nvarchar] (40) NOT NULL,
		[ColumnName] [nvarchar] (40) NOT NULL,
		[TypeName] [nvarchar] (40) NOT NULL,
		[DynamicInformation] [nvarchar] (200)
);

GO
--creating types
INSERT INTO [UserTypes] VALUES ('CheckedListBox',
 '<CheckedListBox Name="ColumnName">
  <Item Name="W³aœciciel" Value="0 or 1" />
  <Item Name="Project Manager" Value="0 or 1" />
  <Item Name="Prze³o¿ony" Value="0 or 1" />
  <Item Name="Pracownik" Value="0 or 1" />
  <Item Name="Opiekun handlowy" Value="0 or 1" />
</CheckedListBox>');

INSERT INTO [UserTypes] VALUES ('Link','<Link Name="ColumnName" ViewName="view name" Id="Id rekordu do ktorego sie odwolujemy"
Text="Tekst wyswietlany na gridzie" />');



GO
