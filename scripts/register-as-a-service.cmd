@ECHO off

CD "%~dp0"

ECHO Registering as a Windows Service
ECHO.

sc create "DNS Flusher" ^
    binPath= "%~dp0DNSFlusherWindowsService.exe" ^
    start= delayed-auto

ECHO.
ECHO NOTE: If access was denied, you need to run the script as Admin
ECHO.

PAUSE
