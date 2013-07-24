@ECHO OFF
%SystemRoot%\Microsoft.NET\Framework\v3.5\msbuild.exe LinkMe.proj /t:DevBuild /p:Configuration=Debug
IF ERRORLEVEL 1 ECHO LinkMe debug build failed!
IF ERRORLEVEL 1 PAUSE
