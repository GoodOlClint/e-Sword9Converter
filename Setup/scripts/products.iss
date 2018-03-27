#include "isxdl\isxdl.iss"

[CustomMessages]
DependenciesDir=MyProgramDependencies

;de.isxdl_langfile=german2.ini
en.isxdl_langfile=english.ini
pl.isxdl_langfile=polish.ini
es.isxdl_langfile=spanish.ini

en.depdownload_title=Download dependencies

en.depinstall_title=Install dependencies

en.depinstall_status=Installing %1... (May take a few minutes)

en.depdownload_msg=The following applications are required before setup can continue:%n%1%nDownload and install now?

en.depinstall_missing=%1 must be installed before setup can continue. Please install %1 and run Setup again.


[Files]
Source: "scripts\isxdl\polish.ini"; Flags: dontcopy
Source: "scripts\isxdl\spanish.ini"; Flags: dontcopy
Source: "scripts\isxdl\english.ini"; Flags: dontcopy

[Code]
var
	installMemo, downloadMemo, downloadMessage: string;

procedure InstallPackage(PackageName, FileName, Title, Size, URL: string);
var
	path: string;
begin
	installMemo := installMemo + '%1' + Title + #13;
	
	path := ExpandConstant('{src}{\}') + CustomMessage('DependenciesDir') + '\' + FileName;
	if not FileExists(path) then begin
		path := ExpandConstant('{tmp}{\}') + FileName;
		
		if not FileExists(path) then begin
			downloadMemo := downloadMemo + '%1' + Title + #13;
			downloadMessage := downloadMessage + Title + ' (' + Size + ')' + #13;
			
			isxdl_AddFile(URL, path);
		end;
	end;
	SetIniString('install', PackageName, path, ExpandConstant('{tmp}{\}dep.ini'));
end;

function NextButtonClick(CurPage: Integer): Boolean;
begin
	Result := true;

	if CurPage = wpReady then begin

		if downloadMemo <> '' then begin
			// only change isxdl language if it is not english because isxdl default language is already english
			if ActiveLanguage() <> 'en' then begin
				ExtractTemporaryFile(CustomMessage('isxdl_langfile'));
				isxdl_SetOption('language', ExpandConstant('{tmp}{\}') + CustomMessage('isxdl_langfile'));
			end;
			//isxdl_SetOption('title', FmtMessage(SetupMessage(msgSetupWindowTitle), [CustomMessage('appname')]));
			
			if SuppressibleMsgBox(FmtMessage(CustomMessage('depdownload_msg'), [downloadMessage]), mbConfirmation, MB_YESNO, IDYES) = IDNO then
				Result := false
			else if isxdl_DownloadFiles(StrToInt(ExpandConstant('{wizardhwnd}'))) = 0 then
				Result := false;
		end;
	end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
	s: string;
begin
	if downloadMemo <> '' then
		s := s + CustomMessage('depdownload_title') + ':' + NewLine + FmtMessage(downloadMemo, [Space]) + NewLine;
	if installMemo <> '' then
		s := s + CustomMessage('depinstall_title') + ':' + NewLine + FmtMessage(installMemo, [Space]) + NewLine;

	s := s + MemoDirInfo + NewLine + NewLine + MemoGroupInfo
	
	if MemoTasksInfo <> '' then
		s := s + NewLine + NewLine + MemoTasksInfo;

	Result := s
end;
