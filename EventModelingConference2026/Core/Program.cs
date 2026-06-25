// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Core.Identity;
using Cratis.Arc.MongoDB;
using Scalar.AspNetCore;
using AspNetCoreArcBuilderExtensions = Microsoft.AspNetCore.Builder.ArcBuilderExtensions;

// Force invariant culture for the Backend
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args)
    .AddCratisArc(
        options =>
        {
            options.GeneratedApis.RoutePrefix = "api";
            options.GeneratedApis.IncludeCommandNameInRoute = false;
            options.GeneratedApis.SegmentsToSkipForRoute = 1;
        },
        arcBuilder => AspNetCoreArcBuilderExtensions.WithChronicle(arcBuilder))
    .AddCratisChronicle(
        options => options.EventStore = "EventModelingConference2026",
        configure: chronicleBuilder => chronicleBuilder.WithCamelCaseNamingPolicy());
builder.UseCratisMongoDB(configureMongoDB: mongoBuilder => mongoBuilder.WithCamelCaseNamingPolicy());
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddOpenApi();
builder.Services.AddMicrosoftIdentityPlatformIdentityAuthentication();
builder.Services.AddIdentityProvider<IdentityDetailsProvider>();
builder.Services.Configure<ApiBehaviorOptions>(_ => _.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseWebSockets();
app.MapControllers();
app.UseCratisArc();
app.UseCratisChronicle();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapFallbackToFile("/index.html");

await app.RunAsync();
