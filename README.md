# Newsletter

Projeto desenvolvido durante uma **live de imersão do canal balta.io** com o objetivo de explorar o novo **Microsoft Agent Framework** do ecossistema .NET.

A aplicação simula o envio automático de uma newsletter semanal: um *worker* roda em segundo plano, coleta os artigos da última semana e utiliza agentes de IA para gerar o **título** e o **conteúdo** da newsletter antes de enviá-la aos inscritos.

## Estrutura do projeto

A solução está organizada em quatro projetos seguindo uma separação clara de responsabilidades:

```
newsletter/
├── Newsletter.Api/     # Host da aplicação (Web API + BackgroundService)
├── Newsletter.Core/    # Modelos, enums, abstrações e configuração
├── Newsletter.Infra/   # Implementações de serviços e repositórios
└── Newsletter.Ai/      # Agentes de IA e prompts (Microsoft Agent Framework)
```

- **Newsletter.Api** — Ponto de entrada da aplicação. Hospeda o `NewsletterWorker`, um `BackgroundService` que dispara o envio da newsletter em intervalos definidos.
- **Newsletter.Core** — Camada de domínio com os modelos (`Article`, `Subscriber`), abstrações de serviços/repositórios e a `Configuration` estática.
- **Newsletter.Infra** — Implementações concretas, como o `NewsletterService` que orquestra a geração e o envio da newsletter.
- **Newsletter.Ai** — Contém os agentes (`TitleGeneratorAgent` e `NewsletterGeneratorAgent`) construídos sobre o Microsoft Agent Framework, além dos *prompts* utilizados por cada agente.

## Stacks utilizadas

- .NET 10
- Microsoft Agent Framework
- OpenAI API

## Como rodar

### Pré-requisitos

- .NET SDK 10
- Uma **API Key** da OpenAI

### 1. Configurar a API Key da OpenAI

Defina a chave da OpenAI via **User Secrets**:

```bash
cd Newsletter.Api
dotnet user-secrets set "OpenAi:ApiKey" "sua-api-key-aqui"
```

Ou diretamente no `appsettings.Development.json`:

```json
{
  "OpenAi": {
    "ApiKey": "sua-api-key-aqui"
  }
}
```

### 2. Executar a aplicação

A partir da raiz do projeto:

```bash
dotnet run --project Newsletter.Api
```

O `NewsletterWorker` será iniciado e executará o fluxo de geração e envio da newsletter conforme o intervalo configurado.

---

> Projeto criado com fins educacionais durante a live com o balta.io.
