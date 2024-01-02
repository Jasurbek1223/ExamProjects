namespace LocalIdentity.SimpleInfra.Infrastructure.Common.Settings;

public class PasswordValidationSettings
{
    public bool RequireDigit { get; set; }

    public bool RequireUpperCase { get; set; }

    public bool RequireLowerCase { get; set; }

    public bool RequireNonAlphanumeric { get; set; }

    public int MinimumLength { get; set; }

    public int MaximumLength { get; set; }
}