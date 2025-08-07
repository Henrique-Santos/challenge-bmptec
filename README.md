# Challenge BMPTec - Chu Bank Inc. API

## Como Executar

Clone o projeto

```bash
  git clone https://github.com/Henrique-Santos/challenge-bmptec.git
```

Entre no diretorio raiz

```bash
  cd challenge-bmptec
```

### Rodando localmente com Docker

#### Pré-requisitos

- [Docker](https://www.docker.com/get-started/)
- [Docker Compose](https://docs.docker.com/compose/install/)

Suba as aplicações com o docker compose

```bash
  docker compose -f docker/docker-compose.yml up -d --build
```

#### Acesse a aplicação pelo Swagger em: [link](http://localhost:8080/swagger/index.html)


### Rodando localmente via CLI ou Debug

#### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download)

Rodando o projeto via CLI

```bash
  dotnet run --project src/Chu.Bank.Inc.Api/Chu.Bank.Inc.Api.csproj
```

Para rodar via Debug utilize sua IDE favorita e execute o projeto 

```bash
  Chu.Bank.Inc.Api
```

#### Acesse a aplicação pelo Swagger em: [link](http://localhost:5097/swagger/index.html)

### Rodando os teste via CLI ou Debug

Rodando os teste via CLI

```bash
  dotnet test Chu.Bank.Inc.sln
```

Para rodar via Debug utilize sua IDE favorita e execute os projetos

```bash
  Chu.Bank.Inc.UnitTests
  Chu.Bank.Inc.IntegrationTests
```

## Seed de Dados

Caso queira agilidade nos testes, o banco é criado já com alguns registros pré inseridos.

- Usuários

```bash
  {
    "username": "john_doe",
    "password": "wq^2I#2wgN0GdIZ"
  }

  {
    "username": "mary_doe",
    "password": "u8aatbj45igfgP!"
  }
```

- Contas

```bash
  {
    "id": "728e087f-31d0-44ef-9092-4d0e65c581ee",
    "userId": "cc74a267-ed34-473d-a775-0a9d5e70f969",
    "balance": 10000
  }

  {
    "id": "8b77a349-7518-4607-85c5-2fe7bbb0382a",
    "userId": "95ff55e0-f6da-4fb1-b4b8-6b42b145da77",
    "balance": 15000
  }
```

- Transferencias

```bash
  {
    "fromAccountId": "728e087f-31d0-44ef-9092-4d0e65c581ee",
    "toAccountId": "8b77a349-7518-4607-85c5-2fe7bbb0382a",
    "amount": 1000
  }

  {
    "fromAccountId": "8b77a349-7518-4607-85c5-2fe7bbb0382a",
    "toAccountId": "728e087f-31d0-44ef-9092-4d0e65c581ee",
    "amount": 5000
  }
```