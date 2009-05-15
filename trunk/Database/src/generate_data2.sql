Use szpifDatabase
PRINT 'GENERATING DATA...'
GO
delete from [Roles];
delete from [Employees];
delete from [Credentials];
delete from [RoleNames];

DBCC CHECKIDENT (Employees, RESEED, 0);
DBCC CHECKIDENT (Roles, RESEED, 0);
DBCC CHECKIDENT (Credentials, RESEED, 0);

INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Lucas', 'Szymon','Wiatrak', 'lucas@lucas.pl', '666', 'Krzyzowa', '7', '0', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Mooseq', 'Krzysztof','Master', 'moose@master.pl', '123', 'Slaska', '42', '3', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Marcin', 'Krzysztof','Kuta', 'moose@master.pl', '123', 'Slaska', '42', '3', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Marek', 'Szymon','Valenta', 'lucas@lucas.pl', '666', 'Krzyzowa', '7', '0', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Anna', 'Krzysztof','Zygmunt', 'moose@master.pl', '123', 'Slaska', '42', '3', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Kolejne', 'Krzysztof','Cos', 'moose@master.pl', '123', 'Slaska', '42', '3', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Employees] (CredentialsId, Login, Password, HoursNr, RatePerHour) VALUES (0, 'lukasz', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, Login, Password, HoursNr, RatePerHour) VALUES (1, 'Moose123', 'Master', 1, 1);
INSERT INTO [Employees]  VALUES (2,0,'Krzys', 'bla123',1,1);
INSERT INTO [Employees]  VALUES (3,1,'Marcin', 'mus123',1,1);
INSERT INTO [Employees]  VALUES (4,0,'ktos', 'mus123',1,1);
INSERT INTO [Employees]  VALUES (5,1,'az', 'mus123',1,1);
INSERT INTO [RoleNames] VALUES ('W³aœciciel');
INSERT INTO [RoleNames] VALUES ('Project Manager');
INSERT INTO [RoleNames] VALUES ('Prze³o¿ony');
INSERT INTO [RoleNames] VALUES ('Pracownik');
INSERT INTO [RoleNames] VALUES ('Opiekun handlowy');
INSERT INTO [Roles] VALUES (0,'W³aœciciel');
INSERT INTO [Roles] VALUES (1,'Opiekun handlowy');
INSERT INTO [Roles] VALUES (1,'Prze³o¿ony');
INSERT INTO [Roles] VALUES (2,'Prze³o¿ony');
INSERT INTO [Roles] VALUES (3,'Prze³o¿ony');
INSERT INTO [Roles] VALUES (0,'Project Manager');
INSERT INTO [Roles] VALUES (2,'Project Manager');
INSERT INTO [Roles] VALUES (3,'Project Manager');
INSERT INTO [Roles] VALUES (4,'Pracownik');
INSERT INTO [Roles] VALUES (4,'Opiekun handlowy');
INSERT INTO [Roles] VALUES (5,'Pracownik');

INSERT INTO [Projects]
           ([ManagerId],OrderId,[ProjectStatusId],[ProjectName],[MaxHours],[MaxBudget],[StartDate],[ExpectedEndDate])
     VALUES(0, null, null, 'Szpif',3,null,null,null);

INSERT INTO [Projects]
           ([ManagerId],OrderId,[ProjectStatusId],[ProjectName],[MaxHours],[MaxBudget],[StartDate],[ExpectedEndDate])
     VALUES(3, null, null, 'Szpif2',3,null,null,null);

INSERT INTO [Projects]
           ([ManagerId],OrderId,[ProjectStatusId],[ProjectName],[MaxHours],[MaxBudget],[StartDate],[ExpectedEndDate])
     VALUES(3, null, null, 'Szpif3',3,null,null,null);


GO