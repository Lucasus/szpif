use szpifDatabase
PRINT 'CREATING ProjectsForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getProjectsForPM') IS NOT NULL
	DROP PROCEDURE getProjectsForPMWToku
GO
IF OBJECT_ID('updateProjectsForPM') IS NOT NULL
	DROP PROCEDURE updateProjectsForPMWToku
GO
IF OBJECT_ID('insertProjectsForPM') IS NOT NULL
	DROP PROCEDURE insertProjectsForPMWToku
GO
IF OBJECT_ID('deleteProjectsForPM') IS NOT NULL
	DROP PROCEDURE deleteProjectsForPMWToku
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getProjectsForPMWToku
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  select	Id, 
		dbo.EmployeeToXmlLink(pr.ManagerId, 'PM', 'PMForSelect') AS  'PM',
		ProjectName,
		MaxHours,
		MaxBudget,
		StartDate,
		ExpectedEndDate,
		Status
  from Projects pr 
  where ManagerId in (select Id from Employees where Login = @login)
  and Status like('W Toku')
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsForPMWToku
  @Id					int,
  @PM					xml,
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Status				nvarchar(100)
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

    update Projects set ManagerId = @przelId, 
						Status = @Status,
						ProjectName = @ProjectName, 
						MaxHours = @MaxHours,
						MaxBudget = @MaxBudget,
						StartDate = @StartDate,
						ExpectedEndDate = @ExpectedEndDate
						where Id = @Id and Status like ('W Toku')
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsForPMWToku
AS
GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsForPMWToku
AS
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPMWToku','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getProjectsForPMWToku TO BasicRole
GRANT EXECUTE ON updateProjectsForPMWToku TO BasicRole
GRANT EXECUTE ON insertProjectsForPMWToku TO BasicRole
GRANT EXECUTE ON deleteProjectsForPMWToku TO BasicRole
use szpifDatabase

