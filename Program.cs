using CrudProfisaComDapper.Connections;
using CrudProfisaComDapper.Exception;
using CrudProfisaComDapper.Repositories;
using CrudProfisaComDapper.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();



// Adiciona acesso ao appsettings.json
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registra a fábrica de conexões
builder.Services.AddSingleton<PostgreSqlConnectionFactory>();

// Habilitar o mapeamento de nomes com underscores no Dapper
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); // Registra o middleware de exceção personalizada

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
