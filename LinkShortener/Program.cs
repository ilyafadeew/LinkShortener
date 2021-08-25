
using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Configuration;
using LinkShortener.DAL.Repository;
using LinkShortener.BLL.Services;
using LinkShortener.BLL;

var builder = WebApplication.CreateBuilder(args);

var Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

// Add services to the container.

#region InfrustructionServices
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<DefaultMappingProfile>());
builder.Services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoGenericRepository<>));
#endregion

#region BuisnesLogicServices
builder.Services.AddScoped(typeof(LinkShortenerService));
#endregion

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LinkShortener", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkShortener v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

