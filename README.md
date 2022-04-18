---

---

# HackSystem

<p align="center">
   <img src="https://raw.github.com/CuteLeon/HackSystem/master/src/HackSystem.Web/wwwroot/LogoImage.png" align="center"/>
   <h2 align="center">Hack System</h2>
   <p align="center">A Hack System based on ASP.NET Core and Blazor WebAssembly.</p>
   <p align="center">Design and implement your program and Execute it in Hack System!</p>
</p>

## Status

<p align="center">
   <a href="https://github.com/CuteLeon/HackSystem/actions/workflows/dotnet-core.yml">
      <img border="0" src="https://github.com/CuteLeon/HackSystem/workflows/.Net%20Build/badge.svg" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/blob/master/LICENSE">
      <img border="0" src="https://img.shields.io/github/license/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/search?l=c%23">
      <img border="0" src="https://img.shields.io/github/languages/top/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem">
      <img border="0" src="https://img.shields.io/github/directory-file-count/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/archive/refs/heads/master.zip">
      <img border="0" src="https://img.shields.io/github/repo-size/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/issues?q=is%3Aopen+is%3Aissue">
      <img border="0" src="https://img.shields.io/github/issues/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/network/members">
      <img border="0" src="https://img.shields.io/github/forks/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/stargazers">
      <img border="0" src="https://img.shields.io/github/stars/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/watchers">
      <img border="0" src="https://img.shields.io/github/watchers/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/releases">
      <img border="0" src="https://img.shields.io/github/v/release/CuteLeon/HackSystem?include_prereleases" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/releases">
      <img border="0" src="https://img.shields.io/github/release-date-pre/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/archive/refs/heads/master.zip">
      <img border="0" src="https://img.shields.io/github/downloads/CuteLeon/HackSystem/total" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/tags">
      <img border="0" src="https://img.shields.io/github/v/tag/CuteLeon/HackSystem" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/releases">
      <img border="0" src="https://img.shields.io/github/commits-since/CuteLeon/HackSystem/latest/master?include_prereleases" />
   </a>
   <a href="https://github.com/CuteLeon/HackSystem/commits/master">
      <img border="0" src="https://img.shields.io/github/last-commit/CuteLeon/HackSystem/master" />
   </a>
</p>

## Nuget Packages

<p align="center">
   <a href="https://www.nuget.org/packages/HackSystem.Intermediary/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Intermediary?label=HackSystem.Intermediary&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Intermediary.Abstractions/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Intermediary.Abstractions?label=HackSystem.Intermediary.Abstractions&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Web.Authentication.Abstractions/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Web.Authentication.Abstractions?label=HackSystem.Web.Authentication.Abstractions&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Web.Component.Abstractions/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Web.Component.Abstractions?label=HackSystem.Web.Component.Abstractions&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Web.CookieStorage/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Web.CookieStorage?label=HackSystem.Web.CookieStorage&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Web.ProgramPlatform/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Web.ProgramPlatform?label=HackSystem.Web.ProgramPlatform&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Web.ProgramPlatform.Abstractions/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Web.ProgramPlatform.Abstractions?label=HackSystem.Web.ProgramPlatform.Abstractions&style=flat-square" />
   </a>
   <a href="https://www.nuget.org/packages/HackSystem.Web.ProgramSchedule.Abstractions/">
      <img border="0" src="https://img.shields.io/nuget/vpre/HackSystem.Web.ProgramSchedule.Abstractions?label=HackSystem.Web.ProgramSchedule.Abstractions&style=flat-square" />
   </a>
</p>

## How to launch?

1. Install .Net 6.0 SDK.
2. Download source code and open in Visual Studio 2019 (16.8+).
3. Set `HackSystem.WebHost` and `HackSystem.WebAPI` as startup projects.
4. Press F5 to run.
5. Navigate to https://localhost:2473
6. Enjoy it.

## How to deploy?

1. Edit `hosting.json` file of `HackSystem.WebHost` and `HackSystem.WebAPI` projects to config the port to listen.
2. Edit `wwwroot/appsettings.json` file of `HackSystem.Web` so that `APIConfiguration.APIURL` **equals** `urls` of `HackSystem.WebHost` to config the address of Web API.
3. Edit `JwtConfiguration` section of  `HackSystem.WebAPI` project's `appsettings.json`, **it is Important for security!**
4. Publish `HackSystem.WebHost` and `HackSystem.WebAPI` projects.
   1. `HackSystem.WebHost` is just a fake host of core project `HackSystem.Web`.
5. Navigate to Hack System:
   1. Open browser and navigate to the address which you just configured for `HackSystem.WebHost`.
   2. Or you can use the `HackSystem.Host` project to visit Hack System.
      1. Before you launch `HackSystem.Host`, edit `HostConfigs.json` file so that `RemoteURL` equals the address which you just configured for `HackSystem.WebHost`.
      2. Just run `HackSystem.Host.exe`.
