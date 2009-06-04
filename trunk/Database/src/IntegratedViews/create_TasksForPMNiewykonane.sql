use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMNiewykonane') IS NOT NULL
	DROP PROCEDURE getTasksForPMNiewykonane
GO
IF OBJECT_ID('updateTasksForPMNiewykonane') IS NOT NULL
	DROP PROCEDURE updateTasksForPMNiewykonane
GO
IF OBJECT_ID('insertTasksForPMNiewykonane') IS NOT NULL
	DROP PROCEDURE insertTasksForPMNiewykonane
GO
IF OBJECT_ID('deleteTasksForPMNiewykonane') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMNiewykonane
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPMNiewykonane
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
  and Status like('Niewykonane')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMNiewykonane
  @Id					int,
--  @EmployeeId			int,
--  @ProjectId			int,
--  @TaskName				nvarchar(100),
--  @MaxHours				int,
--  @StartDate			datetime,
--  @ExpectedEndDate		datetime,
--  @Bonus				int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))

UPDATE Tasks   
	SET 
--EmployeeId = @EmployeeId, 
--      ProjectId = @ProjectId, 
--      [TaskName] = @TaskName, 
--      [MaxHours] = @MaxHours, 
--      [StartDate] = @StartDate, 
--      [ExpectedEndDate] = @ExpectedEndDate, 
--      [Bonus] = @Bonus,
      [Status] = @Status 
	where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPMNiewykonane
 -- @Id					int,
 -- @EmployeeId			int,
 -- @ProjectId			int,
 -- @TaskName				nvarchar(100),
 -- @MaxHours				int,
 -- @StartDate			datetime,
 -- @ExpectedEndDate		datetime,
 -- @Bonus				int,
 -- @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
--	INSERT INTO [Tasks]
--           ([EmployeeId]
--           ,[ProjectId]
--           ,[Status]
--           ,[TaskName]
--           ,[MaxHours]
--           ,[StartDate]
--           ,[ExpectedEndDate]
--           ,[Bonus])
--     VALUES
--           (@EmployeeId, 
--           @ProjectId, 
--           @Status, 
--           @TaskName,
--           @MaxHours, 
--           @StartDate, 
--           @ExpectedEndDate, 
--           @Bonus)
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMNiewykonane
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMNiewykonane','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMNiewykonane','EmployeeId', 'Link', 'EmployeeForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMNiewykonane','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPMNiewykonane TO BasicRole
GRANT EXECUTE ON updateTasksForPMNiewykonane TO BasicRole
GRANT EXECUTE ON insertTasksForPMNiewykonane TO BasicRole
GRANT EXECUTE ON deleteTasksForPMNiewykonane TO BasicRole
use szpifDatabase

