#include "scripts\products.iss"
#include "scripts\products\dotnetfx35.iss"
#include "scripts\products\winversion.iss"

[Files]
Source: ..\e-Sword9Converter\bin\Release\eSword9Converter.exe; DestDir: {app}
Source: ..\e-Sword9Converter\bin\Release\System.Data.SQLite.dll; DestDir: {app}
Source: e-Sword 9 Converter- User Guide.chm; DestDir: {app}; DestName: help.chm

[Setup]
MinVersion=0,5.01.2600
AppCopyright=2009
AppName=eSword 9 Converter
AppVerName=eSword 9 Converter 1.0
RestartIfNeededByRun=false
DefaultDirName={pf}\eSword9Converter
AppID={{ED36C999-9843-4A4E-B60A-5152074D5EDD}
UninstallDisplayIcon={app}\eSword9Converter.exe
LicenseFile=License.rtf
AppSupportURL=http://goodolclint.com/e-sword/
AppUpdatesURL=http://goodolclint.com/e-sword/
DefaultGroupName=eSword9Converter

[Languages]
Name: en; MessagesFile: compiler:Default.isl
Name: es; MessagesFile: compiler:Languages\Spanish.isl
Name: pl; MessagesFile: compiler:Languages\Polish.isl

[CustomMessages]
;English Strings
en.win2000sp3_title=Windows 2000 Service Pack 3
en.winxpsp2_title=Windows XP Service Pack 2

;Spanish Strings
es.isxdl_langfile=spanish.ini
es.win2000sp3_title=Windows 2000 Service Pack 3
es.winxpsp2_title=Windows XP Service Pack 2
es.depdownload_title=Download dependencies
es.depinstall_title=Install dependencies
es.depinstall_status=Installing %1... (May take a few minutes)
es.depdownload_msg=The following applications are required before setup can continue:%n%1%nDownload and install now?
es.depinstall_missing=%1 must be installed before setup can continue. Please install %1 and run Setup again.
es.dotnetfx35_size=3 MB - 197 MB
es.dotnetfx35_title=Microsoft .NET Framework 3.5

;Polish Strings
pl.isxdl_langfile=polish.ini
pl.win2000sp3_title=Windows 2000 Service Pack 3
pl.winxpsp2_title=Windows XP Service Pack 2
pl.depdownload_title=Download dependencies
pl.depinstall_title=Install dependencies
pl.depinstall_status=Installing %1... (May take a few minutes)
pl.depdownload_msg=The following applications are required before setup can continue:%n%1%nDownload and install now?
pl.depinstall_missing=%1 must be installed before setup can continue. Please install %1 and run Setup again.
pl.dotnetfx35_size=3 MB - 197 MB
pl.dotnetfx35_title=Microsoft .NET Framework 3.5

[Code]
function InitializeSetup(): Boolean;
begin;
	initwinversion();
	if (not minspversion(5, 0, 3)) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('win2000sp3_title')]), mbError, MB_OK);
		exit;
	end;
	if (not minspversion(5, 1, 2)) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('winxpsp2_title')]), mbError, MB_OK);
		exit;
	end;
	dotnetfx35();
	Result := true;
end;

[Icons]
Name: {userdesktop}\eSword9Converter; Filename: {app}\eSword9Converter.exe; WorkingDir: {app}; IconFilename: {app}\eSword9Converter.exe
Name: {group}\eSword9Converter; Filename: {app}\eSword9Converter.exe; WorkingDir: {app}
Name: {group}\Help; Filename: {app}\help.chm; WorkingDir: {app}
