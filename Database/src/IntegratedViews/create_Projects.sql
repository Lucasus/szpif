use szpifDatabase
PRINT 'CREATING PROJECTS VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjects') IS NOT NULL
	DROP PROCEDURE getProjects
GO
IF OBJECT_ID('updateProjects') IS NOT NULL
	DROP PROCEDURE updateProjects
GO
IF OBJECT_ID('insertProjects') IS NOT NULL
	DROP PROCEDURE insertProjects
GO
IF OBJECT_ID('deleteProjects') IS NOT NULL
	DROP PROCEDURE deleteProjects
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getProjects
AS
	SELECT 
		Id, 
		ProjectName,
		dbo.EmployeeToXmlLink(pr.ManagerId) AS  'PM'
	FROM Projects pr
GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateProjects
  @Id			int,
  @Name			nvarchar(40),
  @PM			xml
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

    update Projects set ProjectName = @Name, ManagerId = @przelId where Id = @Id        
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertProjects
  @Name			nvarchar(40),
  @PM			xml
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))
	
	INSERT INTO [Projects] (ManagerId, OrderId, ProjectStatusId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate) VALUES (@przelId, 1, 1 , @Name , 1, 1, 1, 1);
GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjects
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('Projects','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getProjects TO OwnerRole
GRANT EXECUTE ON updateProjects TO OwnerRole
GRANT EXECUTE ON insertProjects TO OwnerRole
GRANT EXECUTE ON deleteProjects TO OwnerRole
use szpifDatabase

