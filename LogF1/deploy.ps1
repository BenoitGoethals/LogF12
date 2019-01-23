#nuget.exe config -Set repositoryPath="\full-path-to-your-packages-folder\"
#set-executionpolicy -executionpolicy unrestricted -Force

 nuget setapikey faff1a52-0d45-31c9-a7f7-3dcd41456f40 -source http://192.168.0.123:8081/repository/nuget-hosted/ 
# nuget push -ApiKey faff1a52-0d45-31c9-a7f7-3dcd41456f40  -source http://192.168.0.123:8081/repository/nuget-hosted/  .\PostLab\bin\NuGet\PostLab.1.2.0.1.nupkg
nuget sources update -Name "nexus" -source http://192.168.0.123:8081/repository/nuget-hosted/ -User "deploy" -pass "password"


nuget pack LogF1  -IncludeReferencedProjects -properties Configuration=Release


nuget push LogF1.nupkg -Source  http://192.168.0.123:8081/repository/nuget-hosted/ 
