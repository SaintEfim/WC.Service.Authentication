namespace WC.Service.Authentication.AuthorizationAdmin;

public class AdminSettingsOptions
{
    public string[] PositionNames { get; set; } = [];

    public Guid AdminPositionId { get; set; }

    public string? AdminEmailLocalPart { get; set; }

    public string? AdminEmailDomain { get; set; }

    public string? AdminRegistrationPassword { get; set; }
}
