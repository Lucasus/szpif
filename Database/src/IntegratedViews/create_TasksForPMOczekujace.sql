use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMOczekujace') IS NOT NULL
	DROP PROCEDURE getTasksForPMOczekujace
GO
IF OBJECT_ID('updateTasksForPMOczekujace') IS NOT NULL
	DROP PROCEDURE updateTasksForPMOczekujace
GO
IF OBJECT_ID('insertTasksForPMOczekujace') IS NOT NULL
	DROP PROCEDURE insertTasksForPMOczekujace
GO
IF OBJECT_ID('deleteTasksForPMOczekujace') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMOczekujace
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPMOczekujace
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
  where ProjectId in (select Id from Projects where ManagerId in (select Id from Employees where Login = @login)) 
  and Status like('Oczekuj¹ce na akceptacjê')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMOczekujace
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
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPMOczekujace
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
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMOczekujace
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPMOczekujace TO BasicRole
GRANT EXECUTE ON updateTasksForPMOczekujace TO BasicRole
GRANT EXECUTE ON insertTasksForPMOczekujace TO BasicRole
GRANT EXECUTE ON deleteTasksForPMOczekujace TO BasicRole
use szpifDatabase

