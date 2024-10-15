Add-Migration InitialCreate -StartupProject Organizations.Grpc -Project Organizations.Infrastructure -OutputDir Migrations\Postgresql

Add-Migration ADD_MIG -StartupProject Organizations.Grpc -Project Organizations.Infrastructure -OutputDir Migrations\Postgresql

Remove-Migration -StartupProject Organizations.Grpc -Project Organizations.Infrastructure

Update-Database -StartupProject Organizations.Grpc -Project Organizations.Infrastructure