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

DBCC CHECKIDENT (Employees, RESEED, -1);
DBCC CHECKIDENT (Permissions, RESEED, -1);



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

DROP PROCEDURE changePassword
GO
CREATE PROCEDURE changePassword
		@Login nvarchar(40),
		@currentPassword nvarchar(40),
		@Password nvarchar(40)
AS
	update Employees 
	set Password=@Password
	where Login=@Login AND Password=@currentPassword
GO

Select * from Employees 
Select * from Permissions
--exec checkPermissions @Login='Lukasz',@Password='Master'
--exec checkPermissions @Login='Jan',@Password='Kowalski'



