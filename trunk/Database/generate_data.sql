Use szpifDatabase
PRINT 'GENERATING DATA...'
GO
delete from [Roles];
delete from [Employees];
delete from [Credentials];
DBCC CHECKIDENT (Employees, RESEED, 0);
DBCC CHECKIDENT (Roles, RESEED, 0);
DBCC CHECKIDENT (Credentials, RESEED, 0);

INSERT INTO [Credentials] VALUES ('blabla','Prze³o¿ony');
INSERT INTO [Employees]  VALUES (0,'lukasz', 'Master');
INSERT INTO [Employees]  VALUES (0,'Jan', 'Kowalski');
INSERT INTO [Employees]  VALUES (0,'Moose', 'mus123');
INSERT INTO [Roles] VALUES (0,'Boss');
INSERT INTO [Roles] VALUES (0,'Administrator');
INSERT INTO [Roles] VALUES (1,'PM');
INSERT INTO [Roles] VALUES (2,'PM');
INSERT INTO [Roles] VALUES (2,'Prze³o¿ony');

GO