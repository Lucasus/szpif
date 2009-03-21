Use szpifDatabase

delete from [Permissions];
delete from [Employees];
delete from [Credentials];
DBCC CHECKIDENT (Credentials, RESEED, -1);
DBCC CHECKIDENT (Employees, RESEED, -1);
DBCC CHECKIDENT (Permissions, RESEED, -1);


INSERT INTO [Credentials] VALUES ('Lukasz Wiatrak','Lukasz@gmail.com');
INSERT INTO [Credentials] VALUES ('Juuuuuurek','jurek@gmail.com');
INSERT INTO [Credentials] VALUES ('Losiek Loskowski','Moose@gmail.com');
INSERT INTO [Employees]  VALUES ('0','lukasz', 'Master');
INSERT INTO [Employees]  VALUES ('1','Jan', 'Kowalski');
INSERT INTO [Employees]  VALUES ('2','Moose', 'mus123');
INSERT INTO [Permissions] VALUES (0,'Boss');
INSERT INTO [Permissions] VALUES (0,'Administrator');
INSERT INTO [Permissions] VALUES (1,'PM');
INSERT INTO [Permissions] VALUES (2,'PM');
INSERT INTO [Permissions] VALUES (2,'Prze³o¿ony');

Select * from Employees 
Select * from Permissions
Select * from Credentials