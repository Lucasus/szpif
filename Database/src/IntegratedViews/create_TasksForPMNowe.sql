use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMNowe') IS NOT NULL
	DROP PROCEDURE getTasksForPMNowe
GO
IF OBJECT_ID('updateTasksForPMNowe') IS NOT NULL
	DROP PROCEDURE updateTasksForPMNowe
GO
IF OBJECT_ID('insertTasksForPMNowe') IS NOT NULL
	DROP PROCEDURE insertTasksForPMNowe
GO
IF OBJECT_ID('deleteTasksForPMNowe') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMNowe
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPMNowe
AS
  declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT tr.Id
	  ,dbo.EmployeeToXmlLink(tr.EmployeeId, 'Imiê Podw³adnego', 'EmployeeForSelect') AS  'EmployeeId'
	  ,dbo.ProjectToXmlLink(tr.ProjectId, 'Nazwa Projektu', 'ProjectForSelect') AS  'ProjectId'
      ,tr.[TaskName]-- AS 'Nazwa Zadania'
      ,tr.[MaxHours]-- AS 'Maksymalna iloœæ godzin'
      ,tr.[StartDate]-- AS 'Data Rozpoczêcia'
      ,tr.[ExpectedEndDate]-- As 'Oczekiwana Data Zakoñczenia'
      ,tr.[Bonus] AS 'Bonus'
      ,tr.[Status] AS 'Status'
  FROM Tasks AS tr
  --inner join Projects AS pr on tr.ProjectId = pr.Id
  --inner join Employees AS emp on tr.EmployeeId = emp.Id
  where tr.ProjectId in (select ProjectId from Projects where ManagerId in (select Id from Employees where Login = @login)) 
 and tr.Status like('Nowe')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMNowe
  @Id					int,
  @EmployeeId			xml,
  @ProjectId			xml,
  @TaskName				nvarchar(100),
  @MaxHours				int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Bonus				int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
	declare @emplId int;
	select @emplId = (SELECT nref.value('@Id[1]', 'int') Id
	from @EmployeeId.nodes('//Link') AS R(nref))

	declare @projId int;
	select @projId = (SELECT nref.value('@Id[1]', 'int') Id
	from @ProjectId.nodes('//Link') AS R(nref))

UPDATE Tasks   
	SET EmployeeId = @emplId, 
      ProjectId = @projId, 
      [TaskName] = @TaskName, 
      [MaxHours] = @MaxHours, 
      [StartDate] = @StartDate, 
      [ExpectedEndDate] = @ExpectedEndDate, 
      [Bonus] = @Bonus,
      [Status] = @Status 
	where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPMNowe
  @Id					int,
  @EmployeeId			xml,
  @ProjectId			xml,
  @TaskName				nvarchar(100),
  @MaxHours				int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Bonus				int,
  @Status				nvarchar(100)
AS

	declare @emplId int;
	select @emplId = (SELECT nref.value('@Id[1]', 'int') Id
	from @EmployeeId.nodes('//Link') AS R(nref))

	declare @projId int;
	select @projId = (SELECT nref.value('@Id[1]', 'int') Id
	from @ProjectId.nodes('//Link') AS R(nref))


--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
	INSERT INTO [Tasks]
           ([EmployeeId]
           ,[ProjectId]
           ,[Status]
           ,[TaskName]
           ,[MaxHours]
           ,[StartDate]
           ,[ExpectedEndDate]
           ,[Bonus])
     VALUES
           (@emplId, 
           @projId, 
           @Status, 
           @TaskName,
           @MaxHours, 
           @StartDate, 
           @ExpectedEndDate, 
           @Bonus)
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMNowe
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMNowe','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMNowe','EmployeeId', 'Link', 'EmployeeForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMNowe','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPMNowe TO BasicRole
GRANT EXECUTE ON updateTasksForPMNowe TO BasicRole
GRANT EXECUTE ON insertTasksForPMNowe TO BasicRole
GRANT EXECUTE ON deleteTasksForPMNowe TO BasicRole
use szpifDatabase

