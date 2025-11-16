using Microsoft.EntityFrameworkCore;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;

namespace Rutana.API.Suscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Model builder extensions for Subscriptions bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the Subscriptions context configuration to the model builder.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        // Organization Aggregate Configuration
        builder.Entity<Organization>().HasKey(o => o.Id);
        builder.Entity<Organization>().Property(o => o.Id)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => new OrganizationId(value))
            .ValueGeneratedOnAdd();

        // Value Objects as Owned Types
        builder.Entity<Organization>().OwnsOne(o => o.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(name => name.Value)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.Entity<Organization>().OwnsOne(o => o.Ruc, r =>
        {
            r.WithOwner().HasForeignKey("Id");
            r.Property(ruc => ruc.Value)
                .HasColumnName("Ruc")
                .IsRequired()
                .HasMaxLength(20);
        });

        // Note: Indexes on Owned Type properties cannot be created here
        // They will be added via raw SQL migration after the initial schema is created
    }
}

