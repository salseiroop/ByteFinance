# 💳 ByteFinance API

API RESTful para gestão financeira desenvolvida em **.NET 10 (C# 13)** utilizando **Clean Architecture** e **Domain-Driven Design (DDD)**.

---

## 📌 Visão Geral do Sistema

O **ByteFinance** oferece uma solução completa para controle financeiro pessoal, englobando o gerenciamento de receitas, despesas, categorias e a geração de relatórios de saldo consolidados em tempo real.

* **Gestão de Categorias:** Cadastro e listagem de categorias para classificação orçamentária (ex: Alimentação, Salário, Lazer).
* **Gestão de Transações:** Registro e exclusão de receitas e despesas associadas a categorias válidas.
* **Filtros e Paginação:** Consulta paginada com suporte a filtros por mês, ano e categoria.
* **Resumo Financeiro Consolidado:** Relatório mensal contendo Total de Receitas, Total de Despesas e Saldo Total calculado em tempo real.

---

## 📋 Pré-requisitos

Antes de iniciar, certifique-se de ter instalado em sua máquina:
* **[.NET 10 SDK](https://dotnet.microsoft.com/download)** ou superior (`dotnet --version`).
* **Git** para clonagem do repositório.

---

## 🚀 Como Executar e Testar a Aplicação (Passo a Passo)

### Passo 1: Clonar o repositório e restaurar as dependências
```bash
git clone https://github.com/salseiroop/ByteFinance.git
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
Com a aplicação em execução, observe no terminal o endereço informado na linha `Now listening on` (ex: `http://localhost:5232` ou `https://localhost:7224`).

Acesse no navegador adicionando `/swagger` ao final da URL exibida no seu console:

> 🌐 **`http://localhost:5232/swagger`**

*(Nota: Substitua a porta `5232` pela porta exata exibida no seu terminal).*

---

### Passo 4: Testar os endpoints na prática via Swagger

1. **Criar uma categoria (`POST /api/v1/categorias`):**
   * Clique em **`Try it out`** e envie o JSON:
   ```json
   {
     "nome": "Alimentação"
   }
   ```
   * **Retorno esperado:** Status `201 Created` contendo o `id` (GUID) da categoria gerada. Copie esse `id`.

2. **Listar as categorias (`GET /api/v1/categorias`):**
   * Clique em **`Try it out`** e em seguida em **`Execute`**.
   * **Retorno esperado:** Status `200 OK` com a lista de categorias cadastradas.

3. **Criar uma transação de receita (`POST /api/v1/transacoes`):**
   * Clique em **`Try it out`**, informe no campo `"categoriaId"` o GUID obtido no item 1 e envie o JSON:
   ```json
   {
     "descricao": "Salário Mensal",
     "valor": 3500.00,
     "data": "2026-08-18T00:00:00Z",
     "tipo": 1,
     "categoriaId": "COLE_O_GUID_DA_CATEGORIA_AQUI"
   }
   ```
   *(Nota: O campo `tipo` aceita `1` para Receita e `2` para Despesa).*
   * **Retorno esperado:** Status `201 Created` contendo o ID da nova transação.

4. **Criar uma transação de despesa (`POST /api/v1/transacoes`):**
   * Envie os dados de uma despesa no mesmo endpoint:
   ```json
   {
     "descricao": "Supermercado",
     "valor": 450.50,
     "data": "2026-08-18T00:00:00Z",
     "tipo": 2,
     "categoriaId": "COLE_O_GUID_DA_CATEGORIA_AQUI"
   }
   ```
   * **Retorno esperado:** Status `201 Created`. Guarde o `id` da transação para o teste de exclusão.

5. **Consultar o resumo financeiro consolidado (`GET /api/v1/transacoes/resumo`):**
   * Preencha os parâmetros de busca: `mes = 8` e `ano = 2026`.
   * Clique em **`Execute`**.
   * **Retorno esperado:** Status `200 OK` apresentando o cálculo do saldo do período:
   ```json
   {
     "totalReceitas": 3500.00,
     "totalDespesas": 450.50,
     "saldoTotal": 3049.50
   }
   ```

6. **Listar transações paginadas e filtradas (`GET /api/v1/transacoes`):**
   * Preencha os parâmetros: `pagina = 1`, `tamanhoPagina = 10`, `mes = 8` e `ano = 2026`.
   * Clique em **`Execute`**.
   * **Retorno esperado:** Status `200 OK` contendo a lista paginada de lançamentos.

7. **Remover uma transação (`DELETE /api/v1/transacoes/{id}`):**
   * Cole o ID da transação de despesa criada no Item 4 e clique em **`Execute`**.
   * **Retorno esperado:** Status `204 No Content`.

8. **Testar validação de regra de negócio e tratamento de erro (`POST /api/v1/transacoes`):**
   * Altere o campo `"valor"` para um número negativo (ex: `-50.00`) e clique em **`Execute`**.
   * **Retorno esperado:** Status `400 Bad Request` padronizado no formato RFC 7807 (Problem Details).

---

### Passo 5: Executar a suíte de testes automatizados
Para rodar todos os testes unitários e de integração da solução via terminal:

```bash
dotnet test
```

---

## 🛠️ Solução de Problemas Comuns (Troubleshooting)

* **Aviso de Certificado SSL Inseguro ao acessar via HTTPS:**
  Caso o navegador bloqueie o acesso ao Swagger em HTTPS, execute no terminal o comando para confiar no certificado de desenvolvimento:
  ```bash
  dotnet dev-certs https --trust
  ```

* **Erro ao conectar no banco de dados SQLite:**
  Caso as tabelas não sejam criadas automaticamente no primeiro `dotnet run`, certifique-se de ter a ferramenta `dotnet-ef` e aplique as migrações manualmente:
  ```bash
  dotnet tool install --global dotnet-ef
  dotnet ef database update --project src/ByteFinance.Infrastructure --startup-project src/ByteFinance.API
  ```

---

## 🎯 Checklist de Qualidade e Boas Práticas

* [x] **Git:** Commits no padrão **Conventional Commits** (`feat:`, `fix:`, `test:`, `docs:`).
* [x] **Logs:** Log estruturado com `ILogger<T>` em operações-chave e exceções.
* [x] **HTTP Semântico:** Códigos de status apropriados (`201 Created`, `200 OK`, `204 No Content`, `400 Bad Request`, `404 Not Found`, `500 Internal Error`).
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
