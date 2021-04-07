# EF Core management
- Migration operations
* List migrations
`dotnet-ef migrations list -s ./Support.API --project ./Support.API.Services --context ApplicationDbContext`

* Add a new migration
`dotnet-ef migrations add init -s ./Support.API --project ./Support.API.Services  --context ApplicationDbContext`

* Remove a migration
`dotnet-ef migrations remove -s ./Support.API --project ./Support.API.Services --context ApplicationDbContext`

- Database Operations
* Update
`dotnet-ef database update -s ./Support.API --project ./Support.API.Services --context ApplicationDbContext`