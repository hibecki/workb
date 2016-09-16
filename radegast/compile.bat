@echo off 
C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild Radegast.sln 
:SUCCESS 
echo Build Successful! 
exit /B 0 
:FAIL 
echo Build Failed, check log for reason 
exit /B 1 
