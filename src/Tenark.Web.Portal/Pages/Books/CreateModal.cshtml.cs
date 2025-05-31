using Microsoft.AspNetCore.Mvc;
using OpenId2Ids.Books;
using System.Threading.Tasks;
using Tenark.Web.Portal.Pages;

namespace Tenark.Web.Pages.Books
{
    public class CreateModalModel : TenarkPageModel
    {
        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }

        private readonly IBookAppService _bookAppService;

        public CreateModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public void OnGet()
        {
            Book = new CreateUpdateBookDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}