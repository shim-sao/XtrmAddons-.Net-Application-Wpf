@echo off
setlocal EnableDelayedExpansion
set SourceDir=%SourceDirName%\
set DestDir=%DestDirName%\
set DestDirBin=%DestDir%Bin\
set DestDirLocal=%DestDir%Local\
set DestDirAssets=%DestDir%Assets\
set DestDirBinName=%DestDirName%Bin

:: Operation create Bin directory
if not exist %DestDirBin% (
    mkdir %DestDirBin%
)

set count=0
for %%x in ("%DestDir%*.dll") do set /a count+=1
if %count% GTR 0 ( move /y "%DestDir%*.dll" "%DestDirBin%" )

:: set count=0
:: for %%x in ("%DestDir%*.xml") do set /a count+=1
:: if %count% GTR 0 ( move /y "%DestDir%*.xml" "%DestDirBin%" )


:: Operation create Local directory
if not exist %DestDirLocal% (
    mkdir %DestDirLocal%
)

if exist "%DestDir%de" ( move /y "%DestDir%de" "%DestDirLocal%de" )
if exist "%DestDir%en" ( move /y "%DestDir%en" "%DestDirLocal%en" )
if exist "%DestDir%en-GB" ( move /y "%DestDir%en-GB" "%DestDirLocal%en-GB" )
if exist "%DestDir%es" ( move /y "%DestDir%es" "%DestDirLocal%es" )
if exist "%DestDir%fr" ( move /y "%DestDir%fr" "%DestDirLocal%fr" )
if exist "%DestDir%fr-FR" ( move /y "%DestDir%fr-FR" "%DestDirLocal%fr-FR" )
if exist "%DestDir%hu" ( move /y "%DestDir%hu" "%DestDirLocal%hu" )
if exist "%DestDir%it" ( move /y "%DestDir%it" "%DestDirLocal%it" )
if exist "%DestDir%pt-BR" ( move /y "%DestDir%pt-BR" "%DestDirLocal%pt-BR" )
if exist "%DestDir%ro" ( move /y "%DestDir%ro" "%DestDirLocal%ro" )
if exist "%DestDir%ru" ( move /y "%DestDir%ru" "%DestDirLocal%ru" )
if exist "%DestDir%sv" ( move /y "%DestDir%sv" "%DestDirLocal%sv" )
if exist "%DestDir%us" ( move /y "%DestDir%us" "%DestDirLocal%us" )
if exist "%DestDir%zh-Hans" ( move /y "%DestDir%zh-Hans" "%DestDirLocal%zh-Hans" )