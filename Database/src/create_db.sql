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

:r src\create_tables.sql
:r src\create_functions.sql
:r src\create_views.sql
:r src\create_procedures.sql
:r src\create_triggers.sql
:r src\create_roles.sql
:r src\IntegratedViews\create_Employees.sql
:r src\IntegratedViews\create_Projects.sql
:r src\IntegratedViews\create_EmployeesForUser.sql
:r src\IntegratedViews\create_PrzelozeniForSelect.sql
PRINT 'DATABASE CREATE IS COMPLETE'
:r src\generate_data.sql
:r src\show_db.sql
PRINT 'GENERATING DATA IS COMPLETE'
GO
