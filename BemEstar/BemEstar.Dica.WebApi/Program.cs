using BemEstar.Dica.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra os servi�os da aplica��o no cont�iner de DI
// Scoped = nova inst�ncia por requisi��o HTTP
builder.Services.AddScoped<DicaService>();

builder.Services.AddControllers();

// Configura��o do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o de CORS para permitir o front-end local
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

// Em caso de autentica��o
app.UseAuthorization();

// Mapeamento dos controllers
app.MapControllers();

// Execu��o da aplica��o
app.Run();
