@echo off

:: ----------------------------------------------------------------
:: Set prefix for logs
:: Set file name to append logs
:: ----------------------------------------------------------------
set logger=BATCH DEBUG #
set loggerFile=compilation.log

echo %logger% #######################################

:: Set path to sources directory of the application.
set SrcDirectory=%cd%\..\..
echo %logger% Source Directory : %SrcDirectory%
echo %logger% Source Directory : %SrcDirectory% > %loggerFile%

:: Call scripts.
echo %logger% Process calls of scripts
echo %logger% Process calls of scripts >> %loggerFile%

echo %logger% ####################################### END

call %SrcDirectory%\build.bat
call %SrcDirectory%\prebuild.bat
call %SrcDirectory%\postbuild.bat