use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR ADMINISTRATION...'
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getEmployeeViewForAdministration') IS NOT NULL
	DROP PROCEDURE getEmployeeViewForAdministration
GO
IF OBJECT_ID('updateEmployeeViewForAdministration') IS NOT NULL
	DROP PROCEDURE updateEmployeeViewForAdministration
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getEmployeeViewForAdministration
AS
	SELECT DISTINCT em.Id, Login, Name, EMail,
	           dbo.aggregateRolesFunction (em.Id) AS Uprawnienia
	FROM Employees em
			inner join [Roles] perm on em.Id = perm.EmployeeId
			inner join [Credentials] creds on em.CredentialsId = creds.Id
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateEmployeeViewForAdministration
  @Id			int,
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40)
AS
    update Employees set Login = @Login  where Id = @Id    
    update Credentials set Name = @Name, EMail = @EMail where Id = 
    (select CredentialsId from Employees where Id = @Id)
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployeeViewForAdministration TO OwnerRole
GRANT EXECUTE ON updateEmployeeViewForAdministration TO OwnerRole
