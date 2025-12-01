using Microsoft.EntityFrameworkCore;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Aggregates; 
using Rutana.API.Suscriptions.Domain.Model.ValueObjects;

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
        
        // Payment Aggregate Configuration 
        
        builder.Entity<Payment>().ToTable("Payments");
        
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Payment>().Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(10,2)"); // 10 total digits, 2 decimals

        builder.Entity<Payment>().Property(p => p.Currency)
            .IsRequired()
            .HasMaxLength(3); // Example: "PEN"

        builder.Entity<Payment>().Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(20); // Example: "Completed"

        builder.Entity<Payment>()
            .HasOne<User>()            
            .WithMany()                 
            .HasForeignKey(p => p.UserId) 
            .HasConstraintName("FK_Payments_Users_UserId") 
            .OnDelete(DeleteBehavior.Cascade); 
        
        builder.Entity<Payment>().Property(p => p.SubscriptionId)
            .IsRequired()
            .HasConversion(
                id => (int)id.Value,                
                value => new SubscriptionId(value)  
            );
        
        builder.Entity<Payment>().Property(p => p.SubscriptionId).IsRequired();
        builder.Entity<Payment>()
            .HasOne<Subscription>()       
            .WithMany()               
            .HasForeignKey(p => p.SubscriptionId)
            .HasConstraintName("FK_Payments_Subscriptions_SubscriptionId")
            .OnDelete(DeleteBehavior.Cascade); 
        
        // Subscription Aggregate Configuration 
        
        builder.Entity<Subscription>().ToTable("Subscriptions");
        
        builder.Entity<Subscription>().HasKey(s => s.Id);

        // SubscriptionId <-> int/long
        builder.Entity<Subscription>().Property(s => s.Id)
            .IsRequired()
            .HasConversion(
                id => (int)id.Value,             
                value => new SubscriptionId(value) 
            )
            .ValueGeneratedOnAdd();
        
        builder.Entity<Subscription>().Property(s => s.PlanType)
            .IsRequired()
            .HasConversion(
                plan => plan.Value,            
                value => PlanType.From(value)  
            )
            .HasMaxLength(50);

        // Price
        builder.Entity<Subscription>().Property(s => s.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)"); 
    }
}

