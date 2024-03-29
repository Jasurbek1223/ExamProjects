using LocalIdentity.SimpleInfra.Domain.Common.Entities;
using LocalIdentity.SimpleInfra.Domain.Enums;

namespace LocalIdentity.SimpleInfra.Domain.Entities;

public class UserSettings : IEntity
{
    public NotificationType? PreferredNotificationType { get; set; }
    
    public Guid Id { get; set; }
}