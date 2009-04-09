use master
CREATE LOGIN Lukasz
    WITH PASSWORD = 'Lukasz123'
GO
use szpifDatabase
CREATE USER Lukasz 
   FOR LOGIN Lukasz;
GO 
