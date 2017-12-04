pushd %~dp0
call "%VS100COMNTOOLS%\vsvars32.bat"
call "%VS110COMNTOOLS%\vsvars32.bat"
call "%VS120COMNTOOLS%\vsvars32.bat"
call BuildAssemblyVersion.bat

if "%VS110COMNTOOLS%"=="" set VisualStudioVersion=10.0
set app_name=ESFT

rd /s /q %tmpdir%
msbuild ESFT.sln /t:ReBuild
set ee=%errorlevel%


set zipfile=%date:~0,4%%date:~5,2%%date:~8,2%%time:~0,2%%time:~3,2%%time:~6,2%
if "%time:~0,1%"==" " set zipfile=%date:~0,4%%date:~5,2%%date:~8,2%0%time:~1,1%%time:~3,2%%time:~6,2%
set zipfile=%app_name%_%zipfile%_%newversion%.rar
call ConnectToZ_For5.bat
pushd bin
"%ProgramW6432%\winrar\rar.exe" a -r -x*\*.config -x*\sys.ini ..\%zipfile% *.*
move ..\%zipfile% z:\正式版升级包
popd
net use z: /d
pause
exit /b %ee%