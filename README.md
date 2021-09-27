# HackSystem

[![Hack System](https://github.com/CuteLeon/HackSystem/workflows/.Net%20Build/badge.svg)](https://github.com/CuteLeon/HackSystem/actions/workflows/dotnet-core.yml)
[![GitHub](https://img.shields.io/github/license/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/blob/master/LICENSE)
[![GitHub top language](https://img.shields.io/github/languages/top/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/search?l=c%23)
[![GitHub repo file count](https://img.shields.io/github/directory-file-count/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem)
[![GitHub repo size](https://img.shields.io/github/repo-size/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/archive/refs/heads/master.zip)

[![GitHub issues](https://img.shields.io/github/issues/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/issues?q=is%3Aopen+is%3Aissue)
[![GitHub forks](https://img.shields.io/github/forks/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/network/members)
[![GitHub Repo stars](https://img.shields.io/github/stars/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/stargazers)
[![GitHub watchers](https://img.shields.io/github/watchers/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/watchers)
[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/CuteLeon/HackSystem?include_prereleases)](https://github.com/CuteLeon/HackSystem/releases)
[![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/releases)

[![GitHub all releases](https://img.shields.io/github/downloads/CuteLeon/HackSystem/total)](https://github.com/CuteLeon/HackSystem/archive/refs/heads/master.zip)
[![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/CuteLeon/HackSystem)](https://github.com/CuteLeon/HackSystem/tags)
[![GitHub commits since latest release (by date including pre-releases)](https://img.shields.io/github/commits-since/CuteLeon/HackSystem/latest/master?include_prereleases)](https://github.com/CuteLeon/HackSystem/releases)
[![GitHub last commit (branch)](https://img.shields.io/github/last-commit/CuteLeon/HackSystem/master)](https://github.com/CuteLeon/HackSystem/commits/master)

![](https://raw.github.com/CuteLeon/HackSystem/master/HackSystem.Web/wwwroot/LogoImage.png)

## Nuget Packages

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/HackSystem.Intermediary?label=HackSystem.Intermediary&style=flat-square)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/HackSystem.Intermediary.Abstractions?label=HackSystem.Intermediary.Abstractions&style=flat-square)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/HackSystem.Web.CookieStorage?label=HackSystem.Web.CookieStorage&style=flat-square)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/HackSystem.Web.CookieStorage.Abstractions?label=HackSystem.Web.CookieStorage.Abstractions&style=flat-square)

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


# Video

> Click image below to watch video.

[![CuteLeon/HackSystem](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/VideoSplash.jpg)](https://www.bilibili.com/video/BV1di4y177TH/ "CuteLeon/HackSystem")

# Screen

## Start Up

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/StartUp.jpg)



## Register

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/Register.jpg)



## Login

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/Login.jpg)



## Desktop Demo

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/DesktopDemo_0.jpg)

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/DesktopDemo_2.jpg)

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/DesktopDemo_1.jpg)



## Task Scheduler

![](https://raw.github.com/CuteLeon/HackSystem/master/ReadMe/TaskScheduler.jpg)



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
