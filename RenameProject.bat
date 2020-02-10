@ECHO OFF
SET destU=%1
IF /I "%destU%"=="" SET /P destU=Enter PascalCase target project title, e.g., "MyAnalytic" (without quotes):
SET srcU=%2
IF /I "%srcU%"=="" SET srcU=ProjectAlpha
SET srcL=%srcU%
SET destL=%destU%
SET ucase=ABCDEFGHIJKLMNOPQRSTUVWXYZ
SET lcase=abcdefghijklmnopqrstuvwxyz
SETLOCAL EnableDelayedExpansion

FOR /L %%a IN (0,1,25) DO (
   CALL SET "from=%%ucase:~%%a,1%%
   CALL SET "to=%%lcase:~%%a,1%%
   CALL SET "srcL=%%srcL:!from!=!to!%%
   CALL SET "destL=%%destL:!from!=!to!%%
)

ECHO.
ECHO *** ProjectAlpha Rename Script ***
ECHO.
ECHO About to rename "%srcU%/%srcL%" to "%destU%/%destL%", press Ctrl+C to cancel, or
PAUSE
.\Source\Dependencies\GSF\ReplaceInFiles /r /v /c ".\Build\*.*" %srcU% %destU%
.\Source\Dependencies\GSF\ReplaceInFiles /r /v /c ".\Source\*.*" %srcU% %destU%
.\Source\Dependencies\GSF\BRC64 /REPLACECS:%srcU%:%destU% /RECURSIVE /EXECUTE
.\Source\Dependencies\GSF\ReplaceInFiles /r /v /c ".\Build\*.*" %srcL% %destL%
.\Source\Dependencies\GSF\ReplaceInFiles /r /v /c ".\Source\*.*" %srcL% %destL%
.\Source\Dependencies\GSF\BRC64 /REPLACECS:%srcL%:%destL% /RECURSIVE /EXECUTE

ENDLOCAL
ECHO.
ECHO Project Rename to "%destU%" is now Complete.
ECHO.
PAUSE