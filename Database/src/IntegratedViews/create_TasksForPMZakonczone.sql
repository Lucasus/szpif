use szpifDatabase
PRINT 'CREATING TasksForPM VIEW...'
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-----------------Usuwamy poprzednie wersje---------------------
IF OBJECT_ID('getTasksForPMZakonczone') IS NOT NULL
	DROP PROCEDURE getTasksForPMZakonczone
GO
IF OBJECT_ID('updateTasksForPMZakonczone') IS NOT NULL
	DROP PROCEDURE updateTasksForPMZakonczone
GO
IF OBJECT_ID('insertTasksForPMZakonczone') IS NOT NULL
	DROP PROCEDURE insertTasksForPMZakonczone
GO
IF OBJECT_ID('deleteTasksForPMZakonczone') IS NOT NULL
	DROP PROCEDURE deleteTasksForPMZakonczone
GO

----------Procedura zwracaj�ca widok------------------------------
CREATE PROCEDURE getTasksForPMZakonczone
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
 and Status like('Zako�czone')

--  from Projects pr 
 GO

---------Procedura update'uj�ca rekordy z widoku------------------
CREATE PROCEDURE updateTasksForPMZakonczone
  @Id					int,
  @Status				nvarchar(100)
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))    
GO
---------Procedura dodaj�ca rekord do widoku---------------------
CREATE PROCEDURE insertTasksForPMZakonczone
AS
--	declare @przelId int;
--	select @przelId = (SELECT nref.value('@Id[1]', 'int') Id
--	from @PM.nodes('//Link') AS R(nref))
GO


GO
---------Procedura usuwaj�ca rekord z widoku--------------------- 
CREATE PROCEDURE deleteTasksForPMZakonczone
	@Id	int
WITH EXECUTE AS  'szpifadmin'
AS
  DELETE FROM Tasks where Id = @Id
GO
---------Przypisywanie schemat�w do niestandardowych typ�w danych-------------
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMZakonczone','ProjectId', 'Link', 'ProjectForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMZakonczone','EmployeeId', 'Link', 'EmployeeForSelect');
INSERT INTO [ColumnsToTypes] VALUES ('TasksForPMZakonczone','Status','Task State', null);

GO
---------Nadawanie uprawnie�-------------------------------------
GRANT EXECUTE ON    getTasksForPMZakonczone TO BasicRole
GRANT EXECUTE ON updateTasksForPMZakonczone TO BasicRole
GRANT EXECUTE ON insertTasksForPMZakonczone TO BasicRole
GRANT EXECUTE ON deleteTasksForPMZakonczone TO BasicRole
use szpifDatabase

