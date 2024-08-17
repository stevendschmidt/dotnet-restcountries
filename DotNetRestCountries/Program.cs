using DotNetRestCountries.Models;
using DotNetRestCountries.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EntitySet<Country>("countries");
modelBuilder.EntitySet<Region>("regions");
modelBuilder.EntitySet<Language>("languages");

builder.Services.AddControllers()
    .AddOData(options => options.Select().Filter().OrderBy().Expand()
        .Count().SetMaxTop(null).AddRouteComponents("api", modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

// Inject the data service
builder.Services.AddSingleton<IDataService, RestCountriesV31DataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
