@ECHO off

CD "%~dp0"

CD ..

IF EXIST publish (
	RD publish /S /Q
	MD publish
)

ECHO Running CMake
ECHO.

cmake -S . -B build

RD build /S /Q

CD src\DNSFlusherWindowsService

ECHO.
ECHO Publishing Project
ECHO.

dotnet publish -c Release -r win-x64 -o "..\..\publish"

CD ..\..

ECHO.
ECHO Copying files to a root publish directory
ECHO.

COPY /V /Y scripts\delete-service.cmd publish\delete-service.cmd
ECHO.

ECHO All relevent files are available in the publish directory at the root of the repository
ECHO.

PAUSE
