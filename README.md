# Plane Spotting System

This is a sample Web application developed to assist plane spotters to logging their sightings.


## Setup and Run

The project is developed using .NET 7.0. Therefore, a compatible development environment should be ready with an IDE like Visual Studio.

Set SQLServer database connection string to the DefaultConnection in the appsettings.json to connect a database.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<sql-server-connection-string>"
  },
  "UseSQLiteDatabase": false
}
```
**Note**: To use SQLite database, the following steps should be completed.
* Delete all existing migration files under Migrations folder of Data project.
* Set ```"UseSQLiteDatabase": true``` in the appsettings.json file.
* Run the following [dotnet ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) command to create the initial migration files. 
```bash
dotnet ef migrations add InitialCreate --startup-project Rusada.Web  --project Data
```

## Tech/Framework used

* [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet)
* [Knockout.js](https://knockoutjs.com/index.html)
* [Bootsrap](https://getbootstrap.com/)

## License

[MIT](https://choosealicense.com/licenses/mit/)
