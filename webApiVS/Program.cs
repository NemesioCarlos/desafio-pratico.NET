using Microsoft.EntityFrameworkCore;
using webApiVS.DataContest;
using webApiVS.Service.ContatoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//falando que essa injeção vai usar os metodos de ContatoService
builder.Services.AddScoped<IContatoInterface, ContatoService>();

//falando para o options que vai usar o UseSqlServer
builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    // o cufiguration acessa o que tem no appsettings.json 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
