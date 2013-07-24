@ECHO OFF
%SystemRoot%\Microsoft.NET\Framework\v3.5\msbuild.exe LinkMe.proj /t:DevBuild /p:Configuration=Release
IF ERRORLEVEL 1 ECHO LinkMe release x64 build failed!
IF ERRORLEVEL 1 PAUSE
