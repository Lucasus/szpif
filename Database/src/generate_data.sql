Use szpifDatabase
PRINT 'GENERATING DATA...'
GO
delete from [Roles];
delete from [Employees];
delete from [Credentials];
delete from [Projects];
delete from [Tasks];
--DBCC CHECKIDENT (Employees, RESEED, 1);
--DBCC CHECKIDENT (Roles, RESEED, 1);
--DBCC CHECKIDENT (Credentials, RESEED, 1);


INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Lucas', 'Szymon','Wiatrak', 'lucas@lucas.pl', '666', 'Krzyzowa', '7', '0', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Mooseq', 'Krzysztof','Cywicki', 'moose@master.pl', '123', 'Slaska', '42', '3', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Janek', 'Szymon','Jankowski', 'lucas@lucas.pl', '666', 'Krzyzowa', '7', '10', 'Zabierzow', '32-080', 'Poland','11111111111' ,'111');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Jan', NULL,'Nowak', 'jan@op.pl', NULL, 'Czarnowiejska', '76', '30', 'Krakow', '32-480', 'Poland','11111145511' ,'33456');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Anna', 'Maria','Brzezinska', 'anna@wp.pl', '66634567', 'Rejmonta', '2', NULL, 'Zabierzow', '32-280', 'Poland','11111245511' ,'121');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Piotr', 'Pawel','Herman', 'piotr.k@op.pl', '34666', 'Mickiewicza', '2', '30', 'Warszawa', '12-080', 'Poland','33411111111' ,'345511');
INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Phone, Street, HouseNr, FlatNr , City, PostalCode, Country, Pesel, Nip) 
  VALUES ('Kamil', 'Szymon','Wernon', 'kamil@lucas.pl', '63344446', 'Kawiory', '7', NULL, 'Krakow', '32-080', 'Poland',NULL ,'111');


INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (1, NULL, 'lukasz', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (2, NULL, 'Moose123', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (3, 2, 'janek', 'Master', 15, 12);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (4, 2, 'jan', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (5, 2, 'ania', 'Master', 11, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (6, 2, 'piotr', 'Master', 1, 1);
INSERT INTO [Employees] (CredentialsId, SuperiorId, Login, Password, HoursNr, RatePerHour) VALUES (7, 2, 'kamil', 'Master', 1, 1);

INSERT INTO [RoleNames] VALUES ('W³aœciciel');
INSERT INTO [RoleNames] VALUES ('Project Manager');
INSERT INTO [RoleNames] VALUES ('Prze³o¿ony');
INSERT INTO [RoleNames] VALUES ('Pracownik');
INSERT INTO [RoleNames] VALUES ('Opiekun handlowy');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (1, 'W³aœciciel');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (2, 'W³aœciciel');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (1, 'Project Manager');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (3, 'Project Manager');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (1, 'Prze³o¿ony');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (3, 'Pracownik');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (3, 'Opiekun handlowy');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (4, 'Pracownik');
INSERT INTO [Roles] (EmployeeId, Role) VALUES (5, 'Pracownik');

INSERT INTO [Clients] (CredentialsId, CompanyName) VALUES (6, 'Firemka');

INSERT INTO [Projects] (ManagerId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate, Status)
	VALUES(3, 'Projekt1',  7, 36, '20090810' , '20091105', 'Nowy' );
INSERT INTO [Projects] (ManagerId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate, Status)
	VALUES(3, 'Projekt2',  8, 45, '20090927' , '20091128', 'W toku');
INSERT INTO [Projects] (ManagerId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate, Status)
	VALUES(3, 'Projekt3',  9, 22, '20090922' , '20091201', 'Zakoñczony' );

INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(3, 1,  9, 'Zadanie1', '20091129', '20091201' , 45, 'Nowe');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(4, 1,  1, 'Zadanie2', '20091129', '20091202' , 50, 'W toku');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(5, 1,  2, 'Zadanie3', '20091118', '20091202' , 100, 'Oczekuj¹ce na akceptacjê');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(4, 1,  1, 'Zadanie4', '20091127', '20091201' , 45, 'Odrzucone');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(4, 1,  1, 'Zadanie5', '20091125', '20091202' , 45, 'Niewykonane');

INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(3, 2,  9, 'Zadanie6', '20091129', '20091201' , 45, 'Zakoñczone');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(4, 2,  1, 'Zadanie7', '20091129', '20091202' , 50, 'W toku');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(5, 2,  2, 'Zadanie8', '20091118', '20091202' , 100, 'W toku');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(4, 2,  1, 'Zadanie9', '20091127', '20091201' , 45, 'Odrzucone');
INSERT INTO [Tasks] (EmployeeId, ProjectId,  MaxHours, TaskName, StartDate, ExpectedEndDate, Bonus, Status)
	VALUES(4, 2,  1, 'Zadanie9', '20091125', '20091202' , 45, 'Nowe');

GO