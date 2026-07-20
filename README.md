# InventoryManagement.Api

A REST API for tracking product inventory, suppliers, categories, and stock movements — built with ASP.NET Core Minimal APIs using a vertical-slice architecture.

> This README covers the API (`src/InventoryManagement.Api`). The repo also contains `src/InventoryManagement.Client`, a SvelteKit frontend that consumes this API — see the note under [Getting Started](#getting-started) for running it alongside the API.

## Tech Stack

- **.NET 10** — ASP.NET Core Minimal APIs
- **Entity Framework Core** + **Npgsql** — PostgreSQL data access
- **JWT Bearer Authentication** — via `Microsoft.AspNetCore.Authentication.JwtBearer`
- **BCrypt.Net-Next** — password hashing
- **FluentValidation** — request validation
- **Scalar** — interactive API reference (OpenAPI)

## Architecture

The project uses a **vertical-slice** structure: each feature owns its own endpoints, requests, responses, and validators, rather than being split across horizontal layers (controllers/services/repositories).

```
InventoryManagement.Api/
├── Data/
│   ├── AppDbContext.cs
│   ├── Configuration/       # EF Core / DI setup (AddInfrastructure)
│   ├── Entity/               # Category, Product, Supplier, InventoryTransaction, User
│   ├── Enum/
│   └── Migrations/
├── Extensions/
│   └── BearerSecuritySchemeTransformer.cs   # Adds JWT auth to OpenAPI/Scalar docs
├── Features/
│   ├── Authentication/       # /auth/register, /auth/login
│   ├── Users/                 # /users
│   ├── Categories/            # /categories
│   ├── Products/              # /products
│   ├── Suppliers/             # /suppliers
│   ├── Transactions/          # /products/{productId}/transactions
│   └── Dashboard/             # /dashboard
└── Program.cs
```

Each endpoint file (e.g. `Features/Products/Endpoints/CreateProduct/CreateProduct.cs`) maps a single route and contains its request/response models and validation in the same folder.

## Domain Model

- **Product** — name, SKU, price, stock, minimum stock threshold, optional image URL, belongs to a `Category` and optionally a `Supplier`
- **Category** — groups products
- **Supplier** — name, contact info, linked to products
- **InventoryTransaction** — records stock movements (`StockIn`, `StockOut`, `Adjustment`) with previous/new stock snapshots, linked to a product
- **User** — username + hashed password, for authentication

## API Endpoints

All routes below except `/auth/*` require a valid JWT (`.RequireAuthorization()`).

| Method | Route                                     | Description                                                                                                                                                          |
| ------ | ----------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| POST   | `/auth/register`                          | Register a new user                                                                                                                                                  |
| POST   | `/auth/login`                             | Authenticate and receive a JWT                                                                                                                                       |
| GET    | `/users/{id}`                             | Get a user by ID                                                                                                                                                     |
| GET    | `/dashboard`                              | Summary stats: total products, low/out-of-stock alerts, total inventory value, category & supplier counts                                                            |
| GET    | `/categories`                             | List categories                                                                                                                                                      |
| GET    | `/categories/{id}`                        | Get a category                                                                                                                                                       |
| POST   | `/categories`                             | Create a category                                                                                                                                                    |
| PUT    | `/categories/{id}`                        | Update a category                                                                                                                                                    |
| DELETE | `/categories/{id}`                        | Delete a category — returns `409 Conflict` if any products still reference it                                                                                        |
| GET    | `/products`                               | List products — supports pagination (`pageNumber`, `pageSize`) and filtering by `name`, `categoryId`, `supplierId`, `lowStock`, `outOfStock`, `minPrice`, `maxPrice` |
| GET    | `/products/{id}`                          | Get a product                                                                                                                                                        |
| POST   | `/products`                               | Create a product                                                                                                                                                     |
| PUT    | `/products/{id}`                          | Update a product                                                                                                                                                     |
| DELETE | `/products/{id}`                          | Delete a product                                                                                                                                                     |
| GET    | `/suppliers`                              | List suppliers                                                                                                                                                       |
| GET    | `/suppliers/{id}`                         | Get a supplier                                                                                                                                                       |
| POST   | `/suppliers`                              | Create a supplier                                                                                                                                                    |
| PUT    | `/suppliers/{id}`                         | Update a supplier                                                                                                                                                    |
| DELETE | `/suppliers/{id}`                         | Delete a supplier — returns `409 Conflict` if any products still reference it                                                                                        |
| GET    | `/products/{productId}/transactions`      | List transactions — supports pagination (`pageNumber`, `pageSize`) and filtering by `type`                                                                           |
| GET    | `/products/{productId}/transactions/{id}` | Get a stock transaction                                                                                                                                              |
| POST   | `/products/{productId}/transactions`      | Record a stock movement (in/out/adjustment) and update product stock                                                                                                 |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL running locally (or via Docker)

### Setup

```bash
git clone https://github.com/pregoadisaputro/InventoryManagement.git
cd InventoryManagement/src/InventoryManagement.Api
```

1. Update the connection string in `appsettings.json` if your Postgres credentials differ from the default:

```json
"ConnectionStrings": {
  "Default": "Host=localhost;Port=5432;Database=InventoryDB;Username=postgres"
}
```

2. Apply EF Core migrations:

```bash
dotnet ef database update
```

3. Run the API:

```bash
dotnet run
```

By default the API listens on `http://localhost:5005` (see `Properties/launchSettings.json`). In development mode, an interactive API reference is available at `/scalar`.

### Running the client alongside the API

The API's CORS policy currently only allows requests from `http://localhost:5173`, which is the default Vite dev server port for `InventoryManagement.Client`. If you're running the SvelteKit client locally:

```bash
cd src/InventoryManagement.Client
npm install
npm run dev
```

If you deploy the API or run the client on a different origin, update the CORS policy in `Program.cs` accordingly.

## Authentication

Register a user via `POST /auth/register`, then log in via `POST /auth/login` to receive a JWT. Include it on subsequent requests:

```
Authorization: Bearer <token>
```

Tokens are signed using the key/issuer/audience configured under `Jwt` in `appsettings.json` and expire based on `Jwt:ExpirationMinutes`.

> **Note:** the JWT signing key committed in `appsettings.json` is a development placeholder — replace it with a secret (e.g. via user secrets or environment variables) before deploying anywhere real.

## Stock Transactions

Stock changes are never made by directly editing a product's `Stock` field — they go through `POST /products/{productId}/transactions`, which:

- Validates the request and quantity
- Calculates the new stock based on transaction type (`StockIn`, `StockOut`, `Adjustment`)
- Rejects `StockOut` requests that would drop stock below zero
- Writes an `InventoryTransaction` record alongside the updated product, in the same `SaveChangesAsync` call

### Concurrency handling

`Product` uses optimistic concurrency via a `Version` property (`uint`, `[Timestamp]`) mapped to PostgreSQL's built-in `xmin` system column, which Postgres automatically updates on every row change. If two requests read the same product and both try to save a stock update, the second one's `UPDATE` will affect zero rows (since the `xmin` it read is now stale), EF Core throws `DbUpdateConcurrencyException`, and the endpoint returns `409 Conflict` instead of silently overwriting the first request's change.
