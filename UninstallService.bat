@echo off
SET PROG=%"%C:\Utils\WebStarter\%"%
SET SERVICE_EXE=%"%\WebStarter.exe%"%
SET FIRSTPART=%WINDIR%"\Microsoft.NET\Framework\v"
SET SECONDPART="\InstallUtil.exe"
SET SERVICENAME=%"%WebStarter%"%
SET DELETEBATCH="\*.bat"

SET DOTNETVER=4.0.30319
IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install
SET DOTNETVER=2.0.50727
IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install
SET DOTNETVER=1.1.4322
IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install
SET DOTNETVER=1.0.3705
IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install
GOTO fail
:install
ECHO Found .NET Framework version %DOTNETVER%
ECHO Uninstalling service "%PROG%"
ECHO Uninstalling program "%SERVICE_EXE%"

%FIRSTPART%%DOTNETVER%%SECONDPART% /U /name=%SERVICENAME% "%PROG%%SERVICE_EXE%" 
GOTO end
:fail
echo FAILURE -- Could not find .NET Framework install
:end
ECHO DONE!!!
Pause