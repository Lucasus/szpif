use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForWorkerOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE getTasksForWorkerOczekujaceDecyzje
GO
IF OBJECT_ID('updateTasksForWorkerOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE updateTasksForWorkerOczekujaceDecyzje
GO
IF OBJECT_ID('insertTasksForWorkerOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE insertTasksForWorkerOczekujaceDecyzje
GO
IF OBJECT_ID('deleteTasksForWorkerOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE deleteTasksForWorkerOczekujaceDecyzje
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getTasksForWorkerOczekujaceDecyzje
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT Id
      ,[EmployeeId]
      ,[ProjectId]
      ,[TaskName]
      ,[MaxHours]
      ,[StartDate]
      ,[ExpectedEndDate]
      ,[Bonus]
      ,[Status]
  FROM Tasks
  where EmployeeId in (select Id from Employees where Login = @login)
  --where ProjectId in (select Id from Projects where ManagerId in (select Id from Employees where Login = @login)) 
  and Status like('Oczekuj�ce na decyzj�')

--  from Projects pr 
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForWorkerOczekujaceDecyzje
  @Id					int,
  @EmployeeId			int,
  @ProjectId			int,
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

UPDATE Tasks   
	SET EmployeeId = @EmployeeId, 
      ProjectId = @ProjectId, 
      [TaskName] = @TaskName, 
      [MaxHours] = @MaxHours, 
      [StartDate] = @StartDate, 
      [ExpectedEndDate] = @ExpectedEndDate, 
      [Bonus] = @Bonus,
      [Status] = @Status 
	where Id = @Id        
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForWorkerOczekujaceDecyzje
  @Id					int,
  @EmployeeId			int,
  @ProjectId			int,
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
           (@EmployeeId, 
           @ProjectId, 
           @Status, 
           @TaskName,
           @MaxHours, 
           @StartDate, 
           @ExpectedEndDate, 
           @Bonus)
GO


GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForWorkerOczekujaceDecyzje
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getTasksForWorkerOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON updateTasksForWorkerOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON insertTasksForWorkerOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON deleteTasksForWorkerOczekujaceDecyzje TO BasicRole
use szpifDatabase

