using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Services;

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
                .SetBasePath(AppContext.BaseDirectory) //Definir local padr�o para acessar as confirgura��es como o diret�rio onde est� rodando a aplica��o
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();

//Inje��o de Depend�ncia
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<BemEstar.Dica.Infra.Config.AppConfiguration>();
builder.Services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();
//builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();

builder.Services.AddScoped<BemEstar.Dica.Infra.Repositories.DicaRepository>();
builder.Services.AddScoped<BemEstar.Dica.Infra.Repositories.DicaProductRepository>();
builder.Services.AddScoped<BemEstar.Dica.Infra.Repositories.DicaUserRepository>();

builder.Services.AddScoped<DicaService>();
builder.Services.AddScoped<DicaProductService>();
builder.Services.AddScoped<DicaUserService>();

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

