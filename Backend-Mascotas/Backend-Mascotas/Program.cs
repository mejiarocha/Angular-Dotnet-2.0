using Backend_Mascotas.Models;
using Backend_Mascotas.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors

builder.Services.AddCors(options => options.AddPolicy("Allowebapp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add context

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

// AutoMapper

builder.Services.AddAutoMapper(typeof(Program));

// Add service para injeccion de dependencias

builder.Services.AddScoped<IMascotaRpository, MascotaRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("Allowebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
