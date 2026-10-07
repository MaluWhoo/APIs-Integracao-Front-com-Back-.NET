<div align="center">

<img width="100%" src="https://capsule-render.vercel.app/api?type=waving&color=0:000000,100:FFB3DE&height=120&section=header&text=Produtos%20API&fontSize=34&fontColor=ffffff&fontAlignY=40&animation=fadeIn" />

### API REST para gerenciamento de produtos

![.NET 10](https://img.shields.io/badge/.NET-10-0f172a?style=flat-square&logo=dotnet&logoColor=C587A7)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-0f172a?style=flat-square&logo=dotnet&logoColor=FFB3DE)
![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-0f172a?style=flat-square&logo=dotnet&logoColor=C587A7)
![SQLite](https://img.shields.io/badge/SQLite-0f172a?style=flat-square&logo=sqlite&logoColor=FFB3DE)

<img src="https://capsule-render.vercel.app/api?type=rect&color=C587A7&height=2&width=100%" />
</div>

## Sobre

API em ASP.NET Core para cadastrar e gerenciar produtos. A aplicação organiza o código em controller, service e repository, e usa Entity Framework Core com SQLite para acessar os dados.

## Funcionalidades

- Listar todos os produtos.
- Consultar produto por ID.
- Criar, atualizar e remover produtos.
- Validar preço e quantidade e impedir nomes duplicados.
- Documentar e explorar os endpoints com Swagger em desenvolvimento.

## Tecnologias

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10
- SQLite
- Swagger / OpenAPI

## Requisitos

- .NET 10 SDK.
- Ferramenta `dotnet-ef` para aplicar as migrações. Se necessário, instale com `dotnet tool install --global dotnet-ef`.

## Executar

Na pasta `Produtos-BackEnd`:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

A API inicia em `http://localhost:5027` pelo perfil HTTP. Com o ambiente de desenvolvimento ativo, acesse [`http://localhost:5027/swagger`](http://localhost:5027/swagger) para explorar e testar os endpoints.

O banco `minhaapi.db` é criado localmente pelo SQLite e está excluído do Git. As migrações versionadas em `Migrations/` criam a estrutura do banco.

## Endpoints

A rota base é `/api/produto`.

| Método | Rota | Ação |
| --- | --- | --- |
| `GET` | `/api/produto` | Lista todos os produtos |
| `GET` | `/api/produto/{id}` | Busca um produto pelo ID |
| `POST` | `/api/produto` | Cadastra um produto |
| `PUT` | `/api/produto/{id}` | Atualiza um produto |
| `DELETE` | `/api/produto/{id}` | Remove um produto |

Exemplo de corpo para `POST` e `PUT`:

```json
{
  "nome": "Caderno",
  "preco": 15.9,
  "quantidade": 10
}
```

## CORS

O back-end permite chamadas da aplicação Angular em `http://localhost:4200`, conforme a política `PermitirAngular` em `Program.cs`. Se o front-end for executado em outra origem, ajuste essa política.

## Organização do código

```text
Controllers/   Endpoints e respostas HTTP
Data/          Contexto do Entity Framework Core
Migrations/    Histórico de alterações do esquema do banco
Models/        Entidade Produto
Repositories/  Consultas e persistência
Services/      Regras de negócio
```

<div align="center">
<img src="https://capsule-render.vercel.app/api?type=rect&color=C587A7&height=2&width=100%" />
<a href="../README.md">Voltar ao README principal</a>
<img width="100%" src="https://capsule-render.vercel.app/api?type=waving&color=0:FFB3DE,100:000000&height=80&section=footer&animation=fadeIn" />
</div>
