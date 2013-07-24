@ECHO OFF

REM Update to the latest
ECHO Performing SVN update - Windows Explorer may freeze for a while...
svn.exe update
IF ERRORLEVEL 1 ECHO Failed to perform an SVN Update!
IF ERRORLEVEL 1 PAUSE

REM Restore latest database

osql.exe -E -n -i "resources\database\Restore dev database.sql"
IF ERRORLEVEL 1 ECHO Failed to restore dev database!
IF ERRORLEVEL 1 PAUSE

REM Clean and build

PUSHD build
CALL clean_all.cmd
IF "%PROCESSOR_ARCHITECTURE%"=="AMD64" CALL build_all_debug_x64.cmd
IF NOT "%PROCESSOR_ARCHITECTURE%"=="AMD64" CALL build_all_debug_Win32.cmd
POPD

REM Register SearchMe and install into COM+

PUSHD Solution\Search\Engine
CALL InstallComPlusForDev.cmd
POPD

REM Register ISAPI_Rewrite

PUSHD Solution\External\ISAPI_Rewrite
CALL register_this_dir.cmd
POPD
