Add-Migration InitialCreate -StartupProject Identity.Grpc -Project Identity.Infrastruct -OutputDir Migrations\Postgresql

Add-Migration ADD_MIG -StartupProject Identity.Grpc -Project Identity.Infrastruct -OutputDir Migrations\Postgresql

Remove-Migration -StartupProject Identity.Grpc -Project Identity.Infrastruct

Update-Database -StartupProject Identity.Grpc -Project Identity.Infrastruct