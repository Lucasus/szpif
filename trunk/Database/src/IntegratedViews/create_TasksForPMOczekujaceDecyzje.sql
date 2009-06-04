use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE getTasksForPMOczekujaceDecyzje
GO
IF OBJECT_ID('updateTasksForPMOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE updateTasksForPMOczekujaceDecyzje
GO
IF OBJECT_ID('insertTasksForPMOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE insertTasksForPMOczekujaceDecyzje
GO
IF OBJECT_ID('deleteTasksForPMOczekujaceDecyzje') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMOczekujaceDecyzje
GO

----------Procedura zwracaj¹ca widok------------------------------
CREATE PROCEDURE getTasksForPMOczekujaceDecyzje
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
  and Status like('Oczekuj¹ce na decyzjê PM')

--  from Projects pr 
 GO

---------Procedura update'uj¹ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMOczekujaceDecyzje
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
CREATE PROCEDURE insertTasksForPMOczekujaceDecyzje
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj¹ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMOczekujaceDecyzje
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
 -- DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schematów do niestandardowych typów danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOczekujaceDecyzje','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOczekujaceDecyzje','EmployeeId', 'Link', 'EmployeeForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOczekujaceDecyzje','Status','Task State', null);

GO
---------Nadawanie uprawnieñ-------------------------------------
GRANT EXECUTE ON    getTasksForPMOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON updateTasksForPMOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON insertTasksForPMOczekujaceDecyzje TO BasicRole
GRANT EXECUTE ON deleteTasksForPMOczekujaceDecyzje TO BasicRole
use szpifDatabase

