<div align="center">

<img width="100%" src="https://capsule-render.vercel.app/api?type=waving&color=0:000000,100:FFB3DE&height=120&section=header&text=Produtos%20Front-End&fontSize=32&fontColor=ffffff&fontAlignY=40&animation=fadeIn" />

### Interface Angular para gerenciamento de produtos

![Angular 22](https://img.shields.io/badge/Angular-22-0f172a?style=flat-square&logo=angular&logoColor=FFB3DE)
![TypeScript](https://img.shields.io/badge/TypeScript-0f172a?style=flat-square&logo=typescript&logoColor=C587A7)
![RxJS](https://img.shields.io/badge/RxJS-0f172a?style=flat-square&logo=reactivex&logoColor=FFB3DE)

<img src="https://capsule-render.vercel.app/api?type=rect&color=C587A7&height=2&width=100%" />
</div>

## Sobre

Aplicação Angular que consome a [API de produtos](../Produtos-BackEnd/README.md) do projeto. A interface permite listar, consultar, adicionar, editar e remover produtos por meio de chamadas HTTP.

## Funcionalidades

- Exibir a lista de produtos.
- Buscar um produto pelo ID.
- Adicionar produto com nome, preço e quantidade.
- Editar os dados de um produto.
- Remover produto.
- Exibir mensagens de validação e de erro nas operações.

## Tecnologias

- Angular 22 e TypeScript.
- Angular HttpClient e RxJS para integração com a API.
- Formulários reativos.

## Requisitos

- Node.js e npm.
- API do projeto em execução em `http://localhost:5027`.

## Executar localmente

Na pasta `Produtos-FrontEnd`:

```bash
npm install
npm start
```

Abra [`http://localhost:4200`](http://localhost:4200). O comando `npm start` executa o script `ng serve` definido em `package.json`.

Para gerar a versão de produção:

```bash
npm run build
```

## Integração com a API

O serviço de produtos está em `src/app/services/produto.ts` e usa a URL base `http://localhost:5027/api/produto`. A API permite chamadas CORS a partir de `http://localhost:4200`.

Se a API estiver usando outra porta, atualize `apiUrl` no serviço. A API e o front-end precisam estar rodando ao mesmo tempo para listar ou alterar produtos.

## Estrutura

```text
src/app/
├── components/produto-list/  Tela e operações de produtos
├── services/                 Chamadas HTTP
├── app.config.ts             Configuração da aplicação
└── app.routes.ts             Rotas Angular
```

<div align="center">
<img src="https://capsule-render.vercel.app/api?type=rect&color=C587A7&height=2&width=100%" />

<a href="../README.md">Voltar ao README principal</a>
<img width="100%" src="https://capsule-render.vercel.app/api?type=waving&color=0:FFB3DE,100:000000&height=80&section=footer&animation=fadeIn" />
</div>
