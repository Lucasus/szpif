use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR User...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getEmployeeForSelect') IS NOT NULL
	DROP PROCEDURE getEmployeeForSelect
GO
IF OBJECT_ID('updateEmployeeForSelect') IS NOT NULL
	DROP PROCEDURE updateEmployeeForSelect
GO
IF OBJECT_ID('insertEmployeeForSelect') IS NOT NULL
	DROP PROCEDURE insertEmployeeForSelect
GO
IF OBJECT_ID('deleteEmployeeForSelect') IS NOT NULL
	DROP PROCEDURE deleteEmployeeForSelect
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getEmployeeForSelect
AS
	SELECT 
		em.Id, 
		creds.FirstName, 
		creds.EMail
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
	where em.Id in (select EmployeeId from Roles where Role = 'Pracownik')
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateEmployeeForSelect
AS
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertEmployeeForSelect
AS
--nic nie robi
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteEmployeeForSelect
AS
--nic nie robi
GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getEmployeeForSelect TO BasicRole
GRANT EXECUTE ON updateEmployeeForSelect TO BasicRole
GRANT EXECUTE ON insertEmployeeForSelect TO BasicRole
GRANT EXECUTE ON deleteEmployeeForSelect TO BasicRole
use szpifDatabase

