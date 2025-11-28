
using Rutana.API.Fleet.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Rutana.API.CRM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

using Rutana.API.Planning.Infrastructure.Interfaces.ASP.Configuration.Extensions;

using Rutana.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using Rutana.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Rutana.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Rutana.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;

 
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Rutana.API.Suscriptions.Infrastructure.Interfaces.ASP.Configuration.Extensions;

using Rutana.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Rutana.API.IAM.Infrastructure.Pipeline.Middleware.Extensions; 



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Services
builder.AddDatabaseServices();


// Open API Configuration
builder.AddOpenApiDocumentationServices();

// Dependency Injection - Bounded Context Services Registration
builder.AddSharedContextServices();
builder.AddFleetContextServices();
builder.AddSubscriptionsContextServices();
builder.AddCRMContextServices();


//  REGISTRAMOS EL NUEVO IAM CONTEXT
builder.AddIamContextServices();
builder.AddPlanningContextServices();


// Mediator Configuration
builder.AddCortexConfigurationServices();

// CORS Configuration - Allow all origins (temporary)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// Verify if the database exists and create it if it doesn't
app.UseDatabaseCreationAssurance();

// Configure the HTTP request pipeline.
app.UseOpenApiDocumentation();
app.UseHttpsRedirection();

// Enable CORS
app.UseCors();

app.UseRequestAuthorization();

app.UseAuthorization();
app.MapControllers();
app.Run();