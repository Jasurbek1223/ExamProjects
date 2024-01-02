using LocalIdentity.SimpleInfra.Application.Common.Notifications.Models;

namespace LocalIdentity.SimpleInfra.Application.Common.Notifications.Events;

public class SendNotificationEvent : NotificationEvent
{
    public NotificationMassage Message { get; set; } = default!;
}