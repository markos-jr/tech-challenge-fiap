# ContactManager API - Observabilidade com Prometheus e Grafana

Este projeto demonstra como configurar a **observabilidade de uma API ASP.NET Core** utilizando Prometheus e Grafana, fornecendo métricas detalhadas sobre desempenho, uso e falhas da aplicação, além de promover uma arquitetura limpa e escalável.

---

## :rocket: Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download)
- [Prometheus](https://prometheus.io/)
- [Grafana](https://grafana.com/)
- [Docker](https://www.docker.com/)
- [prometheus-net](https://github.com/prometheus-net/prometheus-net)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)
- [xUnit](https://xunit.net/) para testes automatizados

---

## :file_folder: Estrutura de Pastas

```bash
ContactManager/
├── .github/
│   └── workflows/
│       └── dotnet-desktop.yml         # CI com GitHub Actions
├── docker-compose.yml                 # Orquestração do Prometheus e Grafana
├── prometheus.yml                     # Configuração do Prometheus
├── ContactManager.API/                # Projeto principal da API ASP.NET Core
│   ├── Controllers/
│   │   └── ContactsController.cs      # Controller da API
│   ├── appsettings.json               # Configurações da API
│   ├── launchSettings.json            # Configurações de execução da API
│   ├── ContactManager.API.http        # Arquivo de testes de requisições
│   └── Program.cs                     # Entry point e configuração de middlewares
├── ContactManager.Application/        # Camada de aplicação (casos de uso)
│   └── Interfaces/
│       └── IContactRepository.cs      # Interface de repositório de contatos
├── ContactManager.Domain/             # Camada de domínio (entidades e regras de negócio)
│   └── Entities/
│       └── Contact.cs                 # Entidade Contact
├── ContactManager.Infrastructure/     # Infraestrutura (acesso a dados e serviços externos)
│   └── Data/
│       ├── ContactDbContext.cs        # DbContext do EF Core
│       └── Migrations/                # Migrations da base de dados
├── ContactManager.IntegrationTests/   # Testes de integração
├── ContactManager.Tests/              # Testes unitários
```

---

## :gear: Como Executar o Projeto

1. **Clone o repositório:**

```bash
git clone https://github.com/seu-usuario/contactmanager-observability.git
cd contactmanager-observability
```

2. **Execute a API:**

```bash
cd ContactManager.API
dotnet run
```

3. **Execute Prometheus e Grafana via Docker:**

```bash
docker-compose up -d
```

4. **Acesse os serviços:**

- API: [http://localhost:7054/swagger](http://localhost:7054/swagger)
- Prometheus: [http://localhost:9090](http://localhost:9090)
- Grafana: [http://localhost:3000](http://localhost:3000)

> Usuário padrão do Grafana: `admin` / `admin`

---

## :test_tube: Rodando Testes

Na raiz do projeto:

```bash
dotnet test
```

---

## :bar_chart: Importando Dashboard no Grafana

1. Acesse Grafana e vá em **Dashboards > Import**
2. Escolha o arquivo `ContactManager_Dashboard_Completo.json` localizado em `grafana/dashboards/`
3. Selecione o datasource `Prometheus`

---

## :triangular_ruler: Arquitetura de Observabilidade

- As métricas são expostas pela API no endpoint `/metrics`
- O Prometheus realiza scraping das métricas a cada 15 segundos
- O Grafana é utilizado para visualizar as métricas em tempo real

### Painéis Disponíveis:

- ✅ Requisições por segundo
- ✅ Duração média das requisições
- ✅ Requisições por método HTTP
- ✅ Requisições por código de status HTTP
- ✅ Requisições em progresso
- ✅ Requisições com erro (4xx e 5xx)
- ✅ Uptime da API

---

## :bulb: Lições e Dicas Aprendidas

- Adicionar `UseHttpMetrics()` no `Program.cs` é essencial para exposição automática de métricas.
- A configuração correta do `prometheus.yml` com `scheme: https` e `insecure_skip_verify: true` é necessária ao usar HTTPS com certificado autoassinado.
- O Grafana pode consumir dashboards JSON diretamente com queries PromQL embutidas.
- Para medir corretamente a duração média das requisições, use a expressão:
  ```promql
  rate(http_request_duration_seconds_sum[1m]) / rate(http_request_duration_seconds_count[1m])
  ```
- Mantenha o Docker Desktop atualizado para evitar problemas de rede ao acessar `host.docker.internal`

---

## :construction: Melhorias Futuras

- [ ] Adicionar alertas no Grafana
- [ ] Integrar logs com Loki
- [ ] Dashboards separados por ambiente (dev, prod)
- [ ] Dashboard de performance do banco de dados
- [ ] Configuração para ambientes cloud (Azure Monitor, AWS CloudWatch)

---

## :speech_balloon: Contato

Desenvolvido por [Marcos Jr](https://www.linkedin.com/in/omarkosjr)

> Projeto desenvolvido como parte da trilha de estudos em .NET e Observabilidade com foco em aplicações web e APIs REST.

---

## :camera_flash: Prints do Dashboard

### Visão Geral

![Dashboard - Visão Geral](docs/img/dashboard-overview.png)

### Gráfico: Requisições por Segundo

![Requisições por Segundo](docs/img/requests-per-second.png)

### Gráfico: Duração Média das Requisições

![Duração Média](docs/img/average-duration.png)

> As imagens devem ser adicionadas na pasta `docs/img/` do repositório GitHub para aparecerem corretamente neste README.
