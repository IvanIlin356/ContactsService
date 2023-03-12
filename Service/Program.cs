using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddDbContext<ContactsContext>(opt => opt.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=postgresPasword"));
builder.Services.AddDbContext<ContactsContext>(opt => opt.UseInMemoryDatabase("ContactsList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
