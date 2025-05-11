using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exer3.Pages
{
    public class MainModel : PageModel
    {
        public ActionResult OnPostItem()
        {
            return RedirectToPage("Item");
        }

        public ActionResult OnPostAgent()
        {
            return RedirectToPage("Agent");
        }

        public ActionResult OnPostOrder()
        {
            return RedirectToPage("Order");
        }

        public void OnGet()
        {

        }
    }
}
