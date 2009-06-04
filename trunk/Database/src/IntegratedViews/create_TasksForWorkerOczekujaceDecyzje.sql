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

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForWorkerOczekujaceDecyzje
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
  and tr.Status like('Oczekuj¹ce na decyzjê PM')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForWorkerOczekujaceDecyzje
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref)      
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForWorkerOczekujaceDecyzje
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForWorkerOczekujaceDecyzje
WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForWorkerOczekujaceDecyzje','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForWorkerOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON updateTasksForWorkerOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON insertTasksForWorkerOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON deleteTasksForWorkerOczekujaceDecyzje TO BasicRole
use szpifDatabase

