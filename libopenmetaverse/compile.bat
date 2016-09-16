@echo off 
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" OpenMetaverse.sln /p:Configuration=Release 
:SUCCESS 
echo Build Successful! 
exit /B 0 
:FAIL 
echo Build Failed, check log for reason 
exit /B 1 
