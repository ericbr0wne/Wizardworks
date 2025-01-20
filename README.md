# Wizardworks - Programmeringsuppgift

En webbapplikation som genererar ett dynamiskt rutnät av färgade kvadrater. Varje ny kvadrat får en slumpmässig färg (aldrig samma som föregående) och placeras automatiskt i ett kvadratiskt mönster.

## Teknisk Stack

### Frontend
- React (Vite)
- Tailwind CSS för styling
- Kommunicerar med backend via HTTP requests
- Visuell representation av kvadratmönstret
- Responsiv design

### Backend
- .NET 8 API
- C# för affärslogik
- Repository pattern för datahantering
- JSON-fillagring för persistent data
- CORS-konfiguration för frontend-kommunikation

## Huvudfunktioner

- Dynamisk generering av kvadrater
- Automatisk positionering i kvadratiskt mönster
- Slumpmässig färggenerering med kontroll för att undvika upprepning
- Persistent lagring via JSON
- State-hantering mellan omladdningar

## Projektstruktur

```
├── client/                 # Frontend React-applikation
│   ├── src/
│   │   ├── components/    # React-komponenter
│   │   ├── services/      # API-integrationslogik
│   │   └── ...
└── server/                # Backend .NET-applikation
    ├── Controllers/       # API-endpoints
    ├── Services/          # Affärslogik
    ├── Repositories/      # Dataåtkomst
    └── Models/            # Datamodeller
```

## Implementation Highlights

- Separation of Concerns mellan frontend och backend
- Clean Code principer
- Repository pattern för datahantering
- Asynkron kommunikation
- Felhantering på både frontend och backend
- Skalbar arkitektur

## Hur man startar projektet

1. Starta backend:
```bash
cd server/WizardWorks.Squares.API
dotnet run
```

2. Starta frontend:
```bash
cd client
npm install
npm run dev
```

## API Endpoints

- `GET /api/squares` - Hämtar alla sparade kvadrater
- `POST /api/squares` - Lägger till en ny kvadrat

## Utvecklingsverktyg

- Visual Studio Code
- Git för versionshantering