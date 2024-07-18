using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPIVersionControl.Context;
using WebAPIVersionControl.Controllers;

var builder = WebApplication.CreateBuilder(args);

#region DbContext Configuration

// DbContext Configuration
//builder.Services.AddDbContext<ApiVersionControlDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("ApiVersionControl")));
//options.MySqlConnection(builder.Configuration.GetConnectionString("ApiVersionControl")));

var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

// Pomelo 'DbContext' Configuration
builder.Services.AddDbContext<ApiVersionControlDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("ApiVersionControl"), serverVersion)
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
# endregion

# region Configure Controllers
builder.Services.AddScoped<CityController>();
builder.Services.AddScoped<CountryController>();
builder.Services.AddScoped<WeatherForecastController>();
# endregion

# region API Versioning

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new HeaderApiVersionReader("Api-Version");
});

# endregion

# region Add Versioned API Explorer

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

# endregion

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


# region Configure SwagerGen for API versioning
// Add SwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    // Resolve the Swagger generator options instance.
    var provider = builder.Services.BuildServiceProvider()
                                  .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = "API Documentation",
            Version = description.ApiVersion.ToString(),
            Description = "API Documentation Description",
            Contact = new OpenApiContact
            {
                Name = "Gunjan Patel",
                Email = "gpextreme0110@gmail.com",
                Url = new Uri("https://example.com"),
            }
        });
    }

    // Add support for specifying the Swagger JSON endpoint with the API version in the URL.
    c.DocInclusionPredicate((version, desc) =>
    {
        if (!desc.TryGetMethodInfo(out var methodInfo))
            return false;

        var versions = methodInfo.DeclaringType.GetCustomAttributes(true)
            .OfType<ApiVersionAttribute>()
            .SelectMany(attr => attr.Versions);

        var maps = methodInfo.GetCustomAttributes(true)
            .OfType<MapToApiVersionAttribute>()
            .SelectMany(attr => attr.Versions)
            .ToArray();

        return versions.Any(v => $"v{v}" == version) && (!maps.Any() || maps.Any(v => $"v{v}" == version));
    });

    // Include XML comments (optional) for describing APIs and models.
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

#endregion

var app = builder.Build();

# region Configure http Pipeline
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

# endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
