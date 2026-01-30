using System.Security.Claims;
using System.Text;
using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Services;
using BemEstar.Dica.WebApi.DicaViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

//Definir o uso de JWT para autenticação
//Configurar o Swagger para entender o uso de JWT

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options=>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{Title = "AulasWebApi", Version= "v1"})
    var securitySchema new OpenApiSecurityScheme
    {
        Name: "Authorization",
        Description = "Digite '{Token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http, //Http insere o bearer automaticamente, o ApiKey não insere o bearer automaticamente.
        Reference = new OpenApiRefence
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    options.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {securitySchema, Array.Empty<string>()}
    };
    options.AddSecurityRequirement(securityRequirement);
    
}

);


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

builder.Services.AddScoped<DicaRepository>();
builder.Services.AddScoped<DicaUserRepository>();

builder.Services.AddScoped<DicaService>();
builder.Services.AddScoped<DicaUserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<>(JwtTokenService);

//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
//A linha de cima está fazendo o carregamento automático dessas linhas abaixo:
//JwtOptions option = new JwtOptions();
//option.Issuer = builder.Configuration["Jwt:Issuer"];
//option.Audience = builder.Configuration["Jwt: Audience"];
//option.Key = builder.Configuration["Jwt:key"];


//O maior ganho de performance é buscar a maior quantidade de dados apenas uma vez e tratar eles, menor quantidade de I/O.
var jwtSection = builder.Configuration.GetSection("Jwt");
var issuer = jwtSection["Issuer"]; 
var audience = jwtSection["Audience"];
var key = jwtSection["Key"];

builder.Services.Configure<JwtOptions>(jwtSection);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = issuer,
           ValidAudience = audience,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
           RoleClaimType = ClaimTypes.Role
       };
    });

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFromtEndLocal");
app.UseAuthentication();
app.UseAuthorization();    

app.MapControllers();      
app.Run();

