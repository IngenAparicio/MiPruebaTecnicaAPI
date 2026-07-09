using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiPruebaTecnicaAccess.access;
using MiPruebaTecnicaAccess.context;
using MiPruebaTecnicaAccess.interfaces;
using MiPruebaTecnicaBL.services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 👇 Agregar política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        //policy.WithOrigins("http://localhost:4200") // URL de Angular
        //      .AllowAnyHeader()
        //      .AllowAnyMethod();
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// bd connection
builder.Services.AddDbContext<PruebaTecnicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

// swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Mi Prueba Técnica", Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


// interfaces
builder.Services.AddTransient<IRegistroAtencionServices, RegistroAtencionServices>();
builder.Services.AddTransient<IRegistroAtencionDataAccess, RegistroAtencionDataAccess>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
