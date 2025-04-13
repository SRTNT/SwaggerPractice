using Microsoft.AspNetCore.Mvc;
using SwaggerTest1;
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
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("Group1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Group 1",
        Version = "1",
        Description = "Group 1 API Descriptions",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "Group1@Test.com",
            Name = "Group 1 Contract",
            Url = new Uri("http://google.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Group 1 Licence",
            Url = new Uri("http://google.com")
        },
        TermsOfService = new Uri("http://google.com")
    });
    setup.SwaggerDoc("Group2", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Group 2",
        Version = "2",
        Description = "Group 2 API Descriptions",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "Group2@Test.com",
            Name = "Group 2 Contract",
            Url = new Uri("http://google.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Group 2 Licence",
            Url = new Uri("http://google.com")
        },
        TermsOfService = new Uri("http://google.com")
    });

    //- [Step 1: in setting of project->build->output->documentation file ✔]
    //- [Step 2: in setting of project->build->output->XML documentation file Path ✔(examp: projectName.xml)]
    //- [Code](https://github.com/SRTNT/SwaggerPractice/tree/main/src/SwaggerTest1)

    var xmlCommentsfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsfilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentsfile);

    setup.IncludeXmlComments(xmlCommentsfilePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/Group1/swagger.json", "Group 1 API UI");
        setup.SwaggerEndpoint("/swagger/Group2/swagger.json", "Group 2 API UI");

        // Remove swagger from url
        setup.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
