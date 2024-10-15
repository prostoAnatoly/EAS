Add-Migration InitialCreate -StartupProject  FilesStorage.Grpc -Project FilesStorage.Infrastruct -OutputDir Migrations\Postgresql

Add-Migration ADD_MIG -StartupProject  FilesStorage.Grpc -Project FilesStorage.Infrastruct -OutputDir Migrations\Postgresql

Remove-Migration -StartupProject  FilesStorage.Grpc -Project FilesStorage.Infrastruct

Update-Database -StartupProject  FilesStorage.Grpc -Project FilesStorage.Infrastruct