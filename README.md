# Bringing New .NET Core Features to Old .NET Framework Libraries

## Demos

### Demo 1 - Working with the CLI

1. Note that none of this is .NET Framework specific, yet

    dotnet new sln --name CSC

    dotnet new classlib --name CSCLibStandard -f netstandard2.0

    dotnet sln CSC.sln add CSCLibStandard/

    dotnet new xunit --name CSCLibStandard.Tests

    dotnet sln CSC.sln add CSCLibStandard.Tests/

    dotnet add CSCLibStandard.Tests/ reference CSCLibStandard/

    dotnet build CSC.sln

    dotnet test CSC.sln

    dotnet pack CSCLibStandard/ --output ../ --configuration Release

### Demo 2 - Working with `<PackageReference />`

1. Review `packages.config` entries

   - Only `Serilog.Sinks.Debug`, `Newtonsoft.Json`, and `Dapper` were "installed"
   - Note transitive dependencies in manifest

1. Review `References` node in project

   - Note how all deps are the same icon / type

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

1. Review `References` node in project

   - Note how NuGet references are a different color

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

   Or if you have a private package feed:

   `dotnet tool install --global Project2015To2017.Migrate2019.Tool --ignore-failed-sources`

1. Run the tool

   `dotnet migrate-2019 wizard ./CSCLibFramework`

1. Open `.csproj` without unloading!

1. Add the projec to the sln

   `dotnet sln CSC.sln add CSCLibFramework/`

1. Add a new test project

   `dotnet new xunit --name CSCLibFramework.Tests`

1. Add the test project to the sln

   `dotnet sln CSC.sln add CSCLibFramework.Tests/`

1. Update the TFM of the test project to `net472`

1. Add the Framework library project to the Framework test project

   `dotnet add CSCLibFramework.Tests/ reference CSCLibFramework/`

1. View transitive dep graph in Visual Studio for `CSCLibFramework.Tests`

1. Run tests for solution

   `dotnet test CSC.sln`

1. Set `<VersionPrefix>` in CSCLibFramework to 1.2.3

1. Run pack command with --version-suffix option

   `dotnet pack CSCLibFramework/ --output ../ --configuration Release --no-build --version-suffix ci-20190806.3`

1. Review the generated package

    `start CSCLibFramework.1.2.3-ci-20190806.3.nupkg`

1. Create final 'approved' release without re-build by re-packing without suffix

    `dotnet pack CSCLibFramework/ --output ../ --configuration Release --no-build`

1. CI workflow FTW!
