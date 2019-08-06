# Bringing New .NET Core Features to Old .NET Framework Libraries

## Demos

### Demo 1 - Working with the CLI

    dotnet new sln --name CSC

    dotnet new classlib --name CSCLibStandard -f netstandard2.0

    dotnet sln CSC.sln add CSCLibStandard/

    dotnet new xunit --name CSCLibStandard.Tests

    dotnet sln CSC.sln add CSCLibStandard.Tests/

    dotnet add CSCLibStandard.Tests/ reference CSCLibStandard/

    dotnet build CSC.sln

    dotnet test CSC.sln

    dotnet pack CSCLibStandard/ --output ./ --configuration Release

### Demo 2 - Working with `<PackageReference />`

1. Review `packages.config` entries
    - Only `Serilog.Sinks.Debug`, `Newtonsoft.Json`, and `Dapper` were "installed"
    - Note transitive dependencies in manifest

1. Right click `CSCLibFramework`
   -> Select `Unload Project`

1. Right click `CSCLibFramework`
   -> Edit `CSCLibFramework.csproj`
   - Identify `<HintPath>` for Dapper

1. VS
   -> Options
   -> NuGet Package Manager
   -> Default package management format
   -> `PackageReference`

1. Right click `CSCLibFramework` \ `packages.config`
   -> Select `Migrate packages.config to PackageReference`

1. Right click `CSCLibFramework`
   -> Select `Unload Project`

1. Right click `CSCLibFramework`
   -> Edit `CSCLibFramework.csproj`
   - Review new `<PackageReference />` nodes
   - Identify removed transitive dependencies

### Demo 3 - Working with the Common Project System (CPS)

<https://github.com/hvanbakel/CsprojToVs2017>

1. Install the global tool

   `dotnet tool install --global Project2015To2017.Migrate2019.Tool`

1. Run the tool

   `dotnet migrate-2019 wizard ./CSCLibFramework`

1. Open `.csproj` without unloading!

1. Add the projec to the sln

   `dotnet sln add CSCLibFramework/`

1. Add a new test project

   `dotnet new xunit --name CSCLibFramework.Tests`

1. Add the test project to the sln

   `dotnet sln add CSCLibFramework.Tests/`

1. Update the TFM of the test project to `net472`

1. Add the Framework library project to the Framework test project

   `dotnet add CSCLibFramework.Tests/ reference CSCLibFramework/`

1. View transitive dep graph in Visual Studio for `CSCLibFramework.Tests`
