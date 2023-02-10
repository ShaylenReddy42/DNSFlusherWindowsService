# DNS Flusher Windows Service

[![Build Status](https://dev.azure.com/Shaylen/Personal/_apis/build/status/DNSFlusherWindowsService?branchName=master)](https://dev.azure.com/Shaylen/Personal/_build/latest?definitionId=16&branchName=master)

## What's the purpose of this project?

This aims to be a better solution to a perceived problem of having my dns cache poisoned

In the commit [kubernetes: move all resources to the default namespace](https://github.com/ShaylenReddy42/Seelans-Tyres/commit/f0aceb8d6d7c46f198f4e904ed6c29d32f6e7785) of my Seelan's Tyres project, I said this:

  ".. after watching john savill's video "how dns works", i suspected my dns was poisoned so i started flushing it often, 
  then it got to a point where it seemed like it was being poisoned on a regular basis as my builds would hang, 
  and when i flushed my dns, it would proceed

  to solve this problem, i created a script to flush the dns, 
  and created a task in task scheduler to execute that script every minute indefinitely"

Though that solution works, it causes other problems like:
1. Flashing the terminal and losing focus
2. Causes the terminal [sometimes] to freeze and it builds up on each other

This obviously ruins my experience so I had to come up with another solution and this is it

It's a worker service that can be registered as a Windows Service that runs in the background executing `ipconfig /flushdns` every 30 seconds

This doesn't flash the terminal, and logs are sent to the Windows Event Logs

A much MUCH better solution

## Resources that helped me write this project

### Pluralsight courses

* [Building ASP.NET Core 3 Hosted Services and .NET Core 3 Worker Services](https://www.pluralsight.com/courses/building-aspnet-core-hosted-services-net-core-worker-services)
  * Author: Steve Gordan

### YouTube videos

* [How DNS Works](https://www.youtube.com/watch?v=Ah7fYex6Ups)
  * Author: John Savill's Technical Training
* [Execute an Exe in Azure](https://www.youtube.com/watch?v=I0iheDwm5Ac)
  * Author: Microsoft Developer
* [run command prompt commands from within a C# application](https://www.youtube.com/watch?v=HeHR9q-IWF8)
  * Author: Coders Media

### Microsoft Learn docs
* [Logging providers in .NET (Windows EventLog)](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging-providers#windows-eventlog)

## Required local setup to build and run

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) 17.4 or later
* .NET SDK [7.0.102](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [CMake](https://cmake.org/download/) 3.21.4 or later

## Build instructions

Run `scripts/build.cmd` and the resulting executable will be in the publish folder at the root of the repository

All relevant scripts will be in the publish folder too, to register the service, and to delete it

Upon registering it, you have to go into Services [Press <kbd>🪟</kbd> and Search "Services"], and start it manually