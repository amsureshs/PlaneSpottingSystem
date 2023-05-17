# Plane Spotting System

This is a sample Web application developed to assist plane spotters to logging their sightings.


## Setup and Run

The project can be run in any tool that can be used to run .Net Core application.

Configure DefaultConnection and UseSQLiteDatabase in the appsettings.json to connect a database.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<sql-server-connection-string>"
  },
  "UseSQLiteDatabase": false
}
```

## Tech/Framework used

* [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet)
* [Knockout.js](https://knockoutjs.com/index.html)
* [Bootsrap](https://getbootstrap.com/)

## License

[MIT](https://choosealicense.com/licenses/mit/)
