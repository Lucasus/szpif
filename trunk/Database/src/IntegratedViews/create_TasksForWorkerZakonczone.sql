use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForWorkerZakonczone') IS NOT NULL
	DROP PROCEDURE getTasksForWorkerZakonczone
GO
IF OBJECT_ID('updateTasksForWorkerZakonczone') IS NOT NULL
	DROP PROCEDURE updateTasksForWorkerZakonczone
GO
IF OBJECT_ID('insertTasksForWorkerZakonczone') IS NOT NULL
	DROP PROCEDURE insertTasksForWorkerZakonczone
GO
IF OBJECT_ID('deleteTasksForWorkerZakonczone') IS NOT NULL
	DROP PROCEDURE deleteTasksForWorkerZakonczone
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForWorkerZakonczone
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
  and tr.Status like('Zakoñczone')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForWorkerZakonczone
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))    
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForWorkerZakonczone
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForWorkerZakonczone
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForWorkerZakonczone','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForWorkerZakonczone TO BasicRole
GRANT EXECUTE ON updateTasksForWorkerZakonczone TO BasicRole
GRANT EXECUTE ON insertTasksForWorkerZakonczone TO BasicRole
GRANT EXECUTE ON deleteTasksForWorkerZakonczone TO BasicRole
use szpifDatabase

