using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;

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
        builder.Entity<Client>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        // CompanyName as Owned Type
        builder.Entity<Client>().OwnsOne(c => c.CompanyName, cn =>
        {
            cn.WithOwner().HasForeignKey("Id");
            cn.Property(name => name.Value)
                .HasColumnName("CompanyName")
                .IsRequired()
                .HasMaxLength(200);
        });

        // OrganizationId as Owned Type
        builder.Entity<Client>().OwnsOne(c => c.OrganizationId, o =>
        {
            o.WithOwner().HasForeignKey("Id");
            o.Property(org => org.Value)
                .HasColumnName("OrganizationId")
                .IsRequired();
        });

        // IsEnabled
        builder.Entity<Client>()
            .Property(c => c.IsEnabled)
            .IsRequired();

        // ===========================
        // Location Aggregate Configuration
        // ===========================
        builder.Entity<Location>().HasKey(l => l.Id);
        builder.Entity<Location>().Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();

        // LocationName as Owned Type
        builder.Entity<Location>().OwnsOne(l => l.Name, ln =>
        {
            ln.WithOwner().HasForeignKey("Id");
            ln.Property(name => name.Value)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);
        });

        // ClientId as Owned Type
        builder.Entity<Location>().OwnsOne(l => l.ClientId, ci =>
        {
            ci.WithOwner().HasForeignKey("Id");
            ci.Property(client => client.Value)
                .HasColumnName("ClientId")
                .IsRequired();
        });

        // Proximity as string enum
        builder.Entity<Location>()
            .Property(l => l.Proximity)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        // IsEnabled
        builder.Entity<Location>()
            .Property(l => l.IsEnabled)
            .IsRequired();

        // Note: Indexes will be added via migration if needed
        // Avoiding index creation on Owned Types to prevent EF Core issues
    }
}