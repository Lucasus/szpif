use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR User...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getEmployeesForUser') IS NOT NULL
	DROP PROCEDURE getEmployeesForUser
GO
IF OBJECT_ID('updateEmployeesForUser') IS NOT NULL
	DROP PROCEDURE updateEmployeesForUser
GO
IF OBJECT_ID('insertEmployeesForUser') IS NOT NULL
	DROP PROCEDURE insertEmployeesForUser
GO
IF OBJECT_ID('deleteEmployeesForUser') IS NOT NULL
	DROP PROCEDURE deleteEmployeesForUser
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getEmployeesForUser
AS
	declare @login varchar(40);
    select @login = SYSTEM_USER

	SELECT 
		em.Id, 
		em.Login, 
		creds.Name, 
		creds.EMail,
		em.Przelozony,
	    dbo.xmlRoles (em.Id) AS Roles
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
	where em.Login = @login
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateEmployeesForUser
  @Id			int,
  @Login		nvarchar(40),
  @Name			nvarchar(40),
  @EMail		nvarchar(40),
  @Password		nvarchar(40),
  @Przelozony	int
WITH EXECUTE AS 'szpifadmin'
AS
    update Employees set Login = @Login, Przelozony = @Przelozony  where Id = @Id    
    IF @Password != '' and @Password is not null
    BEGIN
		update Employees set Password = @Password  where Id = @Id    
    END
    
    update Credentials set Name = @Name, EMail = @EMail where Id = 
    (select CredentialsId from Employees where Id = @Id)

    
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertEmployeesForUser
AS
--nic nie robi
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteEmployeesForUser
AS
--nic nie robi
GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployeesForUser TO BasicRole
GRANT EXECUTE ON updateEmployeesForUser TO BasicRole
GRANT EXECUTE ON insertEmployeesForUser TO BasicRole
GRANT EXECUTE ON deleteEmployeesForUser TO BasicRole
use szpifDatabase

