Use szpifDatabase

delete from [Permissions];
delete from [Employees];
DROP TABLE [Permissions];
DROP TABLE [Employees];
	
CREATE TABLE [Employees]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[Login] [nvarchar] (40) NOT NULL,
		[Password] [nvarchar] (40) NOT NULL,
		[Name] [nvarchar] (40) NOT NULL,
);

CREATE TABLE [Permissions]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]), 
		[Permission] [nvarchar] (40) NOT NULL
);

--DBCC CHECKIDENT (Employees, RESEED, -1);
--DBCC CHECKIDENT (Permissions, RESEED, -1);

DROP VIEW dbo.EmployeeAdministrationView
GO
CREATE VIEW dbo.EmployeeAdministrationView
AS
SELECT DISTINCT em.Id, Login, Name,
           dbo.agregatePermissionsFunction (em.Id) AS Uprawnienia
FROM Employees em
		inner join [Permissions] perm on em.Id = perm.EmployeeId
GO
Select * from Employees 
Select * from Permissions
--exec checkPermissions @Login='Lukasz',@Password='Master'
--exec checkPermissions @Login='Jan',@Password='Kowalski'



