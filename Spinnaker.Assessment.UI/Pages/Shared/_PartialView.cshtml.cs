using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Spinnaker.Assessment.UI.Pages.Shared
{
    public class _PartialViewModel : PageModel
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public void OnGet()
        {
        }
    }
}
