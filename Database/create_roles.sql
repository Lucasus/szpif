/*
--Fragment Kodu odpowiedzialny za przyznawanie uprawnie�
--M�j pomys� jest taki: Tworzymy u�ytkownik�w o bardzo �ci�le wyznaczonych mo�liwo�ciach, Do ka�dej zak�adki dopisujemy
--stringa kt�ry okre�la dla kt�rego profilu przeznaczony jest dany element interface'u.
--Przy wywo�ywaniu metody z gui podawany jest ten string, jako argument i on jest u�ywany jako u�ytkownik wywo�uj�cy
--zapytanie z bazy.
*/
PRINT 'CREATING ROLES...'
GO

EXEC sp_droprolemember 'OwnerRole', 'Lukasz'
EXEC sp_droprolemember 'BasicRole', 'Lukasz'
DROP ROLE BasicRole
DROP ROLE OwnerRole
CREATE ROLE BasicRole
CREATE ROLE OwnerRole

GRANT EXECUTE ON getEmployeeViewForAdministration TO OwnerRole
GRANT EXECUTE ON getRolesViewForCurrentUser TO BasicRole
GRANT EXECUTE ON updateEmployeeViewForAdministration TO OwnerRole
EXEC sp_addrolemember 'OwnerRole', 'Lukasz'
EXEC sp_addrolemember 'BasicRole', 'Lukasz'
-- Tutaj mamy ka�dego u�ytkownika.
--EXEC sp_droprolemember 'EveryUser', 'GenericEveryUser'
--EXEC sp_droprolemember 'EveryUser', 'GenericEmployer'
--EXEC sp_droprolemember 'EveryUser', 'GenericEmployee'
--DROP ROLE EveryUser;
--CREATE ROLE EveryUser;
--GRANT EXECUTE ON checkPermissions TO EveryUser;
--GRANT EXECUTE ON changePassword TO EveryUser;
--GRANT EXECUTE ON changeEmail TO EveryUser;

--DROP USER GenericEveryUser;
--CREATE USER GenericEveryUser WITHOUT LOGIN;
--EXEC sp_addrolemember 'EveryUser', 'GenericEveryUser'

--GO
--Tutaj mamy W�a�ciciela
--EXEC sp_droprolemember 'Employer', 'GenericEmployer'
--DROP ROLE Employer;
--CREATE ROLE Employer;
--GRANT EXECUTE ON changePassword TO Employer;
--GRANT SELECT ON EmployeeAdministrationView TO Employer;
--GRANT SELECT (Login, Id) ON Employees TO Employer; 

--DROP USER GenericEmployer;
--CREATE USER GenericEmployer WITHOUT LOGIN;
--EXEC sp_addrolemember 'Employer', 'GenericEmployer'
--EXEC sp_addrolemember 'EveryUser', 'GenericEmployer' 

--GO
--Tutaj mamy szeregowego Pracownika
--EXEC sp_droprolemember 'Employee', 'GenericEmployee'
--DROP ROLE Employee;
--CREATE ROLE Employee;

--DROP USER GenericEmployee;
--CREATE USER GenericEmployee WITHOUT LOGIN;
--EXEC sp_addrolemember 'Employee', 'GenericEmployee'
--EXEC sp_addrolemember 'EveryUser', 'GenericEmployee' 
--GO

