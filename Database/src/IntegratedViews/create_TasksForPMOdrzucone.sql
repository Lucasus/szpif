use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMOdrzucone') IS NOT NULL
	DROP PROCEDURE getTasksForPMOdrzucone
GO
IF OBJECT_ID('updateTasksForPMOdrzucone') IS NOT NULL
	DROP PROCEDURE updateTasksForPMOdrzucone
GO
IF OBJECT_ID('insertTasksForPMOdrzucone') IS NOT NULL
	DROP PROCEDURE insertTasksForPMOdrzucone
GO
IF OBJECT_ID('deleteTasksForPMOdrzucone') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMOdrzucone
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getTasksForPMOdrzucone
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
  and Status like('Odrzucone')

--  from Projects pr 
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMOdrzucone
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
CREATE PROCEDURE insertTasksForPMOdrzucone
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
CREATE PROCEDURE deleteTasksForPMOdrzucone
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOdrzucone','Status','Task State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getTasksForPMOdrzucone TO BasicRole
GRANT EXECUTE ON updateTasksForPMOdrzucone TO BasicRole
GRANT EXECUTE ON insertTasksForPMOdrzucone TO BasicRole
GRANT EXECUTE ON deleteTasksForPMOdrzucone TO BasicRole
use szpifDatabase
