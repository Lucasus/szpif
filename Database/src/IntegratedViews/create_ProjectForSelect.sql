use szpifDatabase
PRINT 'CREATING EMPLOYEE VIEW FOR User...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectForSelect') IS NOT NULL
	DROP PROCEDURE getProjectForSelect
GO
IF OBJECT_ID('updateProjectForSelect') IS NOT NULL
	DROP PROCEDURE updateProjectForSelect
GO
IF OBJECT_ID('insertProjectForSelect') IS NOT NULL
	DROP PROCEDURE insertProjectForSelect
GO
IF OBJECT_ID('deleteProjectForSelect') IS NOT NULL
	DROP PROCEDURE deleteProjectForSelect
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getProjectForSelect
AS
    declare @login varchar(40);
    select @login = SYSTEM_USER
	SELECT 
		pr.Id, 
		pr.ProjectName
	FROM Projects pr
	inner join Employees emp on emp.Id = pr.ManagerId
	WHERE emp.Login = @login
GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectForSelect
AS
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectForSelect
AS
--nic nie robi
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectForSelect
AS
--nic nie robi
GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjectForSelect TO BasicRole
GRANT EXECUTE ON updateProjectForSelect TO BasicRole
GRANT EXECUTE ON insertProjectForSelect TO BasicRole
GRANT EXECUTE ON deleteProjectForSelect TO BasicRole
use szpifDatabase

