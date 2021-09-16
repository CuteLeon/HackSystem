# HackSystem

![GitHub branch checks state](https://img.shields.io/github/checks-status/CuteLeon/HackSystem/master?label=Hack%20System)
![GitHub](https://img.shields.io/github/license/CuteLeon/HackSystem)
![GitHub top language](https://img.shields.io/github/languages/top/CuteLeon/HackSystem)
![GitHub repo file count](https://img.shields.io/github/directory-file-count/CuteLeon/HackSystem)
![GitHub repo size](https://img.shields.io/github/repo-size/CuteLeon/HackSystem)

![GitHub issues](https://img.shields.io/github/issues/CuteLeon/HackSystem)
![GitHub forks](https://img.shields.io/github/forks/CuteLeon/HackSystem)
![GitHub Repo stars](https://img.shields.io/github/stars/CuteLeon/HackSystem)
![GitHub watchers](https://img.shields.io/github/watchers/CuteLeon/HackSystem)
![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/CuteLeon/HackSystem?include_prereleases)
![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/CuteLeon/HackSystem)

![GitHub all releases](https://img.shields.io/github/downloads/CuteLeon/HackSystem/total)
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/CuteLeon/HackSystem)
![GitHub commits since latest release (by date including pre-releases)](https://img.shields.io/github/commits-since/CuteLeon/HackSystem/latest/master?include_prereleases)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/CuteLeon/HackSystem/master)


![](https://raw.github.com/CuteLeon/HackSystem/master/HackSystem.Web/wwwroot/LogoImage.png)

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
