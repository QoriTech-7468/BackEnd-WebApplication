using Rutana.API.IAM.Domain.Model.Aggregates;
using Rutana.API.IAM.Domain.Model.Commands;

namespace Rutana.API.IAM.Domain.Services;

/// <summary>
///     Contract for commands that change invitation state.
/// </summary>
public interface IInvitationCommandService
{
    /// <summary>
    ///     Handle a create invitation operation.
    /// </summary>
    /// <param name="command">The create invitation command.</param>
    /// <returns>The created <see cref="Invitation" /> instance.</returns>
    Task<Invitation> Handle(CreateInvitationCommand command);

    /// <summary>
    ///     Handle an accept invitation operation.
    /// </summary>
    /// <param name="command">The accept invitation command.</param>
    /// <returns>The updated <see cref="Invitation" /> instance.</returns>
    Task<Invitation> Handle(AcceptInvitationCommand command);
}

