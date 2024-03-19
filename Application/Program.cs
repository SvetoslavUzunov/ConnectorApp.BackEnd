using Application.Configurations;
using static System.Net.Mime.MediaTypeNames;
using static Infrastructure.Services.ServiceInjection;
using static Infrastructure.SeedDataManager;
using Application.GraphQLTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration);

builder.Services.AddMemoryCache();

builder.Services.AddServices();

builder.Services.AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>();

builder.Services.AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddConfigurations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages(Text.Plain, "Status Code Page: {0}");

await app.SeedDataAsync();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
