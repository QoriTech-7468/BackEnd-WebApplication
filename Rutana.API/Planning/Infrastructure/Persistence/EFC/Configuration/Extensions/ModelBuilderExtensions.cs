using Microsoft.EntityFrameworkCore;
using Rutana.API.CRM.Domain.Model.Aggregates;
using Rutana.API.CRM.Domain.Model.ValueObjects;
using Rutana.API.Fleet.Domain.Model.Aggregates;
using Rutana.API.Fleet.Domain.Model.ValueObjects;
using Rutana.API.Planning.Domain.Model.Entities;
using Rutana.API.Planning.Domain.Model.ValueObjects;
using Rutana.API.Shared.Domain.Model.ValueObjects;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using RouteDraftAggregate = Rutana.API.Planning.Domain.Model.Aggregates.RouteDraft;
using RouteAggregate = Rutana.API.Planning.Domain.Model.Aggregates.Route;

namespace Rutana.API.Planning.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Model builder extensions for Planning bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the Planning context configuration to the model builder.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyPlanningConfiguration(this ModelBuilder builder)
    {
        ConfigureRouteDraft(builder);
        ConfigureRoute(builder);
        ConfigureDelivery(builder);
        ConfigureRouteTeamMember(builder);
    }

    private static void ConfigureRouteDraft(ModelBuilder builder)
    {
        var routeDraft = builder.Entity<RouteDraftAggregate>();

        routeDraft.HasKey(rd => rd.Id);
        routeDraft.Property(rd => rd.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // OrganizationId as property with conversion
        routeDraft.Property(rd => rd.OrganizationId)
            .HasConversion(
                id => id.Value,
                value => new OrganizationId(value))
            .HasColumnName("OrganizationId")
            .IsRequired();

        // ColorCode as owned type
        var colorCode = routeDraft.OwnsOne(rd => rd.ColorCode);
        colorCode.WithOwner().HasForeignKey("Id");
        colorCode.Property(cc => cc.Value)
            .HasColumnName("ColorCode")
            .IsRequired()
            .HasMaxLength(7);

        // VehicleId as nullable property with conversion
        routeDraft.Property(rd => rd.VehicleId)
            .HasConversion(
                id => id != null ? id.Value : (int?)null,
                value => value.HasValue ? new VehicleId(value.Value) : null)
            .HasColumnName("VehicleId")
            .IsRequired(false);

        // DateTime properties
        routeDraft.Property(rd => rd.StartedAt)
            .HasColumnName("StartedAt")
            .IsRequired(false);

        routeDraft.Property(rd => rd.EndedAt)
            .HasColumnName("EndedAt")
            .IsRequired(false);

        // Relationships
        routeDraft.HasOne<Organization>()
            .WithMany()
            .HasForeignKey(rd => rd.OrganizationId)
            .HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.Restrict);

        routeDraft.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(rd => rd.VehicleId)
            .HasPrincipalKey(v => v.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        // Collection navigation - Deliveries
        routeDraft.HasMany(rd => rd.Deliveries)
            .WithOne()
            .HasForeignKey("RouteDraftId")
            .OnDelete(DeleteBehavior.Cascade);

        // Collection navigation - TeamMembers
        routeDraft.HasMany(rd => rd.TeamMembers)
            .WithOne()
            .HasForeignKey("RouteDraftId")
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureRoute(ModelBuilder builder)
    {
        var route = builder.Entity<RouteAggregate>();

        route.HasKey(r => r.Id);
        route.Property(r => r.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // OrganizationId as property with conversion
        route.Property(r => r.OrganizationId)
            .HasConversion(
                id => id.Value,
                value => new OrganizationId(value))
            .HasColumnName("OrganizationId")
            .IsRequired();

        // ColorCode as owned type
        var colorCode = route.OwnsOne(r => r.ColorCode);
        colorCode.WithOwner().HasForeignKey("Id");
        colorCode.Property(cc => cc.Value)
            .HasColumnName("ColorCode")
            .IsRequired()
            .HasMaxLength(7);

        // VehicleId as property with conversion
        route.Property(r => r.VehicleId)
            .HasConversion(
                id => id.Value,
                value => new VehicleId(value))
            .HasColumnName("VehicleId")
            .IsRequired();

        // DateTime properties
        route.Property(r => r.StartedAt)
            .HasColumnName("StartedAt")
            .IsRequired();

        route.Property(r => r.EndedAt)
            .HasColumnName("EndedAt")
            .IsRequired(false);

        // Status enum as string
        route.Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        // Relationships
        route.HasOne<Organization>()
            .WithMany()
            .HasForeignKey(r => r.OrganizationId)
            .HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.Restrict);

        route.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(r => r.VehicleId)
            .HasPrincipalKey(v => v.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Collection navigation - Deliveries
        route.HasMany(r => r.Deliveries)
            .WithOne()
            .HasForeignKey("RouteId")
            .OnDelete(DeleteBehavior.Cascade);

        // Collection navigation - TeamMembers
        route.HasMany(r => r.TeamMembers)
            .WithOne()
            .HasForeignKey("RouteId")
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureDelivery(ModelBuilder builder)
    {
        var delivery = builder.Entity<Delivery>();

        delivery.HasKey(d => d.Id);

        // DeliveryId as owned type
        var deliveryId = delivery.OwnsOne(d => d.Id);
        deliveryId.WithOwner().HasForeignKey("Id");
        deliveryId.Property(di => di.Value)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        // LocationId as property with conversion
        delivery.Property(d => d.LocationId)
            .HasConversion(
                id => id.Value,
                value => new LocationId(value))
            .HasColumnName("LocationId")
            .IsRequired();

        // Status enum as string
        delivery.Property(d => d.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        // RejectReason enum as nullable string
        delivery.Property(d => d.RejectReason)
            .HasConversion(
                reason => reason.HasValue ? reason.Value.ToString() : null,
                value => value != null ? Enum.Parse<RejectReason>(value) : (RejectReason?)null)
            .HasColumnName("RejectReason")
            .IsRequired(false)
            .HasMaxLength(50);

        // RejectDetails as string
        delivery.Property(d => d.RejectDetails)
            .HasColumnName("RejectDetails")
            .IsRequired(false)
            .HasMaxLength(500);

        // Relationship with Location
        delivery.HasOne<Location>()
            .WithMany()
            .HasForeignKey(d => d.LocationId)
            .HasPrincipalKey(l => l.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureRouteTeamMember(ModelBuilder builder)
    {
        var teamMember = builder.Entity<RouteTeamMember>();

        teamMember.HasKey(tm => tm.Id);

        // RouteTeamMemberId as owned type
        var teamMemberId = teamMember.OwnsOne(tm => tm.Id);
        teamMemberId.WithOwner().HasForeignKey("Id");
        teamMemberId.Property(tmi => tmi.Value)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        // UserId as property with conversion
        // TODO: When IAM bounded context is implemented, add relationship with User aggregate
        teamMember.Property(tm => tm.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .HasColumnName("UserId")
            .IsRequired();
    }
}