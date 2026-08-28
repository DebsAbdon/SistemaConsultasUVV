Sistema de Gestão de Consultas UVV

Aplicação Web desenvolvida em C# utilizando ASP.NET Core MVC e Entity Framework Core para gerenciamento de usuários e consultas.

Tecnologias utilizadas
C#
ASP.NET Core 6
MVC (Model-View-Controller)
Entity Framework Core 6
SQL Server
Razor Views
Data Annotations
Cookie Authentication
Funcionalidades
Cadastro de usuários
Login e autenticação
Logout
Cadastro de consultas
Listagem das consultas do usuário autenticado
Edição de consultas
Exclusão de consultas
Validação dos dados no servidor
Proteção das rotas de consultas para usuários autenticados
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
│   └── Usuario.cs
├── Views/
│   ├── Consulta/
│   ├── Home/
│   ├── Shared/
│   └── Usuario/
├── Program.cs
├── appsettings.json
└── SistemaConsultasUVV.csproj

Requisitos

Para executar o projeto, é necessário ter instalado:

.NET 6 SDK
SQL Server
Entity Framework Core CLI (dotnet-ef)
Configuração do banco de dados

O projeto utiliza SQL Server através do Entity Framework Core.

A connection string deve ser configurada no arquivo appsettings.json.

Exemplo:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=SistemaConsultasUVV;User Id=sa;Password=SUA_SENHA;TrustServerCertificate=True;"
  }
}


Substitua SUA_SENHA pela senha configurada no seu ambiente.

Executando as Migrations

Dentro da pasta do projeto, execute:

dotnet ef migrations list


Para criar ou atualizar o banco de dados:

dotnet ef database update


A Migration inicial utilizada no projeto é:

20260828015849_InitialCreate

Executando a aplicação

Na pasta raiz do projeto:

dotnet restore
dotnet build
dotnet run


Após iniciar a aplicação, acesse no navegador o endereço HTTPS exibido no terminal, por exemplo:

https://localhost:7245

Fluxo de utilização
Acesse a página de cadastro.
Crie um novo usuário.
Faça login utilizando o e-mail e a senha cadastrados.
Acesse a área de consultas.
Cadastre uma nova consulta.
Visualize suas consultas cadastradas.
Edite uma consulta quando necessário.
Exclua uma consulta quando necessário.
Utilize o logout para encerrar a sessão.
Segurança

As rotas relacionadas às consultas são protegidas utilizando o atributo [Authorize].

A aplicação utiliza autenticação baseada em Cookie. O usuário autenticado possui seu identificador armazenado nas Claims, permitindo vincular as consultas ao usuário correto.

As operações de consulta verificam o UsuarioId, impedindo que um usuário edite ou exclua consultas pertencentes a outro usuário.

Validação

Os modelos utilizam Data Annotations para validação dos dados.

Exemplos utilizados:

[Required]
[EmailAddress]
[StringLength]
Demonstração em vídeo

Vídeo demonstrativo do sistema:

[INSIRA AQUI O LINK DO VÍDEO DO LOOM, YOUTUBE OU OUTRO SERVIÇO]

O vídeo deve demonstrar pelo menos:

Cadastro de usuário
Login
Cadastro de consulta
Visualização das consultas
Edição de consulta
Exclusão de consulta
Integrantes

Adicionar abaixo os nomes dos integrantes do grupo em ordem alfabética:

Nome do integrante 1
Nome do integrante 2
Nome do integrante 3
Repositório

Link do repositório GitHub:

[INSIRA AQUI O LINK DO REPOSITÓRIO]
