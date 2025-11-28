using Microsoft.EntityFrameworkCore;
using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Enums;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Shared.Domain.Model.ValueObjects;

namespace Rutana.API.IAM.Infrastructure.Persistance.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // Configuración de la tabla Users
        builder.Entity<User>().ToTable("Users");

        // Primary Key
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.Surname).IsRequired().HasMaxLength(50);
        
        // El Email es obligatorio y debe ser ÚNICO en la base de datos
        builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique(); 

        builder.Entity<User>().Property(u => u.Phone).HasMaxLength(20);
        
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        // Configure Role as enum stored as int in database (efficient, type-safe)
        // JSON serialization will convert to string automatically via JsonStringEnumConverter
        builder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<int>()
            .IsRequired();
        
        // Configure OrganizationId as nullable
        builder.Entity<User>()
            .Property(u => u.OrganizationId)
            .HasConversion(
                id => id != null ? id.Value : (int?)null,
                value => value.HasValue ? new OrganizationId(value.Value) : null)
            .HasColumnName("OrganizationId")
            .IsRequired(false);

        builder.Entity<User>()
            .HasOne<Organization>()
            .WithMany()
            .HasForeignKey(u => u.OrganizationId)
            .HasPrincipalKey(o => o.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuración de la tabla Invitations
        builder.Entity<Invitation>().ToTable("Invitations");

        // Primary Key
        builder.Entity<Invitation>().HasKey(i => i.Id);
        builder.Entity<Invitation>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();

        // Configure OrganizationId
        builder.Entity<Invitation>()
            .Property(i => i.OrganizationId)
            .HasConversion(
                id => id.Value,
                value => new OrganizationId(value))
            .HasColumnName("OrganizationId")
            .IsRequired();

        // Configure UserId
        builder.Entity<Invitation>()
            .Property(i => i.UserId)
            .IsRequired();

        // Configure Role as enum stored as int
        builder.Entity<Invitation>()
            .Property(i => i.Role)
            .HasConversion<int>()
            .IsRequired();

        // Configure Status as enum stored as int
        builder.Entity<Invitation>()
            .Property(i => i.Status)
            .HasConversion<int>()
            .IsRequired();

        // Configure CreatedAt
        builder.Entity<Invitation>()
            .Property(i => i.CreatedAt)
            .IsRequired();

        // Relationships
        builder.Entity<Invitation>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Invitation>()
            .HasOne<Organization>()
            .WithMany()
            .HasForeignKey(i => i.OrganizationId)
            .HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Index to prevent duplicate pending invitations
        builder.Entity<Invitation>()
            .HasIndex(i => new { i.UserId, i.OrganizationId })
            .IsUnique();
    }
}