6. Enjoy it.

## How to develop customized programs

> Something may change with platform developing.

1. Create a new Razor Class Library project.

2. Install above nuget pakcages to this project.

3. Add a new image file named as Index.png in root folder of this project, and copy to output directory if newer.

4. Create a new Razor Component as entry component, and inherits ProgramComponentBase class.
   
   1. Design and Implement it.

5. Create a new static Launcher class and return the type of entry component from static Launch method.
   
   1. ```csharp
      public static class Launcher
      {
          // Launch Parameter is not mandatory.
          public static Type Launch(LaunchParameter parameter)
          {
              return typeof(TaskSchedulerComponent);
          }
      }
      ```

## How to deploy customized programs

> Something may change with platform developing.

1. Insert a new record in database of new program.
   
   1. ```sql
      INSERT INTO ProgramDetails (Id,Name,Enabled,SingleInstance,EntryAssemblyName,EntryTypeName,EntryParameter,Mandatory)
      VALUES ('program0-icon-0828-hack-system000006','TaskServer',1,1,'HackSystem.Web.TaskSchedule','HackSystem.Web.TaskSchedule.Launcher','{ "Developer": "Leon" }',1);    
      ```

2. Edit `ProgramAssetConfiguration` section of  `HackSystem.WebAPI` project's `appsettings.json` to config program asset folder path.

3. Create new filder named as new program ID in program asset folder.

4. Build program project and copy all files into above folder.
   
   1. Post-build event to copy program files into WebAPI's out directory automatically.
      
      ```shell
      set assetFolder=$(SolutionDir)HackSystem.WebAPI\$(OutDir)ProgramAssets\
      MKDIR assetFolder
      set assetFolder=%assetFolder%program0-icon-0828-hack-system000006\
      MKDIR assetFolder
      XCOPY $(TargetDir)* %assetFolder% /Y /S /H
      ```

5. Insert a new record in database to map releationship between user and program.
   
   1. ```sql
      INSERT INTO UserProgramMaps (UserId,ProgramId,PinToDesktop,PinToDock,PinToTop,"Rename")
      VALUES ('msaspnet-core-user-hack-system000001','program0-icon-0828-hack-system000006',1,0,0,NULL);
      ```

6. Launch Hack System and login above user, should see the new program launch and enjoy it.

# Video

> Click image below to watch video.

## Main Desktop (2020-09-27)

<p align="center">
   <a href="https://www.bilibili.com/video/BV1di4y177TH/">
      <img border="0" src="https://raw.github.com/CuteLeon/HackSystem/master/readme/VideoSplash.jpg" />
   </a>
</p>

## Multiple Windows Scheduler (2021-10-24)

<p align="center">
   <a href="https://www.bilibili.com/video/BV16r4y117Fz/">
      <img border="0" src="https://raw.github.com/CuteLeon/HackSystem/master/readme/MultipleWindowsScheduler.jpg" />
   </a>
</p>

# Screen

## Start Up

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/StartUp.jpg)

## Register

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/Register.jpg)

## Login

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/Login.jpg)

## Desktop Demo

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/DesktopDemo_0.jpg)

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/DesktopDemo_2.jpg)

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/DesktopDemo_1.jpg)

## Task Scheduler

