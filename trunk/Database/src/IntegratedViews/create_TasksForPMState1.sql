use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPM') IS NOT NULL
	DROP PROCEDURE getTasksForPMState1
GO
IF OBJECT_ID('updateTasksForPM') IS NOT NULL
	DROP PROCEDURE updateTasksForPMState1
GO
IF OBJECT_ID('insertTasksForPM') IS NOT NULL
	DROP PROCEDURE insertTasksForPMState1
GO
IF OBJECT_ID('deleteTasksForPM') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMState1
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPMState1
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT task.Id
      ,[EmployeeId]
      ,[ProjectId]
      ,[TaskStatusId]
      ,[TaskName]
      ,[MaxHours]
      ,[StartDate]
      ,[ExpectedEndDate]
      ,[Bonus]
  FROM Tasks task
  INNER JOIN TaskStatus stat ON task.TaskStatusId = stat.Id
  where ProjectId in (select Id from Projects where ManagerId in (select Id from Employees where Login = @login)) 
  and stat.Status like('W Toku')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMState1
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
CREATE PROCEDURE insertTasksForPMState1
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
CREATE PROCEDURE deleteTasksForPMState1
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPMState1 TO BasicRole
GRANT EXECUTE ON updateTasksForPMState1 TO BasicRole
GRANT EXECUTE ON insertTasksForPMState1 TO BasicRole
GRANT EXECUTE ON deleteTasksForPMState1 TO BasicRole
use szpifDatabase

