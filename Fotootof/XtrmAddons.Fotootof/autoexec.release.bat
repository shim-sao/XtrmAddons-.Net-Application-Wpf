@echo off

:: IMPORTANT : Replace here the path of the root directory of the project.
set SourceDirName=G:\projects-visualstudio-git\XtrmAddons-.Net-Application-Wpf\Fotootof\XtrmAddons.Fotootof
:: set SourceDirName=%cd%

echo %cd%

set AssetsSourceDirName=%SourceDirName%\..\XtrmAddons.Fotootof.Lib.Assets

set DestDirName=%SourceDirName%\bin\Release

call %SourceDirName%\prebuild.bat
call %SourceDirName%\postbuild.bat

:: pause