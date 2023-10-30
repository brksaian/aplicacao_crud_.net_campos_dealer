# CRUD de clientes, venda, produto.
Projeto utiliza o [.NET Framework 6.0](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0) em uma estrutura MVC, com [bootstrap](https://getbootstrap.com/). Feito no [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/community/)

Para instalar o banco:
1. Alterar a string de dados do banco no arquivo [appsettings.json](https://github.com/brksaian/aplicacao_crud_.net_campos_dealer/blob/master/Teste/appsettings.json)
2. Na barra superior selecionar a opção Tools (Ferramentas) -> NuGet Package Maneger (Gerenciador de pacotes do NuGet) -> NuGet Package Maneger Console (Console do Gerenciador de pacotes)
3. Rodar o comando ```Update-Database -Context BancoContext```
