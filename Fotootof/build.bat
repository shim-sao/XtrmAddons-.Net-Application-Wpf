@echo off
setlocal EnableDelayedExpansion

:: -------------------------------------------------------------------------------
:: Set prefix for logs
:: Set file name to append logs
:: -------------------------------------------------------------------------------
set log=BATCH %mode% #
set logFile=compilation.%mode%.log

:: -------------------------------------------------------------------------------
:: Start logging compilation.
:: -------------------------------------------------------------------------------
echo %log% > %logFile%
call:LogFunc "########################################################## Build"

:: -------------------------------------------------------------------------------
:: Set path to directory of the sources and application.
:: -------------------------------------------------------------------------------
set SrcSol=%cd%\
call:LogFunc "SrcSol : %SrcSol%"

set SrcProj=%cd%\XtrmAddons.Fotootof\
call:LogFunc "SrcProj : %SrcProj%"

set DestProj=%cd%\XtrmAddons.Fotootof\Bin\%mode%\
call:LogFunc "DestProj : %DestProj%"

set DestLocal=%cd%\XtrmAddons.Fotootof\Bin\%mode%\Local\
call:LogFunc "DestLocal : %DestLocal%"

set DestPack=%cd%\XtrmAddons.Fotootof\Bin\%mode%\Packages\
call:LogFunc "DestPack : %DestPack%"

set DestPackExternal=%cd%\XtrmAddons.Fotootof\Bin\%mode%\Packages\External\
call:LogFunc "DestPackExternal : %DestPack%"

set DestPackInternal=%cd%\XtrmAddons.Fotootof\Bin\%mode%\Packages\Internal\
call:LogFunc "DestPackInternal : %DestPackInternal%"

set DestPlg=%cd%\XtrmAddons.Fotootof\Bin\%mode%\Plugins\
call:LogFunc "DestPlg : %DestPlg%"
:: -------------------------------------------------------------------------------
call:LogFunc "########################################################## /Build"

if %prebuild% == True (
call:LogFunc "########################################################## Prebuild"
:: -------------------------------------------------------------------------------
:: Delete necessaries directories.
:: -------------------------------------------------------------------------------
set arrToDel[0]=%DestLocal%
set arrToDel[1]=%DestPack%
call:LogFunc "List directories to delete : 2"

for /l %%n in (0,1,1) do (
	call:LogFunc "Check !arrToDel[%%n]!"
	
	if exist !arrToDel[%%n]! (
		rmdir /S /Q !arrToDel[%%n]!

		call:LogFunc "Process delete !arrToDel[%%n]! : Done."
	) else (
		call:LogFunc "Process delete !arrToDel[%%n]! : Directory not found."
	)
)
:: -------------------------------------------------------------------------------
call:LogFunc "########################################################## /Prebuild"
)

call:LogFunc "########################################################## Postbuild"
:: -------------------------------------------------------------------------------
:: Processor to move assemblies into \Plugins\ directories of the application.
:: This directory contains all necessaries binaries assemblies for the application.
:: -------------------------------------------------------------------------------
if not exist %DestPlg% (
	mkdir %DestPlg%
	call:LogFunc "Create %DestPlg% : Done."
) else (
	call:LogFunc "Create %DestPlg% : Directory already exists"
)

set arrDll[0]=XtrmAddons.*Plugin*

set arrDestPack[0]=%DestPlg%

for /l %%n in (0,1,0) do (
	set /a count=0
	for %%x in (%DestProj%\!arrDll[%%n]!.dll) do set /a count+=1
	call:LogFunc "!count! !arrDll[%%n]!.dll files found."

	if !count! GTR 0 (
		move /Y "%DestProj%\!arrDll[%%n]!.dll" !arrDestPack[%%n]!
		call:LogFunc "Moving %count% !arrDll[%%n]!.dll to !arrDestPack[%%n]! : Done."
	)
)
:: -------------------------------------------------------------------------------



