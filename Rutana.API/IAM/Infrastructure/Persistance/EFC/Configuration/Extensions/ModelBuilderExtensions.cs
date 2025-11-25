using Microsoft.EntityFrameworkCore;
using Rutana.API.IAM.Domain.Model.Aggregates;

namespace Rutana.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

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
    }
}