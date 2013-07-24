@ECHO OFF

IF NOT EXIST %1res\project.dir.path.txt GOTO noFile

FOR /F %%i IN (%1res\project.dir.path.txt) DO IF NOT "%%i"=="%1" ECHO %1>%1res\project.dir.path.txt
GOTO end

:noFile

ECHO %1>%1res\project.dir.path.txt

:end
