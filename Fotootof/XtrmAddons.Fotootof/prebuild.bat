@echo off
setlocal EnableDelayedExpansion

:: Prefix for logs
set logger=BATCH PREBUILD #

echo %logger% # #######################################

:: Process delete on [\Bin] directory
if exist %DestDirectoryBin% (
    rmdir /S /Q "%DestDirectoryBin%"

	echo %logger% Process delete destination [\Bin] directory : Done.
	echo %logger% Process delete destination [\Bin] directory : Done. >> %loggerFile%
) else (
	echo %logger% Process delete destination [\Bin] directory : Directory not found !
	echo %logger% Process delete destination [\Bin] directory : Directory not found ! >> %loggerFile%
)

:: Process delete on [\Local] directory
if exist %DestDirectoryLocal% (
    rmdir /S /Q "%DestDirectoryLocal%"

	echo %logger% Process delete destination [\Local] directory : Done.
	echo %logger% Process delete destination [\Local] directory : Done. >> %loggerFile%
) else (
	echo %logger% Process delete destination [\Local] directory : Directory not found !
	echo %logger% Process delete destination [\Local] directory : Directory not found ! >> %loggerFile%
)

:: Process delete on [\Packages] directory
if exist %DestDirectoryPackages% (
    rmdir /S /Q "%DestDirectoryPackages%"

	echo %logger% Process delete destination [\Packages] directory : Done.
	echo %logger% Process delete destination [\Packages] directory : Done. >> %loggerFile%
) else (
	echo %logger% Process delete destination [\Packages] directory : Directory not found !
	echo %logger% Process delete destination [\Packages] directory : Directory not found ! >> %loggerFile%
)

echo %logger% # ####################################### END

endlocal