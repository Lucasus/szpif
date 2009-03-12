Use szpifDatabase

delete from [Permissions];
delete from [Employees];
DBCC CHECKIDENT (Employees, RESEED, -1);
DBCC CHECKIDENT (Permissions, RESEED, -1);

INSERT INTO [Employees]  VALUES ('lukasz', 'Master', 'Lukasz Wiatrak', 'Boss');
INSERT INTO [Employees]  VALUES ('Jan', 'Kowalski', 'Juuuuuurek', 'Project Manager');
INSERT INTO [Employees]  VALUES ('Moose', 'mus123', 'Losiek Loskowski', 'Prze³o¿ony');
INSERT INTO [Permissions] VALUES (0,'Boss');
INSERT INTO [Permissions] VALUES (0,'Administrator');
INSERT INTO [Permissions] VALUES (1,'PM');
INSERT INTO [Permissions] VALUES (2,'PM');
INSERT INTO [Permissions] VALUES (2,'Prze³o¿ony');

Select * from Employees 
Select * from Permissions