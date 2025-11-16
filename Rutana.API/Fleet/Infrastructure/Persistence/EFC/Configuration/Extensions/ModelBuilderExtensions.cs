using Microsoft.EntityFrameworkCore;
using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;

namespace Rutana.API.Fleet.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Model builder extensions for Fleet bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the Fleet context configuration to the model builder.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyFleetConfiguration(this ModelBuilder builder)
    {
        // Vehicle Aggregate Configuration
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();

        // Value Objects as Owned Types
        builder.Entity<Vehicle>().OwnsOne(v => v.Plate, p =>
        {
            p.WithOwner().HasForeignKey("Id");
            p.Property(plate => plate.Value)
                .HasColumnName("Plate")
                .IsRequired()
                .HasMaxLength(20);
        });

        // OrganizationId as owned type with conversion
        builder.Entity<Vehicle>()
            .OwnsOne(v => v.OrganizationId, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.WithOwner().HasForeignKey("Id");
                ownedNavigationBuilder.Property(org => org.Value)
                    .HasColumnName("OrganizationId")
                    .IsRequired()
                    .HasConversion(
                        id => id.Value,
                        value => new OrganizationId(value));
            });

        // Foreign key relationship to Organization aggregate
        // Note: The relationship is configured using the shadow property created by OwnsOne
        builder.Entity<Vehicle>()
            .HasOne<Organization>()
            .WithMany()
            .HasForeignKey("OrganizationId")
            .HasPrincipalKey(o => o.Id.Value)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Vehicle>().OwnsOne(v => v.Capacity, c =>
        {
            c.WithOwner().HasForeignKey("Id");
            c.Property(cap => cap.Value)
                .HasColumnName("CapacityKg")
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });

        // Enum as string
        builder.Entity<Vehicle>()
            .Property(v => v.State)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        // Note: Indexes on Owned Type properties cannot be created here
        // They will be added via raw SQL migration after the initial schema is created
    }
}
