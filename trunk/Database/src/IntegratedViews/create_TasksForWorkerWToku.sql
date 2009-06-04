use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForWorkerWToku') IS NOT NULL
	DROP PROCEDURE getTasksForWorkerWToku
GO
IF OBJECT_ID('updateTasksForWorkerWToku') IS NOT NULL
	DROP PROCEDURE updateTasksForWorkerWToku
GO
IF OBJECT_ID('insertTasksForWorkerWToku') IS NOT NULL
	DROP PROCEDURE insertTasksForWorkerWToku
GO
IF OBJECT_ID('deleteTasksForWorkerWToku') IS NOT NULL
	DROP PROCEDURE deleteTasksForWorkerWToku
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getTasksForWorkerWToku
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT tr.Id
      ,pr.ProjectName AS 'Nazwa Projektu'
      ,tr.[TaskName] AS 'Nazwa Zadania'
      ,tr.[MaxHours] AS 'Maksymalna ilo�� godzin'
      ,tr.[StartDate] AS 'Data Rozpocz�cia'
      ,tr.[ExpectedEndDate] As 'Oczekiwana Data Zako�czenia'
      ,tr.[Bonus] AS 'Bonus'
      ,tr.[Status] AS 'Status'
  FROM Tasks AS tr
  inner join Projects AS pr on tr.ProjectId = pr.Id
  where EmployeeId in (select Id from Employees where Login = @login)
  --where ProjectId in (select Id from Projects where ManagerId in (select Id from Employees where Login = @login)) 
  and tr.Status like('W Toku')

--  from Projects pr 
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForWorkerWToku
  @Id					int,
  @Status				nvarchar(100)
AS
UPDATE Tasks   
	SET 
      [Status] = @Status 
	where Id = @Id        
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForWorkerWToku
AS
GO


GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForWorkerWToku
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForWorkerWToku','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForWorkerWToku','Status','Task State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getTasksForWorkerWToku TO BasicRole
GRANT EXECUTE ON updateTasksForWorkerWToku TO BasicRole
GRANT EXECUTE ON insertTasksForWorkerWToku TO BasicRole
GRANT EXECUTE ON deleteTasksForWorkerWToku TO BasicRole
use szpifDatabase

