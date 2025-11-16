using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Rutana.API.Fleet.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Represents the application's database context using Entity Framework Core.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Configures the database context options.
    /// </summary>
    /// <param name="builder">The options' builder.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Fleet Context
        builder.ApplyFleetConfiguration();

        // Use snake case for database objects and pluralization for table names
        builder.UseSnakeCaseNamingConvention();
    }
}