:: -------------------------------------------------------------------------------
:: Processor create destination \Packages\ directories of the application.
:: Create all sub-directories.
:: -------------------------------------------------------------------------------
set arrPack[0]=%DestPack%
set arrPack[1]=%DestPackInternal%
set arrPack[2]=%DestPackExternal%
set arrPack[3]=%DestPackExternal%EntityFramework\
set arrPack[4]=%DestPackExternal%Log4net\
set arrPack[5]=%DestPackExternal%Microsoft\
set arrPack[6]=%DestPackExternal%netstandard\
set arrPack[7]=%DestPackExternal%Newtonsoft\
set arrPack[8]=%DestPackExternal%Others\
set arrPack[9]=%DestPackExternal%Remotion\
set arrPack[10]=%DestPackExternal%SQLite\
set arrPack[11]=%DestPackExternal%SQLitePCLRaw\
set arrPack[12]=%DestPackExternal%System\
set arrPack[13]=%DestPackExternal%Xceed\
set arrPack[14]=%DestPackExternal%SQLite-net\

for /l %%n in (0,1,14) do (
	if not exist !arrPack[%%n]! (
		mkdir !arrPack[%%n]!
		call:LogFunc "Create !arrPack[%%n]! : Done."
	) else (
		call:LogFunc "Create !arrPack[%%n]! directory : Directory already exists."
	)
)
:: -------------------------------------------------------------------------------



:: -------------------------------------------------------------------------------
:: Processor to move assemblies into \Packages\ directories of the application.
:: This directory contains all necessaries binaries assemblies for the application.
:: -------------------------------------------------------------------------------
set arrDll[0]=XtrmAddons*
set arrDll[1]=EntityFramework*
set arrDll[2]=log4net*
set arrDll[3]=Microsoft*
set arrDll[4]=netstandard*
set arrDll[5]=Newtonsoft*
set arrDll[6]=Others*
set arrDll[7]=Remotion*
set arrDll[8]=SQLite.Net*
set arrDll[9]=SQLitePCLRaw*
set arrDll[10]=System*
set arrDll[11]=Xceed*
set arrDll[12]=SQLite-net*

set arrDestPack[0]=%DestPackInternal%
set arrDestPack[1]=%DestPackExternal%EntityFramework\
set arrDestPack[2]=%DestPackExternal%Log4net\
set arrDestPack[3]=%DestPackExternal%Microsoft\
set arrDestPack[4]=%DestPackExternal%netstandard\
set arrDestPack[5]=%DestPackExternal%Newtonsoft\
set arrDestPack[6]=%DestPackExternal%Others\
set arrDestPack[7]=%DestPackExternal%Remotion\
set arrDestPack[8]=%DestPackExternal%SQLite\
set arrDestPack[9]=%DestPackExternal%SQLitePCLRaw\
set arrDestPack[10]=%DestPackExternal%System\
set arrDestPack[11]=%DestPackExternal%Xceed\
set arrDestPack[12]=%DestPackExternal%SQLite-net\

for /l %%n in (0,1,12) do (
	set /a count=0
	for %%x in (%DestProj%\!arrDll[%%n]!.dll) do set /a count+=1
	call:LogFunc "!count! !arrDll[%%n]!.dll files found."

	if !count! GTR 0 (
		move /Y "%DestProj%\!arrDll[%%n]!.dll" !arrDestPack[%%n]!
		call:LogFunc "Moving %count% !arrDll[%%n]!.dll to !arrDestPack[%%n]! : Done."
	)
)

set count=0
for %%x in ("%DestProj%\*.dll") do set /a count+=1
call:LogFunc "%count% *.dll files found."

if %count% GTR 0 (
	move /Y "%DestProj%\*.dll" %DestPackExternal%
	call:LogFunc "Moving %count% *.dll to %DestPackExternal% : Done."
)
:: -------------------------------------------------------------------------------



:: -------------------------------------------------------------------------------
:: Processor move destination \Local\ directories of the application 
:: This directory contains all necessaries local culture binaries assemblies for the application.
:: -------------------------------------------------------------------------------
if not exist %DestLocal% (
    mkdir %DestLocal%
	call:LogFunc "Create %DestLocal% : Done."
) else (
	call:LogFunc "Create %DestLocal% : Directory already exists"
)

set arrLocal[0]=de
set arrLocal[1]=en
set arrLocal[2]=en-GB
set arrLocal[3]=es
set arrLocal[4]=fr
set arrLocal[5]=fr-FR
set arrLocal[6]=hu
set arrLocal[7]=it
set arrLocal[8]=pt-BR
set arrLocal[9]=ro
set arrLocal[10]=ru
set arrLocal[11]=sv
set arrLocal[12]=us
set arrLocal[13]=zh-Hans

