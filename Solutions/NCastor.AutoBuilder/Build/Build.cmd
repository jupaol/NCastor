SET NET_FRAMEWORK_DIR=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
CALL "%VS100COMNTOOLS%..\..\VC\vcvarsall.bat"

msbuild BuildSolution.proj
pause