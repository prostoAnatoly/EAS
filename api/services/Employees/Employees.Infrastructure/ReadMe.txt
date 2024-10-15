Add-Migration InitialCreate -StartupProject Employees.Grpc -Project Employees.Infrastructure -OutputDir Migrations\Postgresql

Add-Migration ADD_MIG -StartupProject Employees.Grpc -Project Employees.Infrastructure -OutputDir Migrations\Postgresql -Context EmployeesDatabaseContext

Remove-Migration -StartupProject Employees.Grpc -Project Employees.Infrastructure

Update-Database -StartupProject Employees.Grpc -Project Employees.Infrastructure

----------
READ_MODELS
Add-Migration ADD_MIG -StartupProject Employees.Grpc -Project Employees.Infrastructure -OutputDir Migrations\Postgresql\ReadModels -Context ReadModelsDatabaseContext
