namespace Infinion.Infrastructure.HelperMethods;
public static class UrlHelper
{
    public static string GenerateConfirmationLink(
        string baseUrl, 
        string controller, 
        string action, 
        string token, 
        string email)
    {
        var uriBuilder = new UriBuilder(baseUrl)
            {
                Path = $"{controller}/{action}"
            };
        var query = System.Web.HttpUtility
            .ParseQueryString(string.Empty);
        query["token"] = token;
        query["email"] = email;
        uriBuilder.Query = query.ToString();
        return uriBuilder.ToString();
    }
}
