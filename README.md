# 💳 ByteFinance API

API RESTful para gestão financeira desenvolvida em **.NET 10 (C# 13)** utilizando **Clean Architecture** e **Domain-Driven Design (DDD)**.

---

## 🚀 Como Executar e Testar a Aplicação (Passo a Passo)

### Passo 1: Clonar o repositório e restaurar as dependências
```bash
git clone [https://github.com/salseiroop/ByteFinance.git](https://github.com/salseiroop/ByteFinance.git)
cd ByteFinance
dotnet restore
```

---

### Passo 2: Executar a API
*O Entity Framework Core criará o banco SQLite local (`ByteFinance.db`) e aplicará as migrações automaticamente no primeiro start.*

```bash
dotnet run --project src/ByteFinance.API
```

---

### Passo 3: Acessar a interface interativa (Swagger UI)
Com a aplicação em execução, acesse pelo navegador:

> 🌐 **`https://localhost:7224/swagger`**

---

### Passo 4: Testar os endpoints na prática via Swagger

1. **Criar uma transação (`POST /api/v1/transacoes`):**
   * Clique em **`Try it out`** e envie o JSON:
   ```json
   {
     "descricao": "Recebimento de Freelance",
     "valor": 1500.00,
     "tipo": 1,
     "categoriaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
   }
   ```
   * **Retorno esperado:** Status `201 Created`.

2. **Testar validação de regra de negócio (`POST /api/v1/transacoes`):**
   * Altere o campo `"valor"` para `-50.00` e clique em **`Execute`**.
   * **Retorno esperado:** Status `400 Bad Request` no formato RFC 7807 (Problem Details).

3. **Listar as transações (`GET /api/v1/transacoes`):**
   * Clique em **`Try it out`** e em seguida em **`Execute`**.
   * **Retorno esperado:** Status `200 OK` com a lista cadastrada.

---

### Passo 5: Executar a suíte de testes automatizados
Para rodar todos os testes unitários e de integração da solução via terminal:

```bash
dotnet test
```

---

## 🎯 Checklist de Qualidade e Boas Práticas

* [x] **Git:** Commits no padrão **Conventional Commits** (`feat:`, `fix:`, `test:`, `docs:`).
* [x] **Logs:** Log estruturado com `ILogger<T>` em operações-chave e exceções.
* [x] **HTTP Semântico:** Códigos de status apropriados (`201 Created`, `200 OK`, `400 Bad Request`, `404 Not Found`, `500 Internal Error`).
* [x] **OpenAPI / Swagger:** Endpoints e retornos de erro padronizados via **RFC 7807 (Problem Details)**.
* [x] **Documentação do Projeto:** Instruções claras de execução da aplicação e o comando para rodar toda a suíte de testes (`dotnet test`).

---

## 🏛️ Arquitetura da Solução

```text
ByteFinance/
├── 📂 src/
│   ├── ByteFinance.API              # Controllers, Middlewares, Swagger UI e DI
│   ├── ByteFinance.Application      # Casos de Uso, DTOs e Mappings
│   ├── ByteFinance.Domain           # Entidades, Regras de Negócio e Exceções
│   └── ByteFinance.Infrastructure   # DbContext (EF Core), SQLite e Repositórios
└── 📂 tests/
    ├── ByteFinance.Domain.Tests      # Testes Unitários de Regras de Negócio
    ├── ByteFinance.App.Tests         # Testes Unitários de Serviços (Moq)
    └── ByteFinance.IntegrationTests  # Testes de Integração End-to-End (WebApplicationFactory)
```