![](https://raw.github.com/CuteLeon/HackSystem/master/readme/TaskScheduler.jpg)

# Guide

## Architecture

### Client

    Contains all Front-end related projects, such as interfaces or implementations of UI Components, Authentication, Front-end Contracts, Cookie Storage, Domain Models, and a WebHost process.

    The core project is `HackSystem.Web`, configura Front-end requests process pipe and Dependency injection in `Program.cs`, configura Front-end related application settings in `wwwroot\appsettings.json`.

#### ProgramSchedule

    Program schedule component of Front-end, used to Lazy load program assemblies, Manage and Schedule Program UI WIndows, Launch and Destory Processes, Produce and Consume programs or processes related notifications.

### DevelopmentKit

    A development kit used to help developer to design and implement mini programs which can be executed in Hack System.    

### Programs

    Mini programs which implemented by above Development Kit. Includes all internal programs of Hack System here:

#### AppStore

#### Explorer

#### Home

#### MockServer

#### Profile

#### Settings

#### TaskSchedule

    This is the most useful program for Hack System currently, used to manage and manually trigger Back-end tasks in this program.

### Server

    Contains all Back-end related projects, such as interfaces or implementations of Back-end Services, Authentication, Back-end Contracts, Domain Models. It works as both of a Web MVC and a Web API programs.

    The core project is `HackSystem.WebAPI`, configura Back-end requests process pipe, Dependency injection and Security policies in `Program.cs`, configura Back-end related application settings in `appsettings.json`.

#### MockServer

    Mock server related interfaces, domain models, implementations.

#### ProgramServer

    Mock server related interfaces, domain models, implementations. Used to store and resolve program assets, such as JS, CSS, Assemblies and so on.

#### TaskServer

    Task server related interfaces, domain models, implementations. Used to manage and schedule Back-end Tasks.

    Implement and inject Hack System's Tasks in `HackSystem.WebAPI.Tasks` project.

### Shared

    Shared contracts or asset for both of Front-end and Back-end.

### UnitTests

    As you see, Unit Tests.

# Tutorials

## Log

    Declare log format and out level in `NLog.config`, log with different level will be wrote into sepcified log files at Back-end side.

## Good Dependency Injection Practice

    Split different component into sub domains, and create Application, Domain, Infrastructure projects for each sub domain, then register interfaces and implementations in root project of current sub domain. You can find this practice in almost every component of Hack System.

### Application

    Define interfaces of each service, and use this project as a Abstraction, and can be referred by other projects without any impletmentation included.

### Domain

    Similar with Application, but this project used to define Domain Models, it's also a Abstraction and can be referred by other projects with any implementation included.

### Infrastructure

    Actual implementation of each sub domain, it should not be referred by other projects directly.

### Root Project

    This is a link between application and inplementation, root project of each sub domain should contains a statis Extension class with  a static Extension method on IServiceCollection to inject dependency into DI container, like below:

```csharp
public static IServiceCollection AttachTaskServer(
    this IServiceCollection services,
    TaskServerOptions configuration)
{
    services
        .Configure(new Action<TaskServerOptions>(options => options.TaskServerHost = configuration.TaskServerHost))
        .AddSingleton<IHackSystemTaskServer, HackSystemTaskServer>()
        .AddScoped<ITaskRepository, TaskRepository>()
        .AddScoped<ITaskLogRepository, TaskLogRepository>()
        .AddScoped<ITaskLoader, TaskLoader>()
        .AddScoped<ITaskScheduleWrapper, TaskScheduleWrapper>()
        .AddTransient<ITaskGenericJob, TaskGenericJob>()
        .AttachTaskServerInfrastructure()
        .AddWebAPITasks();

    return services;
}
```

## Database Access

    Currently, we are using EF Core code-first mode in Hack System, which means that there is no need to write any line of SQL, use entity class and linq is enough for all database access scenarios.

### Create Entity Class

    Create entity class in `HackSystem.WebAPI.Domain\Entity` folder, and define required properties of this entity type.

```csharp
public class GenericOption
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OptionID { get; set; }

    public string OptionName { get; set; }

    public string OptionValue { get; set; }

    public string? Category { get; set; }

    public string? OwnerLevel { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }
}
```

### Link Entity Class into DataBase Context

    Add new `DbSet<TEntity>` collection property in `HackSystem.WebAPI.Infrastructure\DBContexts\HackSystemDbContext.cs` to map to a table in database.

```csharp
public virtual DbSet<GenericOption> GenericOptions { get; set; }
```

### Design indexes or relationships of entity classes

    Design indexes or relationships of each entity type in `OnModelCreating` method of `HackSystemDbContext.cs`.

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<UserProgramMap>().HasOne(map => map.Program).WithMany(program => program.UserProgramMaps).HasForeignKey(map => map.ProgramId).OnDelete(DeleteBehavior.Cascade);
    builder.Entity<UserProgramMap>().HasOne(map => map.ProgramUser).WithMany(user => user.UserProgramMaps).HasForeignKey(map => map.UserId).OnDelete(DeleteBehavior.Cascade);
    builder.Entity<UserProgramMap>().HasKey(map => new { map.UserId, map.ProgramId });

    builder.Entity<GenericOption>().HasIndex(nameof(GenericOption.OptionName), nameof(GenericOption.Category), nameof(GenericOption.OwnerLevel)).IsUnique();

    builder.Entity<GenericOption>().Property(nameof(GenericOption.OptionName)).UseCollation("NOCASE");
    builder.Entity<GenericOption>().Property(nameof(GenericOption.Category)).UseCollation("NOCASE");
    builder.Entity<GenericOption>().Property(nameof(GenericOption.OwnerLevel)).UseCollation("NOCASE");
}
```

### Generate Database Migrations code file

1. Select `HackSystem.WebAPI` as Startup project;
2. Open `Package Manager Console` in Visual Studio;
3. Select `HackSystem.WebAPI.Infrastructure` as Default project in Package Manager Console;
4. Input below commands and execute;

#### Commands

| Command                  | Description                                                                                                 |
| ------------------------ | ----------------------------------------------------------------------------------------------------------- |
| Get-Help entityframework | Displays information about entity framework commands.                                                       |
| Add-Migration            | Creates a migration by adding a migration snapshot.                                                         |
| Remove-Migration         | Removes the last migration snapshot.                                                                        |
| Update-Database          | Updates the database schema based on the last migration snapshot.                                           |
| Script-Migration         | Generates a SQL script using all the migration snapshots.                                                   |
| Scaffold-DbContext       | Generates a DbContext and entity type classes for a specified database. This is called reverse engineering. |
| Get-DbContext            | Gets information about a DbContext type.                                                                    |
| Drop-Database            | Drops the database.                                                                                         |

    For examples:

```
-- To generate database migration code files automatically
Add-Migration AddGenericOptionEntity
-- To execute database migration code files 
-- and apply modifications to current connected database file
Update-Database
```

### Apply Pending Database Migrations automatically

    In above step, it's a development-time manual operation to apply modifation to database file.

    Here is a way to apply these modifaction all automatically during production-time, this way is "doing nothing manaully", as there is already a function at Back-end side to check pending database schema modifiton and apply them when launch Back-end program\: `HackSystem.WebAPI.Infrastructure\DataSeed\DatabaseInitializer.cs`.

```csharp
private async static Task InitializeDatabaseAsync(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<IHost>>();
    var dbContext = services.GetRequiredService<HackSystemDbContext>();

    try
    {
        logger.LogDebug($"Ensure database created...");
        await dbContext.Database.EnsureCreatedAsync();
        logger.LogDebug($"Check database pending migrations...");
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Pending database migration should be combined: \n\t{string.Join(",", pendingMigrations)}");
            await dbContext.Database.MigrateAsync();
        }
        logger.LogDebug($"Database check finished.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"Database check failed.");
    }
}
```

### Implement and Inject Repository Service of entity classes

    Define reponsitory interface in `HackSystem.WebAPI.Application\Repository` folder.

```csharp
public interface IGenericOptionRepository : IRepositoryBase<GenericOption>
{
    Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null);
}
```

    Implement repository service in `HackSystem.WebAPI.Infrastructure\Repository` folder.

```csharp
public class GenericOptionRepository : RepositoryBase<GenericOption>, IGenericOptionRepository
{
    public GenericOptionRepository(
        ILogger<GenericOptionRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public async Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null)
    => await this
        .AsQueryable()
        .Where(o => 
            (o.OptionName == optionName) &&
            (string.IsNullOrEmpty(o.OwnerLevel) || o.OwnerLevel == owner) &&
            (string.IsNullOrEmpty(o.Category) || o.Category == category))
        .OrderByDescending(o => o.OwnerLevel)
        .ThenByDescending(o => o.Category)
        .FirstOrDefaultAsync();
}


```

    Inject repository interface and implementation at `HackSystem.WebAPI\Extensions\HackSystemInfrastructureExtension.cs`

```csharp
public static IServiceCollection AddHackSystemWebAPIServices(
    this IServiceCollection services)
{
    services
        .AddScoped<IGenericOptionRepository, GenericOptionRepository>();
    return services;
}
```

## Intermediary

    We use `Intermediary` to communicate between components independently, it support multiple kinds of Communication Behaviors:

### Messages

- Request
  
  - `IIntermediaryRequest<out TResponse>`
  
  - Each request require to return a response from handler
  
  - Support only one kind of Request Handler for each Request type

- Command
  
  - `IIntermediaryCommand`
  
  - Each commend require to be processed be handler but without any response.
  
  - Support only one kind of Command Handler for each Command type

- Notification
  
  - `IIntermediaryNotification`
  
  - Similar with Command mode
  
  - Support multiple Notifiaction Handlers for each Notifaction type

- Event
  
  - `IIntermediaryEvent`
  
  - Similar with combination of Command and Notifaction
  
  - Multiple Event Handlers point to the same instance reference for each Event type, to process published event.

### Message Handlers

- `IIntermediaryRequestHandler<in TRequest, TResponse>`

- `IIntermediaryCommandHandler<TCommand>`

- `IIntermediaryNotificationHandler<in TNotification>`

- `IIntermediaryEventHandler<TEvent>`

### Message Publisher

- `IIntermediaryPublisher`
  
  - `<TResponse> SendRequest<TResponse>(IIntermediaryRequest<TResponse> request, CancellationToken cancellationToken = default)`
  
  - `SendCommand(IIntermediaryCommand command, CancellationToken cancellationToken = default)`
  
  - `PublishNotification(IIntermediaryNotification notification, CancellationToken cancellationToken = default)`
  
  - `PublishEvent(IIntermediaryEvent eventArg, CancellationToken cancellationToken = default)`
