rem �E�N���b�N����"�Ǘ��҂Ƃ��Ď��s"

taskkill /im gwx.exe /F

reg add HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\WindowsUpdate /v "DisableOSUpgrade" /t REG_DWORD /d 00000001 /f

wusa.exe /uninstall /kb:3035583 /norestart /quiet
wusa.exe /uninstall /kb:2952664 /norestart /quiet

pause
