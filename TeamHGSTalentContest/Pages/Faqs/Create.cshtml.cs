using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.Pages.Faqs
{
    public class CreateModel : PageModel
    {
        private readonly TeamHGSTalentContest.Data.ApplicationDbContext _context;

        public CreateModel(TeamHGSTalentContest.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Faq Faq { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Faqs.Add(Faq);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}