echo off 
set filename=%~dp0\LifesenseServer.exe
set servicename=LifesenseServer
echo ============================������־==================================== >UnInstallService.log  
if exist "%SystemRoot%\Microsoft.NET\Framework\v4.0.30319" goto netOld 
:DispError 
echo ���Ļ�����û�а�װ.net FrameWork 4.0,ж�ؼ�����ֹ 
echo ���Ļ�����û�а�װ.net FrameWork 4.0,ж�ؼ�����ֹ >>UnInstallService.log  
pause 
goto LastEnd 
:netOld 
echo ����ж�ر�����
echo off 
pause 
echo *********************
echo ֹͣ����
net stop %servicename% >>UnInstallService.log 
cd %SystemRoot%\Microsoft.NET\Framework\v4.0.30319 
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil /uninstall %filename% >>UnInstallService.log 
echo ж�ط���
echo ======================================================================= >>UnInstallService.log 
echo *********************
type UnInstallService.log 
echo.
echo �������������Բ鿴��־�ļ�UnInstallService.log�о���Ĳ��������
:LastEnd 
pause 
rem exit