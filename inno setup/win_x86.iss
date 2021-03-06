; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Minecraft Unity Edition"
#define MyAppVersion "v1.1-indev"
#define MyAppPublisher "SunnyMonster Production"
#define MyAppExeName "Minecraft Unity Edition.exe"
#define MyAppPath "D:\projects\Unity Games\Minecraft Unity Edition\Builds\" + MyAppVersion + "\Windows\x86"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{69B5158C-26E9-4048-827B-772A1B015DA2}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=MCUE_win_x86_setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SetupIconFile="{#MyAppPath}\setup.ico"
UninstallDisplayIcon="{#MyAppPath}\uninstall.ico"   

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#MyAppPath}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppPath}\UnityCrashHandler32.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppPath}\UnityPlayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppPath}\Data\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"; IconFilename: "{#MyAppPath}\uninstall.ico"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

