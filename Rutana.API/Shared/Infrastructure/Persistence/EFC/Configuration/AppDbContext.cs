using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

namespace Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Organization> Organizations => Set<Organization>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Subscriptions - Organization aggregate
        builder.Entity<Organization>(entity =>
        {
            // Primary key
            entity.HasKey(o => o.Id);

            // Id configuration (similar to Profile.Id)
            entity.Property(o => o.Id)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new OrganizationId(value))
                .ValueGeneratedOnAdd();

            // Name value object as owned type (let ModelBuilderExtensions convert to snake_case)
            entity.OwnsOne(o => o.Name, n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200);
            });

            // Ruc value object as owned type (let ModelBuilderExtensions convert to snake_case)
            entity.OwnsOne(o => o.Ruc, r =>
            {
                r.WithOwner().HasForeignKey("Id");
                r.Property(p => p.Value)
                    .HasColumnName("Ruc")
                    .HasMaxLength(20);
            });
        });

        builder.UseSnakeCaseNamingConvention();
    }
}