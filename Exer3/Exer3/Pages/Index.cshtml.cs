using Exer3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;

namespace Exer3.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDataService service;
    public string ErrorMessage = "";
    public string username = "";
    public string password = "";

    public IndexModel(ILogger<IndexModel> logger, IDataService service)
    {
        _logger = logger;
        this.service = service;
    }

    public void OnGet()
    {
      
    }

    public ActionResult OnPost(string username, string password)
    {
        HideError();
        if (service.isEmail(username))
        {
            if (service.authenticateEmail(username, password))
            {
                return RedirectToPage("Main");
            }
            else 
            {
                ShowError("Username or Password Incorrect");
                return Page();
            }
        }
        else
        {
            if(service.autheticateUsername(username, password))
            {
                return RedirectToPage("Main");
            }
            else
            {
                ShowError("Username or Password Incorrect");
                return Page();
            }
        }
            
    }

    public void ShowError(string msg)
    {
        ErrorMessage = msg;
    }

    public void HideError()
    {
        ErrorMessage = "";
    }
}
