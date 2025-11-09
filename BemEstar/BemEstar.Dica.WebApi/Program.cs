using BemEstar.Dica.Infra.Db;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFromtEndLocal",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Configuration
                .SetBasePath(AppContext.BaseDirectory) //Definir local padrão para acessar as confirgurações como o diretório onde está rodando a aplicação
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();

//Injeção de Dependência
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<BemEstar.Dica.Infra.Config.AppConfiguration>();
builder.Services.AddSingleton<NpgsqlConnectionFactory>();
builder.Services.AddScoped<BemEstar.Dica.Services.DicaService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFromtEndLocal");
app.UseAuthorization();    

app.MapControllers();      
app.Run();

