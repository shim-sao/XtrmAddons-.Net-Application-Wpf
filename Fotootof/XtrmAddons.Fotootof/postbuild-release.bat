@echo off
setlocal EnableDelayedExpansion

:: Prefix for logs
set logger=BATCH POSTBUILD-RELEASE #

echo %logger% # #######################################

:: Delete all files .pdb in destination directory.
set count=0
for %%x in (%DestDirectory%\*.pdb) do set /a count+=1

echo %logger% %count% [.pdb] file(s) found !
echo %logger% %count% [.pdb] file(s) found ! >> %loggerFile%

if !count! GTR 0 (
	del /S /Q "%DestDirectory%\*.pdb"
	echo %logger% Process delete %count% [.pdb] into destination directory : Done.
	echo %logger% Process delete %count% [.pdb] into destination directory : Done. >> %loggerFile%
)

:: Delete all files .lastcodeanalysissucceeded in destination directory.
set count=0
for %%x in (%DestDirectory%\*.lastcodeanalysissucceeded) do set /a count+=1

echo %logger% %count% [.lastcodeanalysissucceeded] file(s) found !
echo %logger% %count% [.lastcodeanalysissucceeded] file(s) found ! >> %loggerFile%

if !count! GTR 0 (
	del /S /Q "%DestDirectory%\*.lastcodeanalysissucceeded"
	echo %logger% Process delete %count% [.lastcodeanalysissucceeded] into destination directory : Done.
	echo %logger% Process delete %count% [.lastcodeanalysissucceeded] into destination directory : Done. >> %loggerFile%
)

:: Delete all files .dll.config in destination directory.
set count=0
for %%x in (%DestDirectory%\*.dll.config) do set /a count+=1

echo %logger% %count% [.dll.config] file(s) found !
echo %logger% %count% [.dll.config] file(s) found ! >> %loggerFile%

if !count! GTR 0 (
	del /S /Q "%DestDirectory%\*.dll.config"
	echo %logger% Process delete %count% [.dll.config] into destination directory : Done.
	echo %logger% Process delete %count% [.dll.config] into destination directory : Done. >> %loggerFile%
)

:: Delete all files .pdb in destination directory.
:: Do not it recursively
set count=0
for %%x in (%DestDirectory%\*.xml) do set /a count+=1

echo %logger% %count% [.xml] file(s) found !
echo %logger% %count% [.xml] file(s) found ! >> %loggerFile%

if !count! GTR 0 (
	del /Q "%DestDirectory%\*.xml"
	echo %logger% Process delete %count% [.xml] into destination directory : Done.
	echo %logger% Process delete %count% [.xml] into destination directory : Done. >> %loggerFile%
)


echo %logger% # ####################################### END