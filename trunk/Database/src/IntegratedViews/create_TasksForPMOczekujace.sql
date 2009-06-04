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

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getTasksForPMOczekujace
AS
  declare @login varchar(40);
  select @login = SYSTEM_USER
  SELECT tr.Id
	  ,dbo.EmployeeToXmlLink(tr.EmployeeId, 'Imi� Podw�adnego', 'EmployeeForSelect') AS  'EmployeeId'
	  ,dbo.ProjectToXmlLink(tr.ProjectId, 'Nazwa Projektu', 'ProjectForSelect') AS  'ProjectId'
      ,tr.[TaskName]-- AS 'Nazwa Zadania'
      ,tr.[MaxHours]-- AS 'Maksymalna ilo�� godzin'
      ,tr.[StartDate]-- AS 'Data Rozpocz�cia'
      ,tr.[ExpectedEndDate]-- As 'Oczekiwana Data Zako�czenia'
      ,tr.[Bonus] AS 'Bonus'
      ,tr.[Status] AS 'Status'
  FROM Tasks AS tr
  --inner join Projects AS pr on tr.ProjectId = pr.Id
  --inner join Employees AS emp on tr.EmployeeId = emp.Id
  where tr.ProjectId in (select ProjectId from Projects where ManagerId in (select Id from Employees where Login = @login)) 
 and Status like('Oczekuj�ce na akceptacj�')

--  from Projects pr 
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMOczekujace
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
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPMOczekujace
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMOczekujace

WITH EXECUTE AS  'szpifadmin'
AS
  --DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOczekujace','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOczekujace','EmployeeId', 'Link', 'EmployeeForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMOczekujace','Status','Task State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getTasksForPMOczekujace TO BasicRole
GRANT EXECUTE ON updateTasksForPMOczekujace TO BasicRole
GRANT EXECUTE ON insertTasksForPMOczekujace TO BasicRole
GRANT EXECUTE ON deleteTasksForPMOczekujace TO BasicRole
use szpifDatabase

