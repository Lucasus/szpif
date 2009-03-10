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
		[Rank] [nvarchar] (40) NOT NULL
);

CREATE TABLE [Permissions]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[EmployeeId] [int] NOT NULL REFERENCES [Employees] ([Id]), 
		[Permission] [nvarchar] (40) NOT NULL
);

DBCC CHECKIDENT (Employees, RESEED, 0);
INSERT INTO [Employees]  VALUES ('lukasz', 'Master', 'Lukasz Wiatrak', 'Boss');
INSERT INTO [Employees]  VALUES ('Jan', 'Kowalski', 'Juuuuuurek', 'Pomywacz');
INSERT INTO [Permissions] VALUES (0,'Boss');
INSERT INTO [Permissions] VALUES (0,'Administrator');
INSERT INTO [Permissions] VALUES (1,'Project Manager');

DROP PROCEDURE checkPermissions
GO
CREATE PROCEDURE checkPermissions
		@Login nvarchar(40),
		@Password nvarchar(40)
AS
	select Permission from [Employees] em
		inner join [Permissions]perm on em.Id = perm.EmployeeId
		where Login = @Login AND Password = @Password
GO


Select * from Employees 
Select * from Permissions
--exec checkPermissions @Login='Lukasz',@Password='Master'
--exec checkPermissions @Login='Jan',@Password='Kowalski'


