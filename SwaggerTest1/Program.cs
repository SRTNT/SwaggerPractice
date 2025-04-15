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
builder.Services.AddSwaggerGen(option =>
{
    // Define Swagger documents for each version
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    option.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });

    // Use the versioned API explorer to create Swagger documents
    #region Filter View Of versions
    option.DocInclusionPredicate((version, apiDescription) =>
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
    #endregion

    // تنظیمات احراز هویت با توکن
    #region Authenticatio of swagger UI for request
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = $@"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    }); 
    option.AddSecurityDefinition("srt", new OpenApiSecurityScheme
    {
        Description = $@"srt Authorization header using the Bearer scheme. Enter 'srt' [space] and then your token in the text input below.",
        Name = "AuthorizationSRT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "srt"
    });
    #endregion
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
    app.UseSwaggerUI(option =>
    {
        option.DocumentTitle = "Swagger Title";

        option.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        option.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");

        option.RoutePrefix = string.Empty;

        // Close All Documentation Accourdeon Panel
        option.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
