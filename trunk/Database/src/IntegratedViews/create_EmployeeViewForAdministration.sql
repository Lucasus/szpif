use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR ADMINISTRATION...'
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
--CREATE PROCEDURE getEmployeeViewForAdministration
--AS
--	SELECT DISTINCT 
--		em.Id, 
--		em.Login, 
--		creds.Name, 
--		creds.EMail,
--	    dbo.aggregateRolesFunction (em.Id) AS Uprawnienia
--	FROM Employees em
--		inner join [Roles] roles on em.Id = roles.EmployeeId
--		inner join [Credentials] creds on em.CredentialsId = creds.Id
--GO

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
AS
    update Employees set Login = @Login  where Id = @Id    
    update Credentials set Name = @Name, EMail = @EMail where Id = 
    (select CredentialsId from Employees where Id = @Id)
    
    DELETE FROM Roles
WHERE EmployeeId = 0

INSERT INTO Roles
SELECT 0, nref.value('@Name[1]', 'nvarchar(50)') Role
FROM   @roles.nodes('//Item') AS R(nref)
WHERE  nref.value('@Value[1]', 'nvarchar(50)') = 1

    
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertEmployeeViewForAdministration
  @Id			int,
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40)
AS
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteEmployeeViewForAdministration
	@Id			int
AS

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployeeViewForAdministration TO OwnerRole
GRANT EXECUTE ON updateEmployeeViewForAdministration TO OwnerRole
