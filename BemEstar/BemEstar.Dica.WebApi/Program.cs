var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEndLocal",
    policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
            .AllowAnyMethod()   // permite qualquer m�todo (GET, POST, PUT, DELETE...)
            .AllowAnyHeader();  // permite qualquer header
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontEndLocal");

app.UseAuthorization();    

app.MapControllers();      
app.Run();

