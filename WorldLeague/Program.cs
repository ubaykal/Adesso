using Microsoft.EntityFrameworkCore;
using WorldLeague.Business.Abstracts;
using WorldLeague.Business.Services;
using WorldLeague.DataAccess.Context;
using WorldLeague.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AdessoContext>(option =>
    option.UseNpgsql(
        "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=AdessoWorldLeague;Integrated Security=true;Pooling=true;"));

builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IDrawLot, DrawLot>();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AdessoContext>();
    db.Database.Migrate();
}

app.Run();