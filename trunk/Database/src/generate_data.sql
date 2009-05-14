Use szpifDatabase
PRINT 'GENERATING DATA...'
GO
delete from [Roles];
delete from [Employees];
delete from [Credentials];
delete from [OrderStatus]
delete from [Orders]
delete from [ProjectStatus]
delete from [Projects]
delete from [TaskStatus]
delete from [Tasks]
--DBCC CHECKIDENT (Employees, RESEED, 1);
--DBCC CHECKIDENT (Roles, RESEED, 1);
--DBCC CHECKIDENT (Credentials, RESEED, 1);


INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Lucas', 'Szymon','Wiatrak', 'lucas@lucas.pl', '666', 'Krzyzowa', '7', '0', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Mooseq', 'Krzysztof','Master', 'moose@master.pl', '123', 'Slaska', '42', '3', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Lucas', 'Szymon','Wiatrak', 'lucas@lucas.pl', '666', 'Krzyzowa', '7', '10', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Jan', NULL,'Nowak', 'jan@op.pl', NULL, 'Czarnowiejska', '76', '30', 'Krakow', '32-480', 'Poland','11111145511' ,'33456');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Anna', 'Maria','Brzezinska', 'anna@wp.pl', '66634567', 'Rejmonta', '2', NULL, 'Zabierzow', '32-280', 'Poland','11111245511' ,'121');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Piotr', 'Pawel','Herman', 'piotr.k@op.pl', '34666', 'Mickiewicza', '2', '30', 'Warszawa', '12-080', 'Poland','33411111111' ,'345511');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Kamil', 'Szymon','Wernon', 'kamil@lucas.pl', '63344446', 'Kawiory', '7', NULL, 'Krakow', '32-080', 'Poland',NULL ,'111');




INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (1, NULL, 'Moose123', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (2, NULL, 'lukasz', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (3, 2, 'janek', 'Master', 15, 12);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (4, 2, 'ania', 'Master', 11, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (5, 2, 'piotr', 'Master', 1, 1);

INSERT INTO [RoleNames] VALUES ('W³aœciciel');
INSERT INTO [RoleNames] VALUES ('Project Manager');
INSERT INTO [RoleNames] VALUES ('Prze³o¿ony');
INSERT INTO [RoleNames] VALUES ('Pracownik');
INSERT INTO [RoleNames] VALUES ('Opiekun handlowy');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (1, 'W³aœciciel');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (2, 'W³aœciciel');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (2, 'Project Manager');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (3, 'Project Manager');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (2, 'Prze³o¿ony');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (3, 'Pracownik');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (3, 'Opiekun handlowy');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (4, 'Pracownik');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (5, 'Pracownik');

INSERT INTO [Clients] (CredentialsId, CompanyName) VALUES (6, 'Firemka');

INSERT INTO [OrderStatus] (EndDate, UsedHours, UsedBudget, Status) VALUES (NULL, 3, 67, 'W toku') 
INSERT INTO [OrderStatus] (EndDate, UsedHours, UsedBudget, Status) VALUES (NULL, 8, 556, 'W toku')


INSERT INTO [Orders] (ClientId, OrderStatusId, OrderName, Description, MaxHours, MaxBudget, Price, StartDate, ExpectedEndDate)
	VALUES(1, 1, 'Zamowienie1', 'Opis1', 45, 365, 766,'20090805' , '20091203' );

INSERT INTO [Orders] (ClientId, OrderStatusId, OrderName, Description, MaxHours, MaxBudget, Price, StartDate, ExpectedEndDate)
	VALUES(1, 2, 'Zamowienie2', 'Opis2', 89, 7000, 8000,'20090815' , '20091029' );

INSERT INTO [ProjectStatus] (EndDate, UsedHours, UsedBudget, Status) VALUES (NULL, 1, 44, 'W toku') 
INSERT INTO [ProjectStatus] (EndDate, UsedHours, UsedBudget, Status) VALUES (NULL, 2, 589, 'W toku')
INSERT INTO [ProjectStatus] (EndDate, UsedHours, UsedBudget, Status) VALUES (NULL, 1, 582, 'W toku')

INSERT INTO [Projects] (ManagerId, OrderId, ProjectStatusId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate)
	VALUES(2, 1, 1, 'Projekt1',  7, 36, '20090810' , '20091105' );
INSERT INTO [Projects] (ManagerId, OrderId, ProjectStatusId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate)
	VALUES(3, 1, 2, 'Projekt2',  8, 45, '20090927' , '20091128' );
INSERT INTO [Projects] (ManagerId, OrderId, ProjectStatusId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate)
	VALUES(2, 2, 3, 'Projekt3',  9, 22, '20090922' , '20091201' );

INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 2, 1, 'Zakonczone') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 2, 1, 'Zakonczone') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 
INSERT INTO [TaskStatus] (EndDate, UsedHours, BonusGiven, Status) VALUES (NULL, 1, 0, 'W toku') 


INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(3, 1, 1, 9, 'Zadanie1', '20091129', '20091201' , 45);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(4, 1, 2, 1, 'Zadanie2', '20091129', '20091202' , 50);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(5, 1, 3, 2, 'Zadanie3', '20091118', '20091202' , 100);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(4, 1, 4, 1, 'Zadanie4', '20091127', '20091201' , 45);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(4, 1, 5, 1, 'Zadanie5', '20091125', '20091202' , 45);

INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(3, 2, 6, 9, 'Zadanie6', '20091129', '20091201' , 45);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(4, 2, 7, 1, 'Zadanie7', '20091129', '20091202' , 50);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(5, 2, 8, 2, 'Zadanie8', '20091118', '20091202' , 100);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(4, 2, 9, 1, 'Zadanie9', '20091127', '20091201' , 45);
INSERT INTO [Tasks] (EmployeeId, ProjectId, TaskStatusId, MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus)
	VALUES(4, 2, 10, 1, 'Zadanie9', '20091125', '20091202' , 45);

/*INSERT INTO [Credentials] VALUES ('Lukasz Wiatrak','lukasz@lukasz');
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

*/
GO