@echo off
"..\Apps\InstallUtil\bin\LinkMe.InstallUtil.exe" /synonymXlsToSql JobTitleSynonyms.xls data_EquivalentTerms.sql >out.txt
echo Done!
pause
