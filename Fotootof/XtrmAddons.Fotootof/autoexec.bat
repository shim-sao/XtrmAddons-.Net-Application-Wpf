@echo off

set SourceDirName=G:\projects-visualstudio-git\XtrmAddons-.Net-Application-Wpf\Fotootof\XtrmAddons.Fotootof
:: set SourceDirName=%cd%
set AssetsSourceDirName=%SourceDirName%\..\XtrmAddons.Fotootof.Lib.Assets

set DestDirName=%SourceDirName%\bin\Debug
call %SourceDirName%\prebuild.bat
call %SourceDirName%\postbuild.bat


set DestDirName=%SourceDirName%\bin\Release
::call %SourceDirName%\prebuild.bat
::call %SourceDirName%\postbuild.bat

:: pause