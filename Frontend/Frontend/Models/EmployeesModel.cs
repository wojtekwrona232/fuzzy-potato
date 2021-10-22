using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Models
{
    public class EmployeesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Link { get; set; }
    }
}