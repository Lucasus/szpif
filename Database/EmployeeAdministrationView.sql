Use szpifDatabase
DROP VIEW dbo.EmployeeAdministrationView
GO
CREATE VIEW dbo.EmployeeAdministrationView
AS
SELECT DISTINCT em.Id, Login, Name, EMail,
           dbo.agregatePermissionsFunction (em.Id) AS Uprawnienia
FROM Employees em
		inner join [Permissions] perm on em.Id = perm.EmployeeId
		inner join [Credentials] creds on em.CredentialsId = creds.Id