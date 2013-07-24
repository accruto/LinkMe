@ECHO OFF
FOR /F %%F IN ('cd') DO ECHO Setting ISAPI_Rewrite installation directory to %%F
FOR /F %%F IN ('cd') DO reg add HKLM\SOFTWARE\Helicon\ISAPI_Rewrite /v InstallDir /f /d %%F
iisreset
