Use szpifDatabase
PRINT 'GENERATING DATA...'
GO
delete from [Roles];
delete from [Employees];
delete from [Credentials];
DBCC CHECKIDENT (Employees, RESEED, 0);
DBCC CHECKIDENT (Roles, RESEED, 0);
DBCC CHECKIDENT (Credentials, RESEED, 0);

INSERT INTO [Credentials] VALUES ('Lukasz Wiatrak','lukasz@lukasz');
INSERT INTO [Credentials] VALUES ('Blablab blablawy','bla@bla');
INSERT INTO [Credentials] VALUES ('Ktostam','nie mam maila');
INSERT INTO [Credentials] VALUES ('Kolejny','nie mam maila');
INSERT INTO [Employees]  VALUES (0,'lukasz', 'Master');
INSERT INTO [Employees]  VALUES (1,'Jan', 'Kowalski');
INSERT INTO [Employees]  VALUES (2,'Moose', 'mus123');
INSERT INTO [Employees]  VALUES (3,'Next', 'mus123');
INSERT INTO [RoleNames] VALUES ('W³aœciciel');
INSERT INTO [RoleNames] VALUES ('Project Manager');
INSERT INTO [RoleNames] VALUES ('Prze³o¿ony');
INSERT INTO [RoleNames] VALUES ('Pracownik');
INSERT INTO [RoleNames] VALUES ('Opiekun handlowy');
INSERT INTO [Roles] VALUES (0,'W³aœciciel');
INSERT INTO [Roles] VALUES (0,'Opiekun handlowy');
INSERT INTO [Roles] VALUES (1,'Project Manager');
INSERT INTO [Roles] VALUES (2,'Project Manager');
INSERT INTO [Roles] VALUES (2,'Prze³o¿ony');
INSERT INTO [Roles] VALUES (3,'Pracownik');

GO