using Rutana.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Rutana.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Rutana.API.Suscriptions.Domain.Model.Aggregates;
using Rutana.API.Suscriptions.Domain.Repositories;

namespace Rutana.API.Suscriptions.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for managing Payment aggregate persistence.
/// </summary>
/// <param name="context">The <see cref="AppDbContext"/> to use.</param>
public class PaymentRepository(AppDbContext context) : BaseRepository<Payment>(context), IPaymentRepository
{
    
}