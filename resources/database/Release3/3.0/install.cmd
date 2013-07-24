@echo off
SET LinkMeInstallUtilPath=..\..\..\InstallUtil\venturelogic.installutil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\..\Solution\InstallUtil\bin\Release\venturelogic.installutil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\..\Solution\InstallUtil\bin\Debug\venturelogic.installutil.exe

%LinkMeInstallUtilPath% /runSql . %1 %2 %3 %4

ECHO ******************************************
ECHO Don't forget to run the rest of the migration!
ECHO See http://intranet/wikime/moin.cgi/ReleaseThreeZeroChecklist
