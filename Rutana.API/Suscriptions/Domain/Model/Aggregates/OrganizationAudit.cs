using System;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Rutana.API.Suscriptions.Domain.Model.Aggregates;

/// <summary>
/// Partial class for Organization audit fields.
/// This integrates with EntityFrameworkCore.CreatedUpdatedDate to
/// automatically manage CreatedDate and UpdatedDate.
/// </summary>
public partial class Organization : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    [Column("created_at")]
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    [Column("updated_at")]
    public DateTimeOffset? UpdatedDate { get; set; }
}
