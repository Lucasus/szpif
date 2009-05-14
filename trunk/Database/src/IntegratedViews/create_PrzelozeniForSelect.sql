use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR User...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getPrzelozeniForSelect') IS NOT NULL
	DROP PROCEDURE getPrzelozeniForSelect
GO
IF OBJECT_ID('updatePrzelozeniForSelect') IS NOT NULL
	DROP PROCEDURE updatePrzelozeniForSelect
GO
IF OBJECT_ID('insertPrzelozeniForSelect') IS NOT NULL
	DROP PROCEDURE insertPrzelozeniForSelect
GO
IF OBJECT_ID('deletePrzelozeniForSelect') IS NOT NULL
	DROP PROCEDURE deletePrzelozeniForSelect
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getPrzelozeniForSelect
AS
	SELECT 
		em.Id, 
		creds.FirstName, 
		creds.EMail
	FROM Employees em
		inner join [Credentials] creds on em.CredentialsId = creds.Id
	where em.Id in (select EmployeeId from Roles where Role = 'Prze³o¿ony')
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updatePrzelozeniForSelect
AS
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertPrzelozeniForSelect
AS
--nic nie robi
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deletePrzelozeniForSelect
AS
--nic nie robi
GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getPrzelozeniForSelect TO BasicRole
GRANT EXECUTE ON updatePrzelozeniForSelect TO BasicRole
GRANT EXECUTE ON insertPrzelozeniForSelect TO BasicRole
GRANT EXECUTE ON deletePrzelozeniForSelect TO BasicRole
use szpifDatabase

