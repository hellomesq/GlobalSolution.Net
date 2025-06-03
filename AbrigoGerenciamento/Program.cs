using Microsoft.EntityFrameworkCore;
using AbrigoGerenciamento.Data;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

// Aqui configuramos o MVC com Razor Views e suporte a TagHelpers
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Define a porta da aplicação conforme variável de ambiente PORT (ou 5000 se não definida)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Rota padrão do MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
