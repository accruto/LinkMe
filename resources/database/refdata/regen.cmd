@echo off
SET LinkMeInstallUtilPath=..\..\InstallUtil\LinkMe.InstallUtil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\Solution\InstallUtil\bin\Release\LinkMe.InstallUtil.exe
IF NOT EXIST %LinkMeInstallUtilPath% SET LinkMeInstallUtilPath=..\..\..\Solution\InstallUtil\bin\Debug\LinkMe.InstallUtil.exe

%LinkMeInstallUtilPath% /genDataScript %1 %2 %3 %4 <query_creditCardType.sql >refdata_CreditCardType.sql
%LinkMeInstallUtilPath% /genDataScript %1 %2 %3 %4 <query_Industry_etc.sql >refdata_Industry_etc.sql
%LinkMeInstallUtilPath% /genDataScript %1 %2 %3 %4 <query_Locality_etc.sql >refdata_Locality_etc.sql
%LinkMeInstallUtilPath% /genDataScript %1 %2 %3 %4 <query_ProductDefinition_etc.sql >refdata_ProductDefinition_etc.sql
%LinkMeInstallUtilPath% /genDataScript %1 %2 %3 %4 <query_ProductGrantReason.sql >refdata_ProductGrantReason.sql
%LinkMeInstallUtilPath% /genDataScript %1 %2 %3 %4 <query_Publisher.sql >refdata_Publisher.sql
