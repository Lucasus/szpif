Use szpifDatabase
PRINT 'GENERATING DATA...'
GO
delete from [Permissions];
delete from [Employees];
delete from [Credentials];
DBCC CHECKIDENT (Employees, RESEED, 0);
DBCC CHECKIDENT (Permissions, RESEED, 0);
DBCC CHECKIDENT (Credentials, RESEED, 0);

INSERT INTO [Credentials] VALUES ('blabla','Prze³o¿ony');
INSERT INTO [Employees]  VALUES (0,'lukasz', 'Master');
INSERT INTO [Employees]  VALUES (0,'Jan', 'Kowalski');
INSERT INTO [Employees]  VALUES (0,'Moose', 'mus123');
INSERT INTO [Permissions] VALUES (0,'Boss');
INSERT INTO [Permissions] VALUES (0,'Administrator');
INSERT INTO [Permissions] VALUES (1,'PM');
INSERT INTO [Permissions] VALUES (2,'PM');
INSERT INTO [Permissions] VALUES (2,'Prze³o¿ony');

GO