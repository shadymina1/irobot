using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using irobot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace irobot.Pages
{
    public class ProfileModel : PageModel
    {
        public User user { get; set; }
        public string notAllowd { get; set; } = "Not allowsd";
        [BindProperty(SupportsGet = true)]
        public string fname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string lname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(fname))
            {
                
                notAllowd = "unknown user";
                return RedirectToPage("Signup", new { notAllowd });
            }
            else
            {
                return Page();
            }
        }
    }
}
