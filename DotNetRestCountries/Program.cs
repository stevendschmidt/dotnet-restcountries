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
modelBuilder.EnableLowerCamelCase();

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
app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
