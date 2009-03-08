Use szpifDatabase

delete from [Employees];
DROP TABLE [Employees];
	
CREATE TABLE [Employees]
(
		[Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,
		[Login] [nvarchar] (40) NOT NULL,
		[Password] [nvarchar] (40) NOT NULL,
		[Name] [nvarchar] (40) NOT NULL
);

DBCC CHECKIDENT (Employees, RESEED, 0);
INSERT INTO [Employees]  VALUES ('Lucas', 'Master', 'Lukasz Wiatrak');

Select * from [Employees] 