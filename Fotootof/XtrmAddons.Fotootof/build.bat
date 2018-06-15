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

:: Set path to destination directory \Bin
set DestDirectoryBin=%DestDirectory%\Bin
echo %logger% Destination directory [{DestDirectory}\Bin] : %DestDirectoryBin%
echo %logger% Destination directory [{DestDirectory}\Bin] : %DestDirectoryBin% >> %loggerFile%

:: Set path to destination directory \Local
set DestDirectoryLocal=%DestDirectory%\Local
echo %logger% Destination directory [{DestDirectory}\Local] : %DestDirectoryLocal%
echo %logger% Destination directory [{DestDirectory}\Local] : %DestDirectoryLocal% >> %loggerFile%

echo %logger% # #######################################