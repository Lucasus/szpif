Use szpifDatabase

delete from [Permissions];
delete from [Employees];
DBCC CHECKIDENT (Employees, RESEED, 0);
DBCC CHECKIDENT (Permissions, RESEED, 0);

INSERT INTO [Employees]  VALUES ('lukasz', 'Master', 'Lukasz Wiatrak', 'Boss');
INSERT INTO [Employees]  VALUES ('Jan', 'Kowalski', 'Juuuuuurek', 'Project Manager');
INSERT INTO [Permissions] VALUES (0,'Boss');
INSERT INTO [Permissions] VALUES (0,'Administrator');
INSERT INTO [Permissions] VALUES (1,'Project Manager');

Select * from Employees 
Select * from Permissions