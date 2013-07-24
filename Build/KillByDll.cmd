@ECHO OFF

REM Default to the taskkill.exe in the system path
SET LinkMeTaskKillExe=taskkill.exe

REM But if running under WOW64 then use our 64-bit taskkill.exe
IF "%PROCESSOR_ARCHITEW6432%"=="AMD64" SET LinkMeTaskKillExe=%~dp0Tools\taskkill64.exe
IF "%PROCESSOR_ARCHITECTURE%"=="AMD64" SET LinkMeTaskKillExe=%~dp0Tools\taskkill64.exe

%LinkMeTaskKillExe% /fi "modules eq %1" /fi "imagename ne msbuild.exe" /fi "imagename ne devenv.exe" /fi "imagename ne gacutil.exe" /f
