using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Tenark.Web.Portal.Pages.Books;

public class IndexModel : PageModel
{
    public string RemoteServiesPath;
    private readonly IConfiguration _config;

    // Expose a C# property with the setting
    public string ApiBaseUrl { get; private set; }

    public IndexModel(IConfiguration config)
    {
        _config = config;
    }
    public void OnGet()
    {
        RemoteServiesPath = _config["RemoteServices:Default:BaseUrl"] ?? "";
    }
}