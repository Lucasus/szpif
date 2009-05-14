EXEC sp_configure 'clr enabled', 1
RECONFIGURE WITH OVERRIDE

Use szpifDatabase

CREATE ASSEMBLY regX
FROM 'D:\Moje dokumenty\Baza informacji\studia - 6 - bazy danych II\Szpif\Database\src\TextFunctions\TextFunctions\bin\Release\TextFunctions.dll'
GO



CREATE Function RegExMatch(@Input NVARCHAR(512),@Pattern NVARCHAR(127))
RETURNS BIT EXTERNAL NAME regX.RegularExpressions.RegExMatch
GO 