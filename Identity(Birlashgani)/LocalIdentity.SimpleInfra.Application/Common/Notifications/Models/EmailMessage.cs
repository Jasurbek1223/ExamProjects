using System.Text.Json.Serialization;
using LocalIdentity.SimpleInfra.Domain.Entities;

namespace LocalIdentity.SimpleInfra.Application.Common.Notifications.Models;

public class EmailMessage : NotificationMassage
{
    public string ReceiverEmailAddress { get; set; } = default!;

    public string SenderEmailAddress { get; set; } = default!;

    public string Subject { get; set; } = default!;

    [JsonIgnore]
    public EmailTemplate EmailTemplate
    {
        get => (EmailTemplate)Template;
        set => Template = value;
    }
}