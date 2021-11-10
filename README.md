# HackSystem

<p align="center">
   <img src="https://raw.github.com/CuteLeon/HackSystem/master/src/HackSystem.Web/wwwroot/LogoImage.png" align="center"/>
   <h2 align="center">Hack System</h2>
   <p align="center">A Hack System based on ASP.NET Core and Blazor WebAssembly.</p>
</p>

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

   2. ```shell
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



# Database Migration

## Setup

1. Select `HackSystem.WebAPI` as Startup project;
2. Open Package Manager Console in Visual Studio;
3. Select `HackSystem.WebAPI.Infrastructure` as Default project in Package Manager Console;
4. Input commands and execute;

## Commands

| Command                  | Description                                                  |
| ------------------------ | ------------------------------------------------------------ |
| Get-Help entityframework | Displays information about entity framework commands.        |
| Add-Migration            | Creates a migration by adding a migration snapshot.          |
| Remove-Migration         | Removes the last migration snapshot.                         |
| Update-Database          | Updates the database schema based on the last migration snapshot. |
| Script-Migration         | Generates a SQL script using all the migration snapshots.    |
| Scaffold-DbContext       | Generates a DbContext and entity type classes for a specified database. This is called reverse engineering. |
| Get-DbContext            | Gets information about a DbContext type.                     |
| Drop-Database            | Drops the database.                                          |
