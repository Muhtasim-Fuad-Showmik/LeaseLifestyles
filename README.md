# LeaseLifestyles

## Specifications:

| Tech | Version |
| ---- | ------- |
| .NET | 8.0.401 |
| Node | 20.8.0  |

## Initialization Guide

For each microservice created in .NET, follow the following steps

### Restore NuGet Packages

Restore the NuGet packages required by the project

```shell
dotnet restore
```

### Apply Migrations

As the project uses Entity Framework Core, apply the migrations to the database

```shell
dotnet ef database update
```

### Build the Project

Build the project to ensure everything is set up correctly

```shell
dotnet build
```

### Run the Project

Finally, run the project

```shell
dotnet run
```
