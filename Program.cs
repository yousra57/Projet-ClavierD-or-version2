using Microsoft.EntityFrameworkCore;
using Projet_ClavierDor.Data;
using Projet_ClavierDor.Services;
using Projet_ClavierDor.Components;

var builder = WebApplication.CreateBuilder(args);

// Services Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// bdd SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=clavierDor.db"));

// Service de génération de PDF pour les résumés de partie
builder.Services.AddScoped<PdfService>();

// les services métier
builder.Services.AddScoped<JoueurService>();
builder.Services.AddScoped<JeuService>();
builder.Services.AddScoped<ScoreService>();

var app = builder.Build();



// Créer et migrer la base de données au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();