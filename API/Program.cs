using Microsoft.OpenApi.Models;
using Serilog;
using SftpXmlTask.DataAccess;
using SftpXmlTask.DataAccess.TaskImplementation;
using SftpXmlTask.Service;
using SftpXmlTask.SftpService;
using System.Reflection;
using System.Text.Json.Serialization;

try
{

    var builder = WebApplication.CreateBuilder(args);

    var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    Log.Logger = logger;

    var assembly = Assembly.GetExecutingAssembly().GetName();
    Log.Information("Starting {name} application version {version}", assembly.Name, assembly.Version);


    builder.Services
        .AddControllers()
        .AddJsonOptions
        (
            options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }
        );

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen
    (
        options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.UseAllOfToExtendReferenceSchemas();
        }
    );

    var sftpConfiguration = builder.Configuration.GetSection("SftpConfiguration").Get<SftpConfiguration>()!;
    builder.Services.AddScoped<ISftpService>
    (
       service => new SftpService(sftpConfiguration)
    );


    builder.Services.AddScoped<IOrderProcessingDataAccess>
    (
       service => new XmlOrderProcessingDataAccess(builder.Configuration)
    );

    builder.Services.AddScoped<IOrderProcessingService>
    (
       service => new XmlOrderProcessingService(service.GetRequiredService<IOrderProcessingDataAccess>())
    );

    builder.Services.AddSwaggerGen
    (
        options =>
        {
            options.SwaggerDoc
            (
                "v1",
                new OpenApiInfo
                {
                    Title = "Toni test order processing application",
                    Version = "v1",
                    Description = $"{assembly.Name} v{assembly.Version}"
                }
            );

            options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });

            options.SwaggerGeneratorOptions.SortKeySelector = (apiDesc) => apiDesc.GroupName;

            options.CustomSchemaIds(x => x.FullName);

            options.DocInclusionPredicate((docName, description) =>
            {
                return (description.RelativePath != String.Empty);
            });
        }
    );

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();

    //if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shutting down.");
    Log.CloseAndFlush();
}