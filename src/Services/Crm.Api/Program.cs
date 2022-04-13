
using Microsoft.EntityFrameworkCore;

using RedPhase.Crm.Data;
using RedPhase.SharedDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration.Get<ConfigurationBase>();
var services = builder.Services;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomAuthentication(config);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomDbContext<CrmDbContext>(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers().RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
