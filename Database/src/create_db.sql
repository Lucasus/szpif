/* BUILD A DATABASE */

-- This is the main caller for each script
SET NOCOUNT ON
GO

PRINT 'CREATING DATABASE'
--IF EXISTS (SELECT 1 FROM SYS.DATABASES WHERE NAME = 'szpifDatabase')
--   DROP DATABASE szpifDatabase
--GO
--CREATE DATABASE szpifDatabase
GO
Use szpifDatabase
GO
:On Error exit
:r src\remove_all.sql
GO
PRINT 'ALL HAVE BEEN REMOVED';
--:r src\create_regularExpressions.sql
PRINT 'REGULAR EXPRESSIONS HAVE BEEN CREATED'
:r src\create_tables.sql
PRINT 'TABLES HAVE BEEN CREATED'
:r src\create_functions.sql
PRINT 'FUNCTIONS HAVE BEEN CREATED'
:r src\create_views.sql
PRINT 'VIEWS HAVE BEEN CREATED'
:r src\create_procedures.sql
PRINT 'PROCEDURES HAVE BEEN CREATED'
:r src\create_triggers.sql
PRINT 'TRIGGERS HAVE BEEN CREATED'
:r src\create_roles.sql
PRINT 'ROLES HAVE BEEN CREATED'
:r src\IntegratedViews\create_Employees.sql
:r src\IntegratedViews\create_ProjectsNowy.sql
:r src\IntegratedViews\create_ProjectsWToku.sql
:r src\IntegratedViews\create_ProjectsZakonczony.sql
:r src\IntegratedViews\create_ProjectsForPMWToku.sql
:r src\IntegratedViews\create_ProjectsForPMZakonczony.sql
:r src\IntegratedViews\create_TasksForPMNowe.sql
:r src\IntegratedViews\create_TasksForPMOczekujace.sql
:r src\IntegratedViews\create_TasksForPMOdrzucone.sql
:r src\IntegratedViews\create_TasksForPMWToku.sql
:r src\IntegratedViews\create_TasksForPMOczekujaceDecyzje.sql
:r src\IntegratedViews\create_TasksForPMNiewykonane.sql
:r src\IntegratedViews\create_TasksForPMZakonczone.sql
:r src\IntegratedViews\create_TasksForWorkerWToku.sql
:r src\IntegratedViews\create_TasksForWorkerOczekujaceDecyzje.sql
:r src\IntegratedViews\create_TasksForWorkerNiewykonane.sql
:r src\IntegratedViews\create_TasksForWorkerZakonczone.sql
:r src\IntegratedViews\create_EmployeesForUser.sql
:r src\IntegratedViews\create_PrzelozeniForSelect.sql
:r src\IntegratedViews\create_PMForSelect.sql
PRINT 'DATABASE CREATE IS COMPLETE'
:r src\generate_data.sql
PRINT 'GENERATING DATA IS COMPLETE'
:r src\show_db.sql
PRINT 'COMPLETED'
GO
