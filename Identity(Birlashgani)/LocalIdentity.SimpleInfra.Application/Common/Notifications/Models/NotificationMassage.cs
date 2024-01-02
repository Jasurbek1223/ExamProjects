using LocalIdentity.SimpleInfra.Domain.Entities;

namespace LocalIdentity.SimpleInfra.Application.Common.Notifications.Models;

public abstract class NotificationMassage
{
    public string Body { get; set; } = default!;

    public NotificationTemplate Template { get; set; } = default!;

    public Dictionary<string, string> Variables { get; set; } = default!;

    public bool IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }
}