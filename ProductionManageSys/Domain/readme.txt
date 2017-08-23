##数据库迁移命令：
Enable-Migrations -ContextTypeName Domain.ApplicationDbContext
Add-Migration InitialCreate
Update-Database -Verbose
update-database -script
Update-Database -Script -SourceMigration