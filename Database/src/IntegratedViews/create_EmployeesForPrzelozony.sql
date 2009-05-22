use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getEmployeesForPrzelozony') IS NOT NULL
	DROP PROCEDURE getEmployeesForPrzelozony
GO
IF OBJECT_ID('updateEmployeesForPrzelozony') IS NOT NULL
	DROP PROCEDURE updateEmployeesForPrzelozony
GO
IF OBJECT_ID('insertEmployeesForPrzelozony') IS NOT NULL
	DROP PROCEDURE insertEmployeesForPrzelozony
GO
IF OBJECT_ID('deleteEmployeesForPrzelozony') IS NOT NULL
	DROP PROCEDURE deleteEmployeesForPrzelozony
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getEmployeesForPrzelozony
AS
    declare @login varchar(40);
    select @login = SYSTEM_USER
	SELECT 
		em.Id, 
		em.Login, 
		creds.FirstName,
		creds.SecondName,
		creds.LastName,
		creds.EMail,
		creds.Street,
		creds.HouseNr,
		creds.FlatNr,
		creds.City,
		creds.PostalCode,
		creds.Country,
		creds.Pesel,
		creds.Phone,
		creds.Nip,
	    dbo.xmlRoles (em.Id) AS Roles,
		dbo.EmployeeToXmlLink(em.SuperiorId, em.Login, 'Employees') AS Przelozony
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
	WHERE em.SuperiorId in (select Id from Employees where Login = @login)
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateEmployeesForPrzelozony
  @Id			int,
  @Login		nvarchar(40),
  @FirstName	nvarchar(40),
  @SecondName	nvarchar(40),
  @LastName		nvarchar(40),
  @EMail		nvarchar(40),
  @Street		nvarchar(40),
  @HouseNr		char(7),
  @FlatNr		char(7),
  @City			nvarchar(40),
  @PostalCode	nvarchar(6),
  @Country		nvarchar(50),
  @Pesel		char(11),
  @Phone		char(20),
  @Nip			char(20),
  @Password		nvarchar(40),
  @Przelozony	xml,
  @Roles		xml
WITH EXECUTE AS 'szpifadmin'
AS
/*
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @Przelozony.nodes('//Link') AS R(nref))

    update Employees set Login = @Login, SuperiorId = @przelId  where Id = @Id    
    IF @Password != '' and @Password is not null
    BEGIN
		update Employees set Password = @Password  where Id = @Id    
    END
    
    update Credentials set FirstName = @FirstName, LastName = @LastName, SecondName = @SecondName, Street = @Street, HouseNr = @HouseNr, FlatNr = @FlatNr, City = @City, PostalCode = @PostalCode, Country = @Country, Pesel = @Pesel, Phone = @Phone, Nip = @Nip, EMail = @EMail where Id = 
    (select CredentialsId from Employees where Id = @Id)
    
    DELETE FROM Roles
WHERE EmployeeId = @Id

INSERT INTO Roles
SELECT @Id, nref.value('@Name[1]', 'nvarchar(50)') Role
FROM   @Roles.nodes('//Item') AS R(nref)
WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

  */  
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertEmployeesForPrzelozony
  @Id			int,
  @Login		nvarchar(40),
  @FirstName	nvarchar(40),
  @SecondName	nvarchar(40),
  @LastName		nvarchar(40),
  @EMail		nvarchar(40),
  @Street		nvarchar(40),
  @HouseNr		char(7),
  @FlatNr		char(7),
  @City			nvarchar(40),
  @PostalCode	nvarchar(6),
  @Country		nvarchar(50),
  @Pesel		char(11),
  @Phone		char(20),
  @Nip			char(20),
  @Password		nvarchar(40),
  @Przelozony	xml,
  @Roles		xml
WITH EXECUTE AS  'szpifadmin'
AS
/*

	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @Przelozony.nodes('//Link') AS R(nref))

INSERT INTO [Credentials] (FirstName, SecondName, LastName, EMail, Street, HouseNr, FlatNr, City, PostalCode, Country, Pesel, Phone, Nip)  VALUES (@FirstName, @SecondName, @LastName ,@EMail, @Street, @HouseNr, @FlatNr, @City, @PostalCode, @Country, @Pesel, @Phone, @Nip);
declare @newId int;
SELECT @newId = SCOPE_IDENTITY() 
INSERT INTO [Employees] (CredentialsId, Login, Password, HoursNr, RatePerHour, SuperiorId)  VALUES (@newId,@Login,@Password, 10 , 10 ,@przelId);
SELECT @newId = SCOPE_IDENTITY() 

INSERT INTO Roles
SELECT @newId, nref.value('@Name[1]', 'nvarchar(50)') Role
FROM   @Roles.nodes('//Item') AS R(nref)
WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1
*/
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteEmployeesForPrzelozony
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  INSERT INTO Help values ('dupa')
  DELETE FROM Employees where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('EmployeesForPrzelozony','Roles', 'CheckedListBox', null);
INSERT INTO [ColumnsToTypes] VALUES ('EmployeesForPrzelozony','Przelozony', 'Link', 'PrzelozeniForSelect');



GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployeesForPrzelozony TO OwnerRole
GRANT EXECUTE ON updateEmployeesForPrzelozony TO OwnerRole
GRANT EXECUTE ON insertEmployeesForPrzelozony TO OwnerRole
GRANT EXECUTE ON deleteEmployeesForPrzelozony TO OwnerRole
use szpifDatabase

