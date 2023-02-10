@ECHO off

ECHO Deleting the "DNS Flusher" Service
ECHO.

sc delete "DNS Flusher"

ECHO.
ECHO NOTE: If access was denied, you need to run the script as Admin
ECHO.

PAUSE