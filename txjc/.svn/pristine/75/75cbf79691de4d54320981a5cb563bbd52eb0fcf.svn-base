pushd "%~dp0"
set oldversion=
set newversion=

:: get the revision of the working copy
SubWCRev . .\version.in .\version.txt

if not exist .\oldversion.txt goto :next

for /F "delims=, tokens=1,2,3,4" %%i in (.\oldversion.txt) do (
	set majorversion=%%i
	set minorversion=%%j
	set buildversion=%%k
	set revisionversion=%%l
)
set oldversion=%majorversion%.%minorversion%.%buildversion%.%revisionversion%

:next
for /F "delims=, tokens=1,2,3,4" %%i in (.\version.txt) do (
	set majorversion=%%i
	set minorversion=%%j
	set buildversion=%%k
	set revisionversion=%%l
)
set newversion=%majorversion%.%minorversion%.%buildversion%.%revisionversion%

if "%oldversion%"=="%newversion%" goto :end
copy version.txt oldversion.txt

:: write the AssemblyInfoVersion.cs file with the version info
echo [assembly: System.Reflection.AssemblyVersion("%majorversion%.%minorversion%.%buildversion%.%revisionversion%")] > .\AssemblyInfoVersion.cs
echo [assembly: System.Reflection.AssemblyCompany("中关村在线Z买卖")]>>.\AssemblyInfoVersion.cs
echo [assembly: System.Reflection.AssemblyCopyright("版权所有 (C) 中关村在线 2016")]>>.\AssemblyInfoVersion.cs
echo [assembly: System.Reflection.AssemblyTrademark("")]>>.\AssemblyInfoVersion.cs

:end
popd