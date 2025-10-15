using BemEstar.Dica.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra os serviços da aplicação no contêiner de DI
// Scoped = nova instância por requisição HTTP
builder.Services.AddScoped<DicaService>();

builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de CORS para permitir o front-end local
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEndLocal",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL pegar do insomnia postman
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowFrontEndLocal");

// Em caso de autenticação
app.UseAuthorization();

// Mapeamento dos controllers
app.MapControllers();

// Execução da aplicação
app.Run();
