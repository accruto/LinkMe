@echo off
SET LinkMeInstallUtilPath=..\..\..\InstallUtil\venturelogic.installutil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\..\Solution\InstallUtil\bin\Release\venturelogic.installutil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\..\Solution\InstallUtil\bin\Debug\venturelogic.installutil.exe

%LinkMeInstallUtilPath% /runSql . %1 %2 %3 %4

ECHO ******************************************
ECHO You now need to run:
ECHO venturelogic.taskrunner /testrun au.com.venturelogic.linkme.taskRunner.temp.FixDuplicateFileReferenceTask
ECHO THEN recreate_file_indexes.sql
PAUSE
