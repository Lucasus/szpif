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
INSERT INTO [Credentials] VALUES ('Janusz Majewski','majewski@agh.edu.pl');
INSERT INTO [Credentials] VALUES ('Krzysztof Nowak','krzychu@vp.pl');
INSERT INTO [Credentials] VALUES ('Marcin Kuta','kuta@agh.edu.pl');
INSERT INTO [Credentials] VALUES ('Marek Valenta','valenta@agh.edu.pl');
INSERT INTO [Credentials] VALUES ('Anna Zygmunt','zygmunt@agh.edu.pl');
INSERT INTO [Employees]  VALUES (0,'lukasz', 'Master',null);
INSERT INTO [Employees]  VALUES (1,'Janek', 'ala123',0);
INSERT INTO [Employees]  VALUES (2,'Krzys', 'bla123',0);
INSERT INTO [Employees]  VALUES (3,'Marcin', 'mus123',0);
INSERT INTO [Employees]  VALUES (4,'ktos', 'mus123',2);
INSERT INTO [Employees]  VALUES (5,'az', 'mus123',2);
INSERT INTO [RoleNames] VALUES ('W³aœciciel');
INSERT INTO [RoleNames] VALUES ('Project Manager');
INSERT INTO [RoleNames] VALUES ('Prze³o¿ony');
INSERT INTO [RoleNames] VALUES ('Pracownik');
INSERT INTO [RoleNames] VALUES ('Opiekun handlowy');
INSERT INTO [Roles] VALUES (0,'W³aœciciel');
INSERT INTO [Roles] VALUES (1,'Opiekun handlowy');
INSERT INTO [Roles] VALUES (2,'Prze³o¿ony');
INSERT INTO [Roles] VALUES (0,'Project Manager');
INSERT INTO [Roles] VALUES (2,'Project Manager');
INSERT INTO [Roles] VALUES (3,'Project Manager');
INSERT INTO [Roles] VALUES (4,'Pracownik');
INSERT INTO [Roles] VALUES (4,'Opiekun handlowy');
INSERT INTO [Roles] VALUES (5,'Pracownik');

GO