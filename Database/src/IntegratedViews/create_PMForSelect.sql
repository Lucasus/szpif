use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR User...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getPMForSelect') IS NOT NULL
	DROP PROCEDURE getPMForSelect
GO
IF OBJECT_ID('updatePMForSelect') IS NOT NULL
	DROP PROCEDURE updatePMForSelect
GO
IF OBJECT_ID('insertPMForSelect') IS NOT NULL
	DROP PROCEDURE insertPMForSelect
GO
IF OBJECT_ID('deletePMForSelect') IS NOT NULL
	DROP PROCEDURE deletePMForSelect
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getPMForSelect
AS
	SELECT 
		em.Id, 
		creds.FirstName, 
		creds.EMail
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
	where em.Id in (select EmployeeId from Roles where Role = 'Project Manager')
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updatePMForSelect
AS
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertPMForSelect
AS
--nic nie robi
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deletePMForSelect
AS
--nic nie robi
GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getPMForSelect TO BasicRole
GRANT EXECUTE ON updatePMForSelect TO BasicRole
GRANT EXECUTE ON insertPMForSelect TO BasicRole
GRANT EXECUTE ON deletePMForSelect TO BasicRole
use szpifDatabase

