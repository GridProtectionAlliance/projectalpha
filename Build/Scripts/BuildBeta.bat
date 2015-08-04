@ECHO OFF
C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ProjectAlpha.buildproj /p:ForceBuild=true /l:FileLogger,Microsoft.Build.Engine;logfile=ProjectAlpha.output
PAUSE