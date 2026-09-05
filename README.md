Sistema de Gestão de Consultas UVV

Aplicação Web desenvolvida em C#, utilizando ASP.NET Core MVC e Entity Framework Core, para gerenciamento de usuários e consultas médicas ou profissionais.

O sistema permite que usuários criem uma conta, realizem login e gerenciem suas próprias consultas, incluindo cadastro, visualização, edição e exclusão.
Tecnologias utilizadas

    C#
    .NET 6
    ASP.NET Core MVC
    Razor Views
    Entity Framework Core 6
    SQL Server 2022
    Docker
    Data Annotations
    Cookie Authentication

Funcionalidades

    Cadastro de usuários
    Validação dos dados no servidor
    Hash das senhas
    Login de usuários
    Autenticação baseada em Cookies
    Logout
    Cadastro de consultas
    Listagem das consultas do usuário autenticado
    Edição de consultas
    Exclusão de consultas
    Proteção das rotas de consultas com [Authorize]
    Associação de cada consulta ao usuário que a cadastrou
    Persistência dos dados utilizando Entity Framework Core e SQL Server

Estrutura do projeto

SistemaConsultasUVV/
├── Controllers/
│   ├── ConsultaController.cs
│   ├── HomeController.cs
│   └── UsuarioController.cs
├── Data/
│   └── AppDbContext.cs
├── Migrations/
│   ├── 20260828015849_InitialCreate.cs
│   ├── 20260828015849_InitialCreate.Designer.cs
│   └── AppDbContextModelSnapshot.cs
├── Models/
│   ├── Consulta.cs
│   ├── ErrorViewModel.cs
│   └── Usuario.cs
├── Views/
│   ├── Consulta/
│   ├── Home/
│   ├── Shared/
│   └── Usuario/
├── wwwroot/
├── Program.cs
├── appsettings.json
└── SistemaConsultasUVV.csproj

Requisitos

Para executar o projeto, é necessário ter instalado:

    .NET 6 SDK
    Docker
    SQL Server 2022 através de Docker
    Entity Framework Core CLI (dotnet-ef)

Para verificar a instalação do .NET:

dotnet --version

Para verificar o Docker:

docker --version

Para verificar o Entity Framework Core CLI:

dotnet ef --version

Banco de dados

O projeto utiliza SQL Server 2022, executado em um container Docker.

A aplicação está configurada para utilizar a porta 1433 e o banco de dados:

SistemaConsultasUVV

Executando o SQL Server com Docker

Caso o container sqlserver-uvv já exista, ele pode ser iniciado com:

docker start sqlserver-uvv

Para verificar se o container está em execução:

docker ps

O container deve aparecer utilizando a porta:

0.0.0.0:1433->1433/tcp

Para verificar os logs do SQL Server:

docker logs sqlserver-uvv

O SQL Server deve estar pronto para receber conexões antes da execução das migrations.
Criando o container do SQL Server

Caso o ambiente ainda não possua o container sqlserver-uvv, ele pode ser criado utilizando a imagem do SQL Server 2022:

docker run \
  --name sqlserver-uvv \
  -e ACCEPT_EULA=Y \
  -e MSSQL_SA_PASSWORD="SUA_SENHA_FORTE" \
  -p 1433:1433 \
  -d mcr.microsoft.com/mssql/server:2022-latest

Substitua SUA_SENHA_FORTE por uma senha forte para o usuário sa.

    Importante: não armazene senhas reais no repositório.

Configuração da Connection String

Por segurança, a Connection String não contém a senha diretamente no appsettings.json.

O projeto utiliza .NET User Secrets para armazenar a Connection String durante o desenvolvimento.

Na pasta raiz do projeto, inicialize os User Secrets:

dotnet user-secrets init

Configure a Connection String:

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=SistemaConsultasUVV;User Id=sa;Password=SUA_SENHA_FORTE;TrustServerCertificate=True;"

Para verificar a configuração:

dotnet user-secrets list

    Nunca publique senhas, tokens ou outras informações sensíveis no GitHub.

Entity Framework Core e Migrations

O projeto utiliza a abordagem Code First do Entity Framework Core.

A Migration inicial do projeto é:

20260828015849_InitialCreate

Para visualizar as migrations:

dotnet ef migrations list

Para criar ou atualizar o banco de dados conforme as migrations existentes:

dotnet ef database update

Após executar o comando, o banco de dados deverá estar atualizado.
Executando o projeto

Na pasta raiz do projeto, execute:

dotnet restore

Depois:

dotnet build

Com o SQL Server em execução, aplique as migrations:

dotnet ef database update

Por fim, execute a aplicação:

dotnet run

O terminal exibirá o endereço em que a aplicação estará disponível, por exemplo:

https://localhost:7245

Abra o endereço apresentado no navegador.
Fluxo de utilização

O fluxo principal do sistema é:

    Acessar a página de cadastro.
    Criar um novo usuário.
    Realizar login com o e-mail e senha cadastrados.
    Acessar a área de consultas.
    Cadastrar uma nova consulta.
    Visualizar as consultas cadastradas pelo usuário.
    Editar uma consulta.
    Excluir uma consulta.
    Realizar logout.

Segurança

A aplicação utiliza autenticação baseada em Cookies.

As rotas relacionadas às consultas são protegidas pelo atributo:

[Authorize]

Após o login, o identificador do usuário é armazenado nas Claims de autenticação.

As consultas são associadas ao usuário autenticado por meio do UsuarioId.

Além da proteção da rota, as operações de consulta verificam se o registro pertence ao usuário autenticado antes de permitir sua edição ou exclusão.

As senhas dos usuários não são armazenadas em texto puro. O sistema utiliza PasswordHasher<Usuario> para gerar e verificar o hash das senhas.
Validação

Os modelos utilizam Data Annotations para validação dos dados no servidor.

Entre as validações utilizadas estão:

[Required]
[EmailAddress]
[StringLength]

Essas validações garantem que os dados enviados pelos usuários atendam aos requisitos definidos pelo sistema.
Arquitetura

O projeto utiliza o padrão arquitetural MVC (Model-View-Controller), separando as responsabilidades da aplicação:

    Models: representam as entidades e regras de validação dos dados.
    Views: responsáveis pela interface apresentada ao usuário.
    Controllers: responsáveis pelo fluxo das requisições e comunicação entre Views e Models.
    Data: contém o AppDbContext, responsável pela comunicação com o banco de dados.

O AppDbContext é registrado no contêiner de Injeção de Dependência no arquivo Program.cs.

A aplicação também configura o pipeline de autenticação e autorização na ordem adequada:

app.UseAuthentication();
app.UseAuthorization();

Demonstração em vídeo

O vídeo demonstrativo apresenta o funcionamento do sistema, incluindo:

    Cadastro de usuário
    Login
    Cadastro de consulta
    Visualização das consultas
    Edição de consulta
    Exclusão de consulta

Vídeo demonstrativo:
https://www.youtube.com/watch?v=mpdIMp0iOtQ
Integrantes

Em ordem alfabética:

    DANIELE ABDON NASCIMENTO
    DÉBORA ABDON NASCIMENTO

Repositório

O código-fonte do projeto está disponível no GitHub:

https://github.com/DebsAbdon/SistemaConsultasUVV
Observações

Para executar o projeto corretamente:

    O Docker deve estar instalado e em execução.
    O container do SQL Server deve estar ativo.
    A Connection String deve estar configurada por meio dos User Secrets.
    As migrations devem ser aplicadas com dotnet ef database update.

As informações sensíveis, como senhas do banco de dados, não devem ser adicionadas ao repositório.