using SMART.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var conStr = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("SMARTDatabase")).ToString();
//conStrBuilder.Password = builder.Configuration["DbPassword"];


builder.Services.AddDbContextFactory<DomainDbContext>(
    options => options.UseSqlServer(conStr));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
