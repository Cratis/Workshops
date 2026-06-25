// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aspire.Hosting.ApplicationModel;
using Cratis.Chronicle.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

// Select the database backend. Accepted values (case-insensitive):
//   mongodb (default), postgresql, mssql, sqlite
// Override at runtime with the run script, e.g.: ./run.sh postgresql
var databaseType = (builder.Configuration["DATABASE_TYPE"] ?? "mongodb").ToLowerInvariant();

// Resolve which Chronicle projection sink type id to pass to the service.
// Sink type ids are plain string identifiers (see WellKnownSinkTypes). They used to be GUIDs in
// older Chronicle versions, but are now the string names below.
// MongoDB sink:  "MongoDB" (WellKnownSinkTypes.MongoDB)
// SQL sink:      "SQL" (WellKnownSinkTypes.SQL)
const string mongoDbSinkTypeId = "MongoDB";
const string sqlSinkTypeId = "SQL";
var sinkTypeId = string.Equals(databaseType, "postgresql", StringComparison.Ordinal)
                 || string.Equals(databaseType, "mssql", StringComparison.Ordinal)
                 || string.Equals(databaseType, "sqlite", StringComparison.Ordinal)
    ? sqlSinkTypeId
    : mongoDbSinkTypeId;

// HashiCorp Vault running in dev mode with a fixed root token for local development
var vault = builder.AddContainer("vault", "hashicorp/vault")
    .WithEnvironment("VAULT_DEV_ROOT_TOKEN_ID", "root")
    .WithEnvironment("VAULT_DEV_LISTEN_ADDRESS", "0.0.0.0:8200")
    .WithArgs("server", "-dev")
    .WithHttpEndpoint(port: 8200, targetPort: 8200, name: "http");

// Chronicle — storage backend is selected dynamically based on DATABASE_TYPE.
IResourceBuilder<ChronicleResource> chronicle;
var vaultEndpoint = vault.GetEndpoint("http");

if (string.Equals(databaseType, "postgresql", StringComparison.Ordinal))
{
    var pgUser = builder.AddParameter("postgres-user", "chronicle");
    var pgPassword = builder.AddParameter("postgres-password", "chronicle", secret: true);
    var db = builder.AddPostgres("postgres", userName: pgUser, password: pgPassword, port: 5432)
        .AddDatabase("chronicle-db", databaseName: "chronicle");
    chronicle = builder.AddCratisChronicle("chronicle", c => c
        .WithPostgreSql(db)
        .WithHashiCorpVault(vaultEndpoint, "root"));
}
else if (string.Equals(databaseType, "mssql", StringComparison.Ordinal))
{
    var mssqlPassword = builder.AddParameter("mssql-password", "Chronicle_Str0ng!", secret: true);
    var db = builder.AddSqlServer("mssql", password: mssqlPassword, port: 1433)
        .AddDatabase("chronicle-db", databaseName: "chronicle");
    chronicle = builder.AddCratisChronicle("chronicle", c => c
        .WithMsSql(db)
        .WithHashiCorpVault(vaultEndpoint, "root"));
}
else if (string.Equals(databaseType, "sqlite", StringComparison.Ordinal))
{
    chronicle = builder.AddCratisChronicle("chronicle", c => c
        .WithSqlite("Data Source=/data/chronicle.db")
        .WithHashiCorpVault(vaultEndpoint, "root"));
}
else
{
    // mongodb — the latest-development image no longer bundles MongoDB, so spin up a dedicated container
    var mongo = builder.AddMongoDB("mongodb", port: 27017).AddDatabase("chronicle-db", databaseName: "chronicle");
    chronicle = builder.AddCratisChronicle("chronicle", c => c
        .WithMongoDB(mongo)
        .WithHashiCorpVault(vaultEndpoint, "root"));
}

// Pin to stable host ports. Management (workbench) runs on 8080, gRPC on 35000.
chronicle
    .WaitFor(vault)
    .WithEndpoint("management", endpoint => endpoint.Port = 8080)
    .WithEndpoint("grpc", endpoint => endpoint.Port = 35000);

// Core backend — connected to Chronicle; the projection sink type flows from DATABASE_TYPE.
// Pinned to port 5000 so the frontend's Vite dev-server proxy (which targets http://localhost:5000)
// reaches the backend.
var core = builder.AddProject<Projects.Core>("core")
    .WithReference(chronicle)
    .WithEnvironment("Cratis__Chronicle__DefaultSinkTypeId", sinkTypeId)
    .WithEndpoint("http", endpoint => endpoint.Port = 5000)
    .WaitFor(chronicle);

// Core frontend (Vite dev server via yarn, port 9000)
builder.AddViteApp("core-frontend", "../Core")
    .WithYarn()
    .WithHttpEndpoint(port: 9000, targetPort: 9000, name: "http", isProxied: false)
    .WaitFor(core);

await builder.Build().RunAsync();
