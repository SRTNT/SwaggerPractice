using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Collections.Immutable;
using System.Reflection;

// Add this for All App
// [assembly: ApiConventionType(typeof(DefaultApiConventions))]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(configure =>
{
    configure.ReturnHttpNotAcceptable = true; // 406

    configure.Filters.Add(new ProducesResponseTypeAttribute(statusCode: StatusCodes.Status404NotFound,
                                                            type: typeof(void)));
    configure.Filters.Add(new ProducesResponseTypeAttribute(statusCode: StatusCodes.Status500InternalServerError,
                                                            type: typeof(void)));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add services to the container
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});
// Register API Explorer for versioned APIs
//.AddApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'VVV"; // Format for versioning
//    options.SubstituteApiVersionInUrl = false; // Substitute version in URL
//});

// Add Swagger
builder.Services.AddSwaggerGen(options =>
{
    // Define Swagger documents for each version
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });

    // Use the versioned API explorer to create Swagger documents
    options.DocInclusionPredicate((version, apiDescription) =>
    {
        var CurrentVersion = double.Parse(version.ToLower().Replace("v", ""));

        var versionsNormal = apiDescription.ActionDescriptor
            .EndpointMetadata
            .OfType<ApiVersionAttribute>()
            .SelectMany(v => v.Versions)
            .Select(v => double.Parse(v.ToString()))
            .ToImmutableList();

        if (versionsNormal.Any(v => v == CurrentVersion))
            return true;

        var versionsMap = apiDescription.ActionDescriptor
            .EndpointMetadata
            .OfType<MapToApiVersionAttribute>()
            .SelectMany(v => v.Versions)
            .Select(v => double.Parse(v.ToString()))
            .ToImmutableList();

        if (versionsMap.Any(v => v == CurrentVersion))
            return true;

        return versionsMap.Count == 0 && versionsNormal.Count == 0;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");

        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
