# GeekBurger-Production

### API
  http://fiap-12net-production.azurewebsites.net/api/production/areas

### Atualização do pacote
1. Faça o download do .exe do NuGet: https://www.nuget.org/downloads
2. Abra o powershell e navegue até a pasta de Downloads onde o .exe se encontra
3. Substitua o caminho do pacote atualizado abaixo e execute o comando:

.\nuget.exe push {package_path} -Source http://nugetfiap12.azurewebsites.net/nuget 8db3cebeef8fd6fb85c4c187160b53d1

### Exclusão do pacote

.\nuget.exe delete {package_name} {package_version} -Source http://nugetfiap12.azurewebsites.net/nuget -apikey 8db3cebeef8fd6fb85c4c187160b53d1
