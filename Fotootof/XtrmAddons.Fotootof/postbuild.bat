@echo off
setlocal EnableDelayedExpansion

:: Prefix for logs
set logger=BATCH POSTBUILD #

echo %logger% # #######################################

:: -------------------------------------------------------------------------------
:: Processor for destination [\Packages] directory of the application 
:: This directory contains all binaries assemblies necessaries for the application.
:: -------------------------------------------------------------------------------

set dirArray[0]=%DestDirectoryPackages%
set dirArray[1]=%DestDirectoryPackagesInt%
set dirArray[2]=%DestDirectoryPackagesExt%
set dirArray[3]=%DestDirectoryPackagesExtMicrosoft%
set dirArray[4]=%DestDirectoryPackagesExtSQLite%
set dirArray[5]=%DestDirectoryPackagesExtSQLitePCLRaw%
set dirArray[6]=%DestDirectoryPackagesExtSystem%
set dirArray[7]=%DestDirectoryPackagesExtXceed%

for /l %%n in (0,1,7) do (
	if not exist !dirArray[%%n]! (
		mkdir !dirArray[%%n]!
		echo %logger% Process create [!dirArray[%%n]!] directory : Done.
		echo %logger% Process create [!dirArray[%%n]!] directory : Done. >> %loggerFile%
	) else (
		echo %logger% Process create [!dirArray[%%n]!] directory : Directory already exists.
		echo %logger% Process create [!dirArray[%%n]!] directory : Directory already exists. >> %loggerFile%
	)
)

set assemblyArray[0]=%DestDirectoryPackagesInt%
set assemblyArray[1]=%DestDirectoryPackagesExtMicrosoft%
set assemblyArray[2]=%DestDirectoryPackagesExtSQLite%
set assemblyArray[3]=%DestDirectoryPackagesExtSQLitePCLRaw%
set assemblyArray[4]=%DestDirectoryPackagesExtSystem%
set assemblyArray[5]=%DestDirectoryPackagesExtXceed%

set assemblyDll[0]=XtrmAddons
set assemblyDll[1]=Microsoft
set assemblyDll[2]=SQLite
set assemblyDll[3]=SQLitePCLRaw
set assemblyDll[4]=System
set assemblyDll[5]=Xceed


for /l %%n in (0,1,5) do (
	set /a count=0
	for %%x in (%DestDirectory%\!assemblyDll[%%n]!.*.dll) do set /a count+=1
	
	echo %logger% !count! [!assemblyDll[%%n]!.*.dll] files found.
	echo %logger% !count! [!assemblyDll[%%n]!.*.dll] files found. >> %loggerFile%

	if !count! GTR 0 (
		move /y "%DestDirectory%\!assemblyDll[%%n]!.*.dll" !assemblyArray[%%n]!
		echo %logger% Process moving %count% [!assemblyDll[%%n]!.*.dll] into Bin directory : Done.
		echo %logger% Process moving %count% [!assemblyDll[%%n]!.*.dll] into Bin directory : Done. >> %loggerFile%
	)
)

:: -------------------------------------------------------------------------------
:: Processor for destination [\Bin] directory of the application 
:: This directory contains all binaries assemblies necessaries for the application.
:: -------------------------------------------------------------------------------

:: Process create [\Bin] directory
if not exist %DestDirectoryBin% (
    mkdir %DestDirectoryBin%
	echo %logger% Process create [{DestDirectory}\Bin] directory : Done.
	echo %logger% Process create [{DestDirectory}\Bin] directory : Done. >> %loggerFile%
) else (
	echo %logger% Process create [{DestDirectory}\Bin] directory : Directory already exists.
	echo %logger% Process create [{DestDirectory}\Bin] directory : Directory already exists. >> %loggerFile%
)

:: Process move all [.dll] into Bin directory
set count=0
for %%x in ("%DestDirectory%\*.dll") do set /a count+=1

echo %logger% %count% [.dll] file(s) found !
echo %logger% %count% [.dll] file(s) found ! >> %loggerFile%

if %count% GTR 0 (
	move /y "%DestDirectory%\*.dll" "%DestDirectoryBin%"
	echo %logger% Process moving %count% [.dll] into Bin directory : Done.
	echo %logger% Process moving %count% [.dll] into Bin directory : Done. >> %loggerFile%
)



:: -------------------------------------------------------------------------------
:: Processor for destination [\Local] directory of the application 
:: This directory contains all local culture binaries assemblies necessaries for the application.
:: -------------------------------------------------------------------------------

:: Process create Local directory
if not exist %DestDirectoryLocal% (
    mkdir %DestDirectoryLocal%
	echo %logger% Process create [{DestDirectory}\Local] directory : Done.
	echo %logger% Process create [{DestDirectory}\Local] directory : Done. >> %loggerFile%
) else (
	echo %logger% Process create [{DestDirectory}\Local] directory : Directory already exists
	echo %logger% Process create [{DestDirectory}\Local] directory : Directory already exists. >> %loggerFile%
)

set LocalArray[0]=de
set LocalArray[1]=en
set LocalArray[2]=en-GB
set LocalArray[3]=es
set LocalArray[4]=fr
set LocalArray[5]=fr-FR
set LocalArray[6]=hu
set LocalArray[7]=it
set LocalArray[8]=pt-BR
set LocalArray[9]=ro
set LocalArray[10]=ru
set LocalArray[11]=sv
set LocalArray[12]=us
set LocalArray[13]=zh-Hans


for /l %%n in (0,1,13) do (
	echo %logger% Local Source : %DestDirectory%\!LocalArray[%%n]!
	echo %logger% Local Source : %DestDirectory%\!LocalArray[%%n]! >> %loggerFile%

	echo %logger% Local Destination : %DestDirectoryLocal%\!LocalArray[%%n]!
	echo %logger% Local Destination : %DestDirectoryLocal%\!LocalArray[%%n]! >> %loggerFile%
	
	if exist %DestDirectory%\!LocalArray[%%n]! ( 
		move /y %DestDirectory%\!LocalArray[%%n]! %DestDirectoryLocal%\!LocalArray[%%n]!

		echo %logger% Process moving directory to Local [!LocalArray[%%n]!] : Done.
		echo %logger% Process moving directory to Local [!LocalArray[%%n]!] : Done. >> %loggerFile%
	) else (
		echo %logger% Process moving directory to Local [!LocalArray[%%n]!] : Directory not found !
		echo %logger% Process moving directory to Local [!LocalArray[%%n]!] : Directory not found ! >> %loggerFile%
	)
)

echo BATCH POSTBUILD # ####################################### END