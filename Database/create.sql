Use szpifDatabase

delete from [Employees];
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
		[Permission] [nvarchar] (40) NOT NULL
);

DBCC CHECKIDENT (Employees, RESEED, 0);
--INSERT INTO [Employees]  VALUES ('lukasz', 'Master', 'Lukasz Wiatrak', 'Boss');
--INSERT INTO [Employees]  VALUES ('Jan', 'Kowalski', 'Juuuuuurek', 'Pomywacz');

Select * from Employees 
