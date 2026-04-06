// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.Web.Components;
using Mealventory.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Scoped AppState for user session memory
builder.Services.AddScoped<AppState>();

// Configure shared API base URL for typed HttpClient services
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7253/";
var apiBaseUri = new Uri(apiBaseUrl);

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = apiBaseUri;
});

// Add typed HttpClient services for the API
builder.Services.AddHttpClient<FoodApiService>(client =>
{
    client.BaseAddress = apiBaseUri;
});

builder.Services.AddHttpClient<AuthApiService>(client =>
{
    client.BaseAddress = apiBaseUri;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
