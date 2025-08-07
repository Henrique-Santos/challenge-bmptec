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
  Chu.Bank.Inc.Api.UnitTests
  Chu.Bank.Inc.Api.IntegrationTests
```