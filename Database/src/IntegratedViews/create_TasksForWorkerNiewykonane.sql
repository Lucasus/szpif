use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForWorkerNiewykonane') IS NOT NULL
	DROP PROCEDURE getTasksForWorkerNiewykonane
GO
IF OBJECT_ID('updateTasksForWorkerNiewykonane') IS NOT NULL
	DROP PROCEDURE updateTasksForWorkerNiewykonane
GO
IF OBJECT_ID('insertTasksForWorkerNiewykonane') IS NOT NULL
	DROP PROCEDURE insertTasksForWorkerNiewykonane
GO
IF OBJECT_ID('deleteTasksForWorkerNiewykonane') IS NOT NULL
	DROP PROCEDURE deleteTasksForWorkerNiewykonane
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForWorkerNiewykonane
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT tr.Id
      ,pr.ProjectName AS 'Nazwa Projektu'
      ,tr.[TaskName] AS 'Nazwa Zadania'
      ,tr.[MaxHours] AS 'Maksymalna iloœæ godzin'
      ,tr.[StartDate] AS 'Data Rozpoczêcia'
      ,tr.[ExpectedEndDate] As 'Oczekiwana Data Zakoñczenia'
      ,tr.[Bonus] AS 'Bonus'
      ,tr.[Status] AS 'Status'
  FROM Tasks AS tr
  inner join Projects AS pr on tr.ProjectId = pr.Id
  where EmployeeId in (select Id from Employees where Login = @login)
   --where ProjectId in (select Id from Projects where ManagerId in (select Id from Employees where Login = @login)) 
  and tr.Status like('Niewykonane')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForWorkerNiewykonane
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
  
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForWorkerNiewykonane
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForWorkerNiewykonane
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForWorkerNiewykonane','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForWorkerNiewykonane TO BasicRole
GRANT EXECUTE ON updateTasksForWorkerNiewykonane TO BasicRole
GRANT EXECUTE ON insertTasksForWorkerNiewykonane TO BasicRole
GRANT EXECUTE ON deleteTasksForWorkerNiewykonane TO BasicRole
use szpifDatabase

