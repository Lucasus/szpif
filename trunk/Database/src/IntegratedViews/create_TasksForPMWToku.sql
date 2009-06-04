use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMWToku') IS NOT NULL
	DROP PROCEDURE getTasksForPMWToku
GO
IF OBJECT_ID('updateTasksForPMWToku') IS NOT NULL
	DROP PROCEDURE updateTasksForPMWToku
GO
IF OBJECT_ID('insertTasksForPMWToku') IS NOT NULL
	DROP PROCEDURE insertTasksForPMWToku
GO
IF OBJECT_ID('deleteTasksForPMWToku') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMWToku
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPMWToku
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
   and Status like('W Toku')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMWToku
  @Id					int,
  @TaskName				nvarchar(100),
  @MaxHours				int,
  @ExpectedEndDate		datetime,
  @Bonus				int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))

UPDATE Tasks   
	SET
      [TaskName] = @TaskName, 
      [MaxHours] = @MaxHours, 
      [ExpectedEndDate] = @ExpectedEndDate, 
      [Bonus] = @Bonus,
      [Status] = @Status 
	where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPMWToku
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMWToku
WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMWToku','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMWToku','EmployeeId', 'Link', 'EmployeeForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMWToku','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPMWToku TO BasicRole
GRANT EXECUTE ON updateTasksForPMWToku TO BasicRole
GRANT EXECUTE ON insertTasksForPMWToku TO BasicRole
GRANT EXECUTE ON deleteTasksForPMWToku TO BasicRole
use szpifDatabase