for /l %%n in (0,1,13) do (
	if exist %DestProj%\!arrLocal[%%n]! ( 
		move /Y %DestProj%\!arrLocal[%%n]! %DestLocal%\!arrLocal[%%n]!

		call:LogFunc "Moving !arrLocal[%%n]! : Done."
	) else (
		call:LogFunc "Moving !arrLocal[%%n]! : Directory not found."
	)
)
:: -------------------------------------------------------------------------------

call:LogFunc "########################################################## /Postbuild"

if %mode% == debug (
call:LogFunc "########################################################## Postbuild Debug"
:: -------------------------------------------------------------------------------
:: -------------------------------------------------------------------------------
set arrFiles[0]=SQLite-net*.pdb
set arrFiles[1]=XtrmAddons.Net.*
set arrFiles[2]=XtrmAddons.Fotootof.Lib.*
set arrFiles[3]=XtrmAddons.Net.*.dll.config
set arrFiles[4]=XtrmAddons.Fotootof.Culture.*
set arrFiles[5]=XtrmAddons.Fotootof.SQLiteService.*
set arrFiles[6]=XtrmAddons.Fotootof.AddInsContracts.*
set arrFiles[7]=XtrmAddons.Fotootof.Template.*
set arrFiles[8]=XtrmAddons.*.Plugin.*

set foldersList[0]=%DestPackExternal%SQLite-net
set foldersList[1]=%DestPackInternal%
set foldersList[2]=%DestPackInternal%
set foldersList[3]=%DestPackInternal%
set foldersList[4]=%DestPackInternal%
set foldersList[5]=%DestPackInternal%
set foldersList[6]=%DestPackInternal%
set foldersList[7]=%DestPackInternal%
set foldersList[8]=%DestPlg%

for /l %%n in (0,1,8) do (
	set /a count=0
	for %%x in (%DestProj%\!arrFiles[%%n]!) do set /a count+=1
	call:LogFunc "!count! !arrFiles[%%n]! files found."

	if !count! GTR 0 (
		if exist !foldersList[%%n]! (
			move /Y "%DestProj%\!arrFiles[%%n]!" !foldersList[%%n]!
			call:LogFunc "Moving !count! !arrFiles[%%n]! to !foldersList[%%n]! : Done."
		) else (
			call:LogFunc "Moving !count! !arrFiles[%%n]! to !foldersList[%%n]! : Destination directory not found."
		)
	)
)
:: -------------------------------------------------------------------------------
call:LogFunc "########################################################## /Postbuild Debug"
)

if %mode% == release (
call:LogFunc "########################################################## Postbuild Release"
:: -------------------------------------------------------------------------------
:: -------------------------------------------------------------------------------
set arrFiles[0]=SQLite-net*.pdb
set arrFiles[1]=XtrmAddons.Net.*
set arrFiles[2]=XtrmAddons.Fotootof.Lib.*
set arrFiles[3]=XtrmAddons.Net.*.dll.config
set arrFiles[4]=XtrmAddons.Fotootof.Culture.*
set arrFiles[5]=XtrmAddons.Fotootof.SQLiteService.*
set arrFiles[6]=XtrmAddons.Fotootof.AddInsContracts.*
set arrFiles[7]=XtrmAddons.Fotootof.Template.*
set arrFiles[8]=XtrmAddons*Fotootof.pdb
set arrFiles[9]=*.CodeAnalysisLog.xml
set arrFiles[10]=*.lastcodeanalysissucceeded

for /l %%n in (0,1,10) do (
	set /a count=0
	for %%x in (%DestProj%\!arrFiles[%%n]!) do set /a count+=1
	call:LogFunc "!count! !arrFiles[%%n]! files found."

	if !count! GTR 0 (
		del /Q "%DestProj%\!arrFiles[%%n]!"
		call:LogFunc "Delete !count! !arrFiles[%%n]! : Done."
	)
)

call:LogFunc "########################################################## /Postbuild Release"
)
:: -------------------------------------------------------------------------------
goto:eof


:: ----------------------------------------------------------------
:: Function to log compilation.
:: ----------------------------------------------------------------
:LogFunc
echo %log% %~1
echo %log% %~1 >> %logFile%
goto:eof