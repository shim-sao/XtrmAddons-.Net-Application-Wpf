@echo off

:: ----------------------------------------------------------------
:: Delete necessaries directories.
:: ----------------------------------------------------------------
if exist %TargetDir%Local (
	rmdir /S /Q %TargetDir%Local

	echo Delete %TargetDir%Local : Done.
) else (
	echo Delete %TargetDir%Local : Directory not found.
)

if exist %TargetDir%Packages (
	rmdir /S /Q %TargetDir%Packages

	echo Delete %TargetDir%Packages : Done.
) else (
	echo Delete %TargetDir%Packages : Directory not found.
)