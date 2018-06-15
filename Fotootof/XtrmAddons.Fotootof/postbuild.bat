@echo off
setlocal EnableDelayedExpansion

:: Prefix for logs
set logger=BATCH POSTBUILD #

echo %logger% # #######################################

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