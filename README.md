# Soft Collection

Веб-модуль взыскания просроченной задолженности для банков, МКК и МФК. Ставится на серверы организации (on-premise), работает с любой АБС через адаптеры.

- **План реализации:** [docs/plan.md](docs/plan.md)
- **Техническое задание:** [docs/tz/tz.md](docs/tz/tz.md) · [PDF](docs/tz/tz-2026-11-01.pdf) · [макеты](docs/tz/mockups-2026-11-01.pdf)
- **Архитектурные решения:** [docs/adr](docs/adr)
- **Задачи и канбан:** GitHub Issues и Projects ([как завести](#задачи-и-канбан))

## Стек

| Часть | Технологии |
|---|---|
| Бэкенд | .NET 10, ASP.NET Core, EF Core + Npgsql, Hangfire, SignalR |
| База | PostgreSQL 16 |
| Веб сотрудников | Vue 3, TypeScript, Vite, PrimeVue 4.5 (MIT), Pinia, vue-i18n (ky/ru) |
| Страница должника | Vue 3, отдельная лёгкая сборка |
| Телефония | Asterisk 20 (PJSIP + WebRTC, ARI), SIP-транк оператора |
| Поставка | Docker, docker compose, офлайн-установка |

## Структура

```
backend/        .NET solution: хосты Api, Worker, PublicGateway; модули; тесты
web/            веб-приложение для сотрудников (Э-01…Э-12)
debtor-page/    страница должника по ссылке (Э-13)
telephony/      конфигурация Asterisk и звуковые фразы ky/ru
deploy/         docker compose и поставка
docs/           план, ТЗ, макеты, ADR
scripts/        бэклог и скрипт заведения задач в GitHub
```

## Запуск для разработки

Бэкенд (.NET 10 SDK):

```bash
cd backend && dotnet build && dotnet test
```

```bash
dotnet run --project backend/src/Hosts/SoftCollection.Api
```

Веб (Node 22):

```bash
cd web && npm ci && npm run dev
```

Всё вместе в Docker:

```bash
cd deploy && cp .env.example .env && docker compose up --build
```

Веб — http://localhost:8000, API — http://localhost:8080, страница должника — http://localhost:8001.

## Задачи и канбан

Бэклог (вехи, метки, 88 задач с критериями готовности) лежит в [scripts/backlog.json](scripts/backlog.json). Чтобы завести его в GitHub Issues и на доску GitHub Projects, один раз выполните из корня репозитория:

```bash
gh auth login
```

```bash
gh auth refresh -s project
```

```bash
dotnet run scripts/BootstrapGithub.cs
```

Сначала можно посмотреть без изменений: `dotnet run scripts/BootstrapGithub.cs -- --dry-run`. Повторный запуск дублей не создаёт.

Задачи этапа 0 и S0 попадают в колонку **Todo**, сделанные — в **Done**, остальные ждут без статуса. На доске включите группировку по **Milestone**, чтобы видеть спринты.
