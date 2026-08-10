Remove-Item -Path .vs,bin,obj -Recurse -Force -ErrorAction SilentlyContinue
dotnet restore
dotnet build