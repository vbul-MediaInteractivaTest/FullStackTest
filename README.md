# Prerequisites\Tools\TechStack:
- Visual Studio 2019 (16.7.7)
- Microsoft SQL Server 2019 (RTM-GDR) (KB4517790) - 15.0.2070.41 (X64)
- SQL Server Management Studio 15.0.18358.0
- .Net Core 3.1 (Windows OS).
- Google Chrome (86.0.4240.111)
- Angular CLI (10.2.0)
- NodeJs (14.15.0)

PS. I`m sure that for most tools can be used older versions

# How to start:

## MediaInteractivaTest - is an API project with most of business logic

Currently aplication designed to use two profiles (Development and Tests). So first of all we should open "appsettings.json" and specify connection string for both profiles (note that "Database" property should not use existing one db).

To prepare database you should open "Package Manager Console" (Tools -> NuGet Package Manager -> Package Manager Console) in VS2019 and execute such commands (both commands for every profile but with corresponding property "ASPNETCORE_ENVIRONMENT"):

```
$env:ASPNETCORE_ENVIRONMENT='Tests'

Update-Database
```

Before start specify "ASPNETCORE_ENVIRONMENT" in project properties ("Debug" tab) according to expected usage ("Development" for regular cases and "Tests" as backend for tests)
By default are use port 5000

## ui - Web application based on angular framework

At first we should open folder with project in command line (~\MediaInteractivaTest\ui)

Then install missing modules:

```
npm install
```

After preparation UI is ready to start:

```
ng serve
```

You can get access to content by [this link](http://localhost:4200/)

## MediaInteractivaTest.ApiTests - is project with some integration tests (based on NUnit)
Nothing special for this project but "appsettings.json" with connection string for test db

PS. REST API should be run before tests will start

# Notes\Possible improvements:
- Maybe make sense to move "Pets" grid to "Employees" as nested
- Modal windows should be reworked to validate and highlight incorrect fields (instead of alert)
- Currently we have no any pagination\filters\sorting in grid (and dropdowns) so application will be unusable when records counts are increased
