using Cortex.Mediator.Notifications;
using Rutana.API.Shared.Domain.Model.Events;

namespace Rutana.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}