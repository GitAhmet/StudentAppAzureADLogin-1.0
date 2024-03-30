
namespace StudentApp.Configurations;

public class SwaggerAzureAD
{
    public string AuthorizationUrl {get; set; } = 
        "https://login.microsoftonline.com/d5af55b1-09d1-4c62-b91b-d108fd981704/oauth2/v2.0/authorize";
    public string TokenUrl { get; set;}   
    public string Scope { get; set;}
    public string ClientId { get; set;}
}
