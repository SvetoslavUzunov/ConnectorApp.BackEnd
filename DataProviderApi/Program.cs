using DataProviderApi.Constants;
using DataProviderApi.Models;
using DataProviderApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BooksManagementDatabaseSettings>
    (builder.Configuration.GetSection(nameof(BooksManagementDatabaseSettings)));

builder.Services.AddSingleton<IBooksManagementDatabaseSettings>
    (settings => settings.GetRequiredService<IOptions<BooksManagementDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(x => new MongoClient(builder.Configuration.GetValue<string>("BooksManagementDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy(CorsConstants.CorsPolicy, build =>
          build.WithOrigins(CorsConstants.TargetLocalhost)
          .AllowAnyMethod()
          .AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(CorsConstants.CorsPolicy);

app.Run();
