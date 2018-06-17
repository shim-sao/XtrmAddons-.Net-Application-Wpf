@echo off

:: Prefix for logs
set logger=BATCH BUILD #

echo %logger% # #######################################

:: ------------------------------------------------------
:: Set all destinations path to directories here 
:: ------------------------------------------------------

:: Set path to destination directory of the application.
set DestDirectory=%cd%
echo %logger% Destination Directory : %DestDirectory%
echo %logger% Destination Directory : %DestDirectory% >> %loggerFile%

:: Set path to destination directory [\Bin]
set DestDirectoryBin=%DestDirectory%\Bin
echo %logger% Destination directory [{DestDirectory}\Bin] : %DestDirectoryBin%
echo %logger% Destination directory [{DestDirectory}\Bin] : %DestDirectoryBin% >> %loggerFile%

:: Set path to destination directory [\Local]
set DestDirectoryLocal=%DestDirectory%\Local
echo %logger% Destination directory [{DestDirectory}\Local] : %DestDirectoryLocal%
echo %logger% Destination directory [{DestDirectory}\Local] : %DestDirectoryLocal% >> %loggerFile%

:: Set path to destination directory [\Packages]
set DestDirectoryPackages=%DestDirectory%\Packages
echo %logger% Destination directory [{DestDirectory}\Packages] : %DestDirectoryPackages%
echo %logger% Destination directory [{DestDirectory}\Packages] : %DestDirectoryPackages% >> %loggerFile%

:: Set path to destination directory [\Packages\Int]
set DestDirectoryPackagesInt=%DestDirectoryPackages%\Int
echo %logger% Destination directory [{DestDirectoryPackages}\Int] : %DestDirectoryPackagesInt%
echo %logger% Destination directory [{DestDirectoryPackages}\Int] : %DestDirectoryPackagesInt% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext]
set DestDirectoryPackagesExt=%DestDirectoryPackages%\Ext
echo %logger% Destination directory [{DestDirectoryPackages}\Ext] : %DestDirectoryPackagesExt%
echo %logger% Destination directory [{DestDirectoryPackages}\Ext] : %DestDirectoryPackagesExt% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext\Microsoft]
set DestDirectoryPackagesExtMicrosoft=%DestDirectoryPackagesExt%\Microsoft
echo %logger% Destination directory [{DestDirectoryPackagesExt}\Microsoft] : %DestDirectoryPackagesExtMicrosoft%
echo %logger% Destination directory [{DestDirectoryPackagesExt}\Microsoft] : %DestDirectoryPackagesExtMicrosoft% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext\SQLite]
set DestDirectoryPackagesExtSQLite=%DestDirectoryPackagesExt%\SQLite
echo %logger% Destination directory [{DestDirectoryPackagesExt}\SQLite] : %DestDirectoryPackagesExtSQLite%
echo %logger% Destination directory [{DestDirectoryPackagesExt}\SQLite] : %DestDirectoryPackagesExtSQLite% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext\SQLitePCLRaw]
set DestDirectoryPackagesExtSQLitePCLRaw=%DestDirectoryPackagesExt%\SQLitePCLRaw
echo %logger% Destination directory [{DestDirectoryPackagesExt}\SQLitePCLRaw] : %DestDirectoryPackagesExtSQLitePCLRaw%
echo %logger% Destination directory [{DestDirectoryPackagesExt}\SQLitePCLRaw] : %DestDirectoryPackagesExtSQLitePCLRaw% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext\System]
set DestDirectoryPackagesExtSystem=%DestDirectoryPackagesExt%\System
echo %logger% Destination directory [{DestDirectoryPackagesExt}\System] : %DestDirectoryPackagesExtSystem%
echo %logger% Destination directory [{DestDirectoryPackagesExt}\System] : %DestDirectoryPackagesExtSystem% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext\Xceed]
set DestDirectoryPackagesExtXceed=%DestDirectoryPackagesExt%\Xceed
echo %logger% Destination directory [{DestDirectoryPackagesExt}\Xceed] : %DestDirectoryPackagesExtXceed%
echo %logger% Destination directory [{DestDirectoryPackagesExt}\Xceed] : %DestDirectoryPackagesExtXceed% >> %loggerFile%

:: Set path to destination directory [\Packages\Ext\Others]
set DestDirectoryPackagesExtOthers=%DestDirectoryPackagesExt%\Others
echo %logger% Destination directory [{DestDirectoryPackagesExt}\Others] : %DestDirectoryPackagesExtOthers%
echo %logger% Destination directory [{DestDirectoryPackagesExt}\Others] : %DestDirectoryPackagesExtOthers% >> %loggerFile%

:: Set path to destination directory [\Plugins]
set DestDirectoryPlugins=%DestDirectory%\Plugins
echo %logger% Destination directory [{DestDirectory}\Plugins] : %DestDirectoryPlugins%
echo %logger% Destination directory [{DestDirectory}\Plugins] : %DestDirectoryPlugins% >> %loggerFile%

echo %logger% # #######################################