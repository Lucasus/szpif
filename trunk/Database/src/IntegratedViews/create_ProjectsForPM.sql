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

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getProjectsForPM
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
  from Projects pr where ManagerId in (select Id from Employees where Login = @login)
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsForPM
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
						where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsForPM
AS
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsForPM
AS
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjectsForPM TO BasicRole
GRANT EXECUTE ON updateProjectsForPM TO BasicRole
GRANT EXECUTE ON insertProjectsForPM TO BasicRole
GRANT EXECUTE ON deleteProjectsForPM TO BasicRole
use szpifDatabase

