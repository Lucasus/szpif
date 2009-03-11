Use szpifDatabase
DROP VIEW dbo.EmployeeAdministrationView
GO
CREATE VIEW dbo.EmployeeAdministrationView
AS
SELECT DISTINCT em.Id, Login, Name,
           dbo.agregatePermissionsFunction (em.Id) AS Uprawnienia
FROM Employees em
		inner join [Permissions] perm on em.Id = perm.EmployeeId