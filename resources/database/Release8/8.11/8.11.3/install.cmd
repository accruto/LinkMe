@echo off
SET LinkMeInstallUtilPath=..\..\..\InstallUtil\LinkMe.InstallUtil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\..\..\Solution\InstallUtil\bin\Debug\LinkMe.InstallUtil.exe

%LinkMeInstallUtilPath% /runSql . %1 %2 %3 %4
PAUSE
