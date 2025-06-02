using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace OpenId2Ids.Web.Portal.Pages;

public class IndexModel : OpenId2IdsPageModel
{
    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
