use szpifDatabase
PRINT 'CREATING ProjectsForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectsForPM') IS NOT NULL
	DROP PROCEDURE getProjectsForPM
GO
IF OBJECT_ID('updateProjectsForPM') IS NOT NULL
	DROP PROCEDURE updateProjectsForPM
GO
IF OBJECT_ID('insertProjectsForPM') IS NOT NULL
	DROP PROCEDURE insertProjectsForPM
GO
IF OBJECT_ID('deleteProjectsForPM') IS NOT NULL
	DROP PROCEDURE deleteProjectsForPM
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getProjectsForPM
AS
    declare @login varchar(40);
    select @login = SYSTEM_USER
	SELECT 
		Id, 
		ProjectName,
		dbo.EmployeeToXmlLink(pr.ManagerId) AS  'PM'
	FROM Projects pr
GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsForPM
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
CREATE PROCEDURE insertProjectsForPM
  @Name			nvarchar(40),
  @PM			xml
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))
	
	INSERT INTO [Projects] (ManagerId, OrderId, ProjectstatusId, ProjectName, MaxHours, MaxBudget, StartDate, ExpectedEndDate) VALUES (@przelId, 1, 1 , @Name , 1, 1, 1, 1);
GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsForPM
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getProjectsForPM TO OwnerRole
GRANT EXECUTE ON updateProjectsForPM TO OwnerRole
GRANT EXECUTE ON insertProjectsForPM TO OwnerRole
GRANT EXECUTE ON deleteProjectsForPM TO OwnerRole
use szpifDatabase

