@echo off
setlocal EnableDelayedExpansion
set SourceDir=%SourceDirName%\
set DestDir=%DestDirName%\
set DestDirBin=%DestDir%Bin\
set DestDirLocal=%DestDir%Local\
set DestDirAssets=%DestDir%Assets\
set DestDirBinName=%DestDirName%Bin

:: Operation delete on Bin directory
if exist %DestDirBin% (
    :: Delete all files in directory.
    set count=0
    for %%x in (%DestDirBin%*.*) do set /a count+=1
    if !count! GTR 0 ( del /s /q "%DestDirBin%*.*" )

    :: Delete subdirectories in 2 level
    :: Hack Bug : recursive /s failed like shit on non empty...
    for /d %%x in ("%DestDirBin%*") do (
        del /s /q %%x\*.*
        for /d %%y in (%%x) do (
            del /s /q %%y\*.*
            rmdir /s /q %%y
        )
        rmdir /s /q %%x
    )
    rmdir /s /q %DestDirBin%
)

:: Operation delete on Local directory
if exist %DestDirLocal% (
    :: Delete all files in directory.
    set count=0
    for %%x in (%DestDirLocal%*.*) do set /a count+=1
    if !count! GTR 0 ( del /s /q "%DestDirLocal%*.*" )

    :: Delete subdirectories in 2 level
    :: Hack Bug : recursive /s failed like shit on non empty...
    for /d %%x in ("%DestDirLocal%*") do (
        del /s /q %%x\*.*
        for /d %%y in (%%x) do (
            del /s /q %%y\*.*
            rmdir /s /q %%y
        )
        rmdir /s /q %%x
    )
    rmdir /s /q %DestDirLocal%
)
endlocal