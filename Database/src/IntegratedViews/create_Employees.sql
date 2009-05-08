use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getEmployees') IS NOT NULL
	DROP PROCEDURE getEmployees
GO
IF OBJECT_ID('updateEmployees') IS NOT NULL
	DROP PROCEDURE updateEmployees
GO
IF OBJECT_ID('insertEmployees') IS NOT NULL
	DROP PROCEDURE insertEmployees
GO
IF OBJECT_ID('deleteEmployees') IS NOT NULL
	DROP PROCEDURE deleteEmployees
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getEmployees
AS
	SELECT 
		em.Id, 
		em.Login, 
		creds.Name, 
		creds.EMail,
		dbo.przelozonyToXmlLink(em.Przelozony) AS  Przelozony,
	    dbo.xmlRoles (em.Id) AS Roles
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateEmployees
  @Id			int,
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40),
  @Password		nvarchar(40),
  @Przelozony	xml,
  @Roles		xml
WITH EXECUTE AS 'szpifadmin'
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @Przelozony.nodes('//Link') AS R(nref))

    update Employees set Login = @Login, Przelozony = @przelId  where Id = @Id    
    IF @Password != '' and @Password is not null
    BEGIN
		update Employees set Password = @Password  where Id = @Id    
    END
    
    update Credentials set Name = @Name, EMail = @EMail where Id = 
    (select CredentialsId from Employees where Id = @Id)
    
    DELETE FROM Roles
WHERE EmployeeId = @Id

INSERT INTO Roles
SELECT @Id, nref.value('@Name[1]', 'nvarchar(50)') Role
FROM   @Roles.nodes('//Item') AS R(nref)
WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

    
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertEmployees
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40),
  @Password		nvarchar(40),
  @Przelozony	xml,
  @Roles		xml
WITH EXECUTE AS  'szpifadmin'
AS


	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @Przelozony.nodes('//Link') AS R(nref))

INSERT INTO [Credentials] VALUES (@Name,@EMail);
declare @newId int;
SELECT @newId = SCOPE_IDENTITY() 
INSERT INTO [Employees]  VALUES (@newId,@Login,@Password,@przelId);
SELECT @newId = SCOPE_IDENTITY() 

INSERT INTO Roles
SELECT @newId, nref.value('@Name[1]', 'nvarchar(50)') Role
FROM   @Roles.nodes('//Item') AS R(nref)
WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteEmployees
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  INSERT INTO Help values ('dupa')
  DELETE FROM Employees where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('Employees','Roles', 'CheckedListBox', null);
INSERT INTO [ColumnsToTypes] VALUES ('Employees','Przelozony', 'Link', 'PrzelozeniForSelect');



GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployees TO OwnerRole
GRANT EXECUTE ON updateEmployees TO OwnerRole
GRANT EXECUTE ON insertEmployees TO OwnerRole
GRANT EXECUTE ON deleteEmployees TO OwnerRole
use szpifDatabase

