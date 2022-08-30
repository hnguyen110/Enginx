rm -rf database.db
rm -rf Migrations
dotnet ef migrations add database
dotnet ef database update
