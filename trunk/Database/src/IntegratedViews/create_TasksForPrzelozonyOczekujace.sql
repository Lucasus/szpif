use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPrzelozonyOczekujace') IS NOT NULL
	DROP PROCEDURE getTasksForPrzelozonyOczekujace
GO
IF OBJECT_ID('updateTasksForPrzelozonyOczekujace') IS NOT NULL
	DROP PROCEDURE updateTasksForPrzelozonyOczekujace
GO
IF OBJECT_ID('insertTasksForPrzelozonyOczekujace') IS NOT NULL
	DROP PROCEDURE insertTasksForPrzelozonyOczekujace
GO
IF OBJECT_ID('deleteTasksForPrzelozonyOczekujace') IS NOT NULL
	DROP PROCEDURE deleteTasksForPrzelozonyOczekujace
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPrzelozonyOczekujace
AS
 declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT tr.Id
	  ,emp.Login AS 'Imiê Podw³adnego'
      ,pr.ProjectName AS 'Nazwa Projektu'
      ,tr.[TaskName] AS 'Nazwa Zadania'
      ,tr.[MaxHours] AS 'Maksymalna iloœæ godzin'
      ,tr.[StartDate] AS 'Data Rozpoczêcia'
      ,tr.[ExpectedEndDate] As 'Oczekiwana Data Zakoñczenia'
      ,tr.[Bonus] AS 'Bonus'
      ,tr.[Status] AS 'Status'
  FROM Tasks AS tr
  inner join Projects AS pr on tr.ProjectId = pr.Id
  inner join Employees AS emp on tr.EmployeeId = emp.Id
  where EmployeeId in (select Id from Employees where SuperiorId in (select Id from Employees where Login = @login)) 
  and tr.Status like('Oczekuj¹ce na akceptacjê')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPrzelozonyOczekujace
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))

UPDATE Tasks   
	SET
      [Status] = @Status 
	where Id = @Id        
GO
---------Procedura dodaj¹ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPrzelozonyOczekujace
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPrzelozonyOczekujace
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  -- DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
--INSERT INTO [ColumnsToTypes] VALUES ('TasksForPM','PM', 'Link', 'PMForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPrzelozonyOczekujace','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPrzelozonyOczekujace TO BasicRole
GRANT EXECUTE ON updateTasksForPrzelozonyOczekujace TO BasicRole
GRANT EXECUTE ON insertTasksForPrzelozonyOczekujace TO BasicRole
GRANT EXECUTE ON deleteTasksForPrzelozonyOczekujace TO BasicRole
use szpifDatabase

