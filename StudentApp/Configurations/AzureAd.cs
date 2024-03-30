
namespace StudentApp.Configurations;

public class AzureAd
{
    public string Instance { get; set; } = 
        "https://login.microsoftonline.com/";
    public string Domain { get; set; }
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string CallbackPath { get; set; }
    public string Scopes { get; set; }
}
