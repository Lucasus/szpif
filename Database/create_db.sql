/* BUILD A DATABASE */

-- This is the main caller for each script
SET NOCOUNT ON
GO

--PRINT 'CREATING DATABASE'
--IF EXISTS (SELECT 1 FROM SYS.DATABASES WHERE NAME = 'SZPIFDATABASE')
--   DROP DATABASE SZPIFDATABASE
--GO
--CREATE DATABASE SZPIFDATABASE
--GO

:On Error exit

:r create_tables.sql
:r create_functions.sql
:r create_views.sql
:r create_procedures.sql
:r create_roles.sql
PRINT 'DATABASE CREATE IS COMPLETE'
:r generate_data.sql
PRINT 'GENERATING DATA IS COMPLETE'
:r show_db.sql
GO