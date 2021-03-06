; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Clock11"
#define MyAppVersion "1.3.0"
#define MyAppPublisher "Code A Software"
#define MyAppExeName "Clock11.exe"

[Tasks]
Name: "StartMenuEntry" ; Description: "Start my app when Windows starts" ; GroupDescription: "Windows Startup"; MinVersion: 4,4;

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{DA566D7D-FD55-4B22-AFEA-72BC47A59725}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
PrivilegesRequired=lowest
OutputDir=F:\Eigene Dateien\Desktop
SetupIconFile=icon\clock.ico
OutputBaseFilename=Clock11
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "..\bin\Release\net6.0-windows\publish\de\Clock11.resources.dll"; DestDir: "{app}\de"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\runtimes\win\lib\net6.0\System.Management.dll"; DestDir: "{app}\runtimes\win\lib\net6.0\"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\Clock11.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\Clock11.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\Clock11.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\Clock11.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\Serialization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\net6.0-windows\publish\System.Management.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{userstartup}\{#MyAppName}"; Filename: "{app}\{#MyAppName}"; Tasks:;

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

