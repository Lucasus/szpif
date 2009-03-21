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

GO
EXEC sp_droprolemember 'ProjectManager', 'Moose'
DROP ROLE ProjectManager;
CREATE ROLE ProjectManager;
GRANT EXECUTE ON OBJECT::changePassword TO ProjectManager;
GO
DROP ROLE Employee;
CREATE ROLE Employee;
GO

DROP USER Moose;
CREATE USER Moose WITHOUT LOGIN;
EXEC sp_addrolemember 'ProjectManager', 'Moose' 
GO

DROP USER Lucas;
CREATE USER Lucas WITHOUT LOGIN;
GO
--DBCC CHECKIDENT (Employees, RESEED, -1);
--DBCC CHECKIDENT (Permissions, RESEED, -1);

Select * from Employees 
Select * from Permissions
Select * from Credentials
GO

EXECUTE AS USER = 'Moose';
EXECUTE changePassword 'lucas', 'master', 'blaster';
REVERT;

