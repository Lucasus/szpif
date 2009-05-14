use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPM') IS NOT NULL
	DROP PROCEDURE getTasksForPM
GO
IF OBJECT_ID('updateTasksForPM') IS NOT NULL
	DROP PROCEDURE updateTasksForPM
GO
IF OBJECT_ID('insertTasksForPM') IS NOT NULL
	DROP PROCEDURE insertTasksForPM
GO
IF OBJECT_ID('deleteTasksForPM') IS NOT NULL
	DROP PROCEDURE deleteTasksForPM
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPM
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT [Id]
      ,[EmployeeId]
      ,[ProjectId]
      ,[TaskStatusId]
      ,[TaskName]
      ,[MaxHours]
      ,[StartDate]
      ,[ExpectedEndDate]
      ,[Bonus]
  FROM Tasks
  where ProjectId in (select Id from Projects where ManagerId in (select Id from Employees where Login = @login))


--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPM
  @Id					int,
  @EmployeeId			int,
  @ProjectId			int,
  @TaskStatusId			int,
  @TaskName				nvarchar(100),
  @MaxHours				int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Bonus				int
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))

UPDATE Tasks   
	SET EmployeeId = @EmployeeId, 
      ProjectId = @ProjectId, 
      [TaskStatusId] = @TaskStatusId, 
      [TaskName] = @TaskName, 
      [MaxHours] = @MaxHours, 
      [StartDate] = @StartDate, 
      [ExpectedEndDate] = @ExpectedEndDate, 
      [Bonus] = @Bonus
	where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPM
  @Id					int,
  @EmployeeId			int,
  @ProjectId			int,
  @TaskStatusId			int,
  @TaskName				nvarchar(100),
  @MaxHours				int,
  @StartDate			datetime,
  @ExpectedEndDate		datetime,
  @Bonus				int
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
	INSERT INTO [Tasks]
           ([EmployeeId]
           ,[ProjectId]
           ,[TaskStatusId]
           ,[TaskName]
           ,[MaxHours]
           ,[StartDate]
           ,[ExpectedEndDate]
           ,[Bonus])
     VALUES
           (@EmployeeId, 
           @ProjectId, 
           @TaskStatusId, 
           @TaskName,
           @MaxHours, 
           @StartDate, 
           @ExpectedEndDate, 
           @Bonus)
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPM
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPM TO BasicRole
GRANT EXECUTE ON updateTasksForPM TO BasicRole
GRANT EXECUTE ON insertTasksForPM TO BasicRole
GRANT EXECUTE ON deleteTasksForPM TO BasicRole
use szpifDatabase

