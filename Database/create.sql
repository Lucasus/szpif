Use szpifDatabase

delete from [Permissions];
GO
delete from [Employees];
GO
delete from [Credentials];
GO
DROP TABLE [Permissions];
GO
DROP TABLE [Employees];
GO
DROP TABLE [Credentials];

GO
CREATE TABLE [Credentials]
(
		[Id] [int] IDENTITY (1,1) NOT NULL PRIMARY KEY,
		[Name] [nvarchar] (40) NOT NULL,
		[EMail] [nvarchar] (40) NOT NULL
);
	
CREATE TABLE [Employees]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[CredentialsId] [int] NOT NULL REFERENCES [Credentials] ([Id]),
		[Login] [nvarchar] (40) NOT NULL,
		[Password] [nvarchar] (40) NOT NULL,
);

CREATE TABLE [Permissions]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]), 
		[Permission] [nvarchar] (40) NOT NULL
);

--DBCC CHECKIDENT (Employees, RESEED, -1);
--DBCC CHECKIDENT (Permissions, RESEED, -1);

Select * from Employees 
Select * from Permissions
Select * from Credentials

--exec checkPermissions @Login='Lukasz',@Password='Master'
--exec checkPermissions @Login='Jan',@Password='Kowalski'



