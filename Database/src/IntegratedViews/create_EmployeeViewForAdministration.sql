use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR ADMINISTRATION...'
GO
SET QUOTED_IDENTIFIER ON

GO

SET ANSI_NULLS ON

GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getEmployeeViewForAdministration') IS NOT NULL
	DROP PROCEDURE getEmployeeViewForAdministration
GO
IF OBJECT_ID('getEmployeeViewForAdministration2') IS NOT NULL
	DROP PROCEDURE getEmployeeViewForAdministration2
GO
IF OBJECT_ID('updateEmployeeViewForAdministration') IS NOT NULL
	DROP PROCEDURE updateEmployeeViewForAdministration
GO
IF OBJECT_ID('insertEmployeeViewForAdministration') IS NOT NULL
	DROP PROCEDURE insertEmployeeViewForAdministration
GO
IF OBJECT_ID('deleteEmployeeViewForAdministration') IS NOT NULL
	DROP PROCEDURE deleteEmployeeViewForAdministration
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getEmployeeViewForAdministration
AS
	SELECT 
		em.Id, 
		em.Login, 
		creds.Name, 
		creds.EMail,
	    dbo.xmlRoles (em.Id) AS Roles
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateEmployeeViewForAdministration
  @Id			int,
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @Roles		xml,
  @EMail		nvarchar(40)
WITH EXECUTE AS 'szpifadmin'
AS
    update Employees set Login = @Login  where Id = @Id    
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
CREATE PROCEDURE insertEmployeeViewForAdministration
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @Roles		xml,
  @EMail		nvarchar(40)
WITH EXECUTE AS  'szpifadmin'
AS
INSERT INTO [Credentials] VALUES (@Name,@EMail);
declare @newId int;
SELECT @newId = SCOPE_IDENTITY() 
INSERT INTO [Employees]  VALUES (@newId,@EMail, 'haslo');
SELECT @newId = SCOPE_IDENTITY() 

INSERT INTO Roles
SELECT @newId, nref.value('@Name[1]', 'nvarchar(50)') Role
FROM   @Roles.nodes('//Item') AS R(nref)
WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteEmployeeViewForAdministration
	@Id			int
WITH EXECUTE AS  'szpifadmin'
AS
	DELETE FROM Roles where EmployeeId = @Id
	declare @CredentialsId int;
	select @CredentialsId = (SELECT CredentialsId from Employees where Id = @Id)
	DELETE FROM Employees where Id = @Id
	DELETE FROM Credentials where Id = @CredentialsId 
GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployeeViewForAdministration TO OwnerRole
GRANT EXECUTE ON updateEmployeeViewForAdministration TO OwnerRole
GRANT EXECUTE ON insertEmployeeViewForAdministration TO OwnerRole
GRANT EXECUTE ON deleteEmployeeViewForAdministration TO OwnerRole
use szpifDatabase

