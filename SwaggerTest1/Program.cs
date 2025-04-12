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

    //configure.Filters.Add(new ProducesResponseTypeAttribute(statusCode: StatusCodes.Status404NotFound,
    //                                                        type: typeof(void)));
    //configure.Filters.Add(new ProducesResponseTypeAttribute(statusCode: StatusCodes.Status500InternalServerError,
    //                                                        type: typeof(void)));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("SRT", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "SRT API",
        Version = "1",
        Description = "SRT API Descriptions",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "SRT@Test.com",
            Name = "SRT Contract",
            Url = new Uri("http://google.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "SRT Licence",
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
        setup.SwaggerEndpoint("/swagger/SRT/swagger.json", "SRT API UI");

        // Remove swagger from url
        setup.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
