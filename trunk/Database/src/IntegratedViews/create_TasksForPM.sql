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
		dbo.EmployeeToXmlLink(pr.ManagerId) AS  'PM',
		OrderId,
		ProjectStatusId,
		ProjectName,
		MaxHours,
		MaxBudget,
		StartDate,
		ExpectedEndDate
  from Projects pr where ManagerId in (select Id from Employees where Login = @login)
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateProjectsForPM
  @Id					int,
  @PM					xml,
  @ProjectStatusId		int,  
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))

    update Projects set ManagerId = @przelId, 
						OrderId = @OrderId,
						ProjectStatusId = @ProjectStatusId,
						ProjectName = @ProjectName, 
						MaxHours = @MaxHours,
						MaxBudget = @MaxBudget,
						StartDate = @StartDate,
						ExpectedEndDate = @ExpectedEndDate
						where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertProjectsForPM
  @Id					int,
  @PM					xml,
  @ProjectStatusId		int,  
  @ProjectName			nvarchar(40),
  @MaxHours				int,
  @MaxBudget			int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime
AS
	declare @przelId int;
	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
	from @PM.nodes('//Link') AS R(nref))
	
INSERT INTO [szpifDatabase].[dbo].[Projects]
           ([ManagerId]
           ,null
           ,[ProjectStatusId]
           ,[ProjectName]
           ,[MaxHours]
           ,[MaxBudget]
           ,[StartDate]
           ,[ExpectedEndDate])
     VALUES
           (@przelId, 
           @OrderId, 
           @ProjectStatusId, 
           @ProjectName, 
           @MaxHours, 
           @MaxBudget, 
           @StartDate, 
           @ExpectedEndDate)
GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteProjectsForPM
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Projects where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('ProjectsForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getProjectsForPM TO OwnerRole
GRANT EXECUTE ON updateProjectsForPM TO OwnerRole
GRANT EXECUTE ON insertProjectsForPM TO OwnerRole
GRANT EXECUTE ON deleteProjectsForPM TO OwnerRole
use szpifDatabase

