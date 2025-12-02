using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;

namespace Rutana.API.CRM.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Model builder extensions for CRM bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the CRM context configuration to the model builder.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyCRMConfiguration(this ModelBuilder builder)
    {
        // ===========================
        // Client Aggregate Configuration
        // ===========================
        builder.Entity<Client>().HasKey(c => c.Id);
        builder.Entity<Client>().Property(c => c.Id)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new ClientId(value))
            .ValueGeneratedOnAdd();

        // CompanyName as Owned Type
        builder.Entity<Client>().OwnsOne(c => c.CompanyName, cn =>
        {
            cn.WithOwner().HasForeignKey("Id");
            cn.Property(name => name.Value)
                .HasColumnName("CompanyName")
                .IsRequired()
                .HasMaxLength(200);
        });

        // OrganizationId as property with conversion (not owned type, since it's a foreign key)
        builder.Entity<Client>().Property(c => c.OrganizationId)
            .HasConversion(
                id => id.Value,
                value => new OrganizationId(value))
            .HasColumnName("OrganizationId")
            .IsRequired();

        // Relación con Organization
        builder.Entity<Client>().HasOne<Organization>()
            .WithMany()
            .HasForeignKey(c => c.OrganizationId)
            .HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // IsEnabled
        builder.Entity<Client>()
            .Property(c => c.IsEnabled)
            .IsRequired();

        // ===========================
        // Location Aggregate Configuration
        // ===========================
        builder.Entity<Location>().HasKey(l => l.Id);
        builder.Entity<Location>().Property(l => l.Id)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new LocationId(value))
            .ValueGeneratedOnAdd();

        // Address as Owned Type
        builder.Entity<Location>().OwnsOne(l => l.Address, a =>
        {
            a.WithOwner().HasForeignKey("Id");
            a.Property(addr => addr.Value)
                .HasColumnName("Address")
                .IsRequired()
                .HasMaxLength(500);
        });

        // Latitude as Owned Type
        builder.Entity<Location>().OwnsOne(l => l.Latitude, lat =>
        {
            lat.WithOwner().HasForeignKey("Id");
            lat.Property(l => l.Value)
                .HasColumnName("Latitude")
                .IsRequired()
                .HasMaxLength(50);
        });

        // Longitude as Owned Type
        builder.Entity<Location>().OwnsOne(l => l.Longitude, lon =>
        {
            lon.WithOwner().HasForeignKey("Id");
            lon.Property(l => l.Value)
                .HasColumnName("Longitude")
                .IsRequired()
                .HasMaxLength(50);
        });

        // ClientId as property with conversion

        builder.Entity<Location>()   
            .Property(l => l.ClientId)
            .HasConversion(
                id => id.Value,
                value => new ClientId(value))
            .HasColumnName("ClientId")
            .IsRequired();

        // Proximity as string enum
        builder.Entity<Location>()
            .Property(l => l.Proximity)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);
        
        // IsEnabled
        // IsEnabled property
        builder.Entity<Location>()
            .Property(l => l.IsEnabled)
            .IsRequired();

        // Relationship with Client
        builder.Entity<Location>()
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(l => l.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Note: Indexes will be added via migration if needed
        // Avoiding index creation on Owned Types to prevent EF Core issues
    }
}