using ApiEmpresarial.Data;
using ApiEmpresarial.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Empresas e Funcionários",
        Version = "v1",
        Description = "Trabalho acadêmico — Arquitetura e Desenvolvimento de API"
    });
});

// DbContext + MySQL — ✅ versão fixa para evitar falhas de auto-detect
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.Parse("8.0.44") // ← versão do seu MySQL (confirmada por `mysql -V`)
    ));

// Serviços
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<FuncionarioService>();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz: /
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();