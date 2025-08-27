# JsonPlaceholderApi

## 📌 Visao Geral do Projeto
Este projeto é uma API desenvolvida em **ASP.NET Core 8**, organizada em camadas seguindo boas práticas de **Clean Architecture, SOLID e Clean Code**.  

O objetivo principal é **integrar com a API pública [JSONPlaceholder](https://jsonplaceholder.typicode.com/)**, permitindo:  
- Buscar dados externos (ex.: posts).  
- Processar e transformar os dados recebidos.  
- Persistir os dados em um **banco de dados SQL Server**, evitando duplicações.  
- Disponibilizar endpoints REST documentados via **Swagger**.  

Além disso, o projeto conta com:  
- **DTOs e AutoMapper** para mapeamento entre entidades e objetos de transferência.  
- **Migrations** para versionamento e criação de tabelas.  
- **Testes unitários** com **xUnit + Moq**.  

---
📂 Estrutura da Solução
A solução principal (JsonPlaceholderApi.sln) está dentro da pasta JsonPlaceholderApi, que contém o projeto da API.

O projeto de testes JsonPlaceholderApi.Tests está fora da pasta da API, em um repositório separado:

C:\Projetos\
│
├── JsonPlaceholderApi.Tests/     # Projeto de testes (xUnit + Moq)
└── JsonPlaceholderApi/           # Projeto da API + JsonPlaceholderApi.sln

Observação:
Para executar a solução completa com testes, você deve clonar os dois repositórios e manter a estrutura acima.

## ⚙️ Instruções de Configuração

### 1. Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recomendado)  
- Git  

### 2. Clonar o Repositório
Clone primeiro o repositório da API:
JsonPlaceholderApi

Depois clone o repositório de testes uma pasta acima:
JsonPlaceholderApi.Tests

A estrutura final deve ficar assim:

C:\Projetos\
├── JsonPlaceholderApi.Tests/
└── JsonPlaceholderApi/
    └── JsonPlaceholderApi.sln

### 3. Configurar o Banco de Dados
- No arquivo appsettings.json, configure a connection string para o seu SQL Server:

"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=JsonPlaceholderDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

### 4. Aplicar Migrations
- Ferramentas -> Gerenciador de Pacotes do NuGet -> Console do Gerenciador de Pacotes

add-migration CriandoBancoDeDados

update-database

### 5. Executar o Projeto

