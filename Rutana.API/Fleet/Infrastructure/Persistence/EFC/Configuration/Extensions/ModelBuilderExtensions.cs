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
        var vehicle = builder.Entity<Vehicle>();

        vehicle.HasKey(v => v.Id);
        vehicle.Property(v => v.Id)
       .IsRequired()
       .ValueGeneratedOnAdd();

// Plate
var plate = vehicle.OwnsOne(v => v.Plate);
plate.WithOwner().HasForeignKey("Id");
plate.Property(p => p.Value)
     .HasColumnName("Plate")
     .IsRequired()
     .HasMaxLength(20);

// OrganizationId as property with conversion (not owned type, since it's a foreign key)
vehicle.Property(v => v.OrganizationId)
       .HasConversion(
           id => id.Value,
           value => new OrganizationId(value))
       .HasColumnName("OrganizationId")
       .IsRequired();

// Capacity
var capacity = vehicle.OwnsOne(v => v.Capacity);
capacity.WithOwner().HasForeignKey("Id");
capacity.Property(c => c.Value)
        .HasColumnName("CapacityKg")
        .IsRequired()
        .HasColumnType("decimal(18,2)");

// Enum
vehicle.Property(v => v.State)
       .HasConversion<string>()
       .IsRequired()
       .HasMaxLength(20);

// Relación con Organization
vehicle.HasOne<Organization>()
       .WithMany()
       .HasForeignKey(v => v.OrganizationId)
       .HasPrincipalKey(o => o.Id)
       .OnDelete(DeleteBehavior.Restrict);

    }
}
