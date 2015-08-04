@ECHO OFF
SET source=%2
IF /I "%source%"=="" SET source=ProjectAlpha
ECHO.
ECHO *** ProjectAlpha Rename Script ***
ECHO.
ECHO About to rename "%source%" to "%1", press Ctrl+C to cancel, or
PAUSE
.\Main\Source\Dependencies\GSF\ReplaceInFiles /r /v /c ".\Contributor Resources\*.*" %source% %1
.\Main\Source\Dependencies\GSF\ReplaceInFiles /r /v /c ".\Main\*.*" %source% %1
.\Main\Source\Dependencies\GSF\BRC64 /REPLACECI:%source%:%1 /RECURSIVE /EXECUTE
ECHO.
ECHO Project Rename Complete.
ECHO